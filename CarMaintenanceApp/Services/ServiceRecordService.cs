using CarMaintenanceApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarMaintenanceApp.Services
{
    public class ServiceRecordService
    {
        private readonly CarMaintenanceContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _enviroment;

        public ServiceRecordService(CarMaintenanceContext context, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment enviroment)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _enviroment = enviroment;
        }

        public async Task<List<ServiceRecord>> GetServiceRecordsAsync(int carID)
        {
            var userIdString = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Sid);
            if (int.TryParse(userIdString, out int userId))
            {
                return await _context.ServiceRecords
                    .Where(s => s.CarId == carID && s.Car.UserId == userId)
                    .OrderBy(s => s.Mileage)
                    .ToListAsync();
            }
            else
            {
                return new List<ServiceRecord>();
            }    
        }
        public async Task<(bool Success, string ErrorMessage)> AddServiceRecordAsync(ServiceRecord serviceRecord, Stream? imageStream)
        {
            var mostRecentServiceRecord = await _context.ServiceRecords
                .Where(s => s.CarId == serviceRecord.CarId)
                .OrderByDescending(s => s.ServiceDate)
                .FirstOrDefaultAsync();

            if (mostRecentServiceRecord != null)
            {
                if (serviceRecord.ServiceDate < mostRecentServiceRecord.ServiceDate && serviceRecord.Mileage > mostRecentServiceRecord.Mileage)
                {
                    return (false, "Mileage cannot be higher than the previous service record for an earlier date.");
                }
                else if (serviceRecord.ServiceDate >= mostRecentServiceRecord.ServiceDate && serviceRecord.Mileage < mostRecentServiceRecord.Mileage)
                {
                    return (false, "Mileage must be greater than the previous service record for the same or later date.");
                }
            }

            if (imageStream != null)
            {
                using var memoryStream = new MemoryStream();
                await imageStream.CopyToAsync(memoryStream);
                serviceRecord.Image = memoryStream.ToArray();

                var filePath = Path.Combine(_enviroment.WebRootPath, "images", $"{serviceRecord.Id}.png");
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                await File.WriteAllBytesAsync(filePath, serviceRecord.Image);
                serviceRecord.ImagePath = filePath;
            }

            _context.ServiceRecords.Add(serviceRecord);
            var car = await _context.Cars.FindAsync(serviceRecord.CarId);
            if (car != null && serviceRecord.Mileage > car.Mileage)
            {
                car.Mileage = serviceRecord.Mileage;
                _context.Cars.Update(car);
            }
            await _context.SaveChangesAsync();

            return (true, string.Empty);
        }
    }
}
