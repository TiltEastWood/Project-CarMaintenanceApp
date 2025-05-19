using CarMaintenanceApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarMaintenanceApp.Services
{
    public class CarService

    {
        private readonly CarMaintenanceContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CarService> _logger;

        public CarService(CarMaintenanceContext context, ILogger<CarService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
            _logger.LogInformation("CarService created.");
        }

        public async Task<List<Car>> GetCarsAsync()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                _logger.LogError("HTTP Context is null.");
                return new List<Car>();
            }

            var userIdString = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Sid);
            if (string.IsNullOrWhiteSpace(userIdString))
            {
                _logger.LogError("User ID claim is missing or empty.");
                return new List<Car>();
            }

            _logger.LogInformation("User ID: {userId}", userIdString);

            if (int.TryParse(userIdString, out int userId))
            {
                return await _context.Cars.Where(c => c.UserId == userId).ToListAsync();
            }
            else 
            {
                _logger.LogError("Invalid user ID format.");
                return new List<Car>();
            }
        }
        public async Task<Car?> GetCarByIdAsync(int id)
        {
            var userIDString = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Sid);
            if (string.IsNullOrWhiteSpace(userIDString))
            {
                _logger.LogError("User ID claim is missing or empty.");
                return null;
            }
            if(!int.TryParse(userIDString, out int userId))
            {
                _logger.LogError("Invalid user ID format.");
                return null;
            }

            _logger.LogInformation("Fetching car with ID: {id} for user ID: {userId}", id, userId);
            var car = await _context.Cars
                .Include(c => c.ServiceRecords)
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            if (car == null)
            {
                _logger.LogWarning("Car with ID: {id} not found for user ID: {userId}", id, userId);
            }
            else
            {
                _logger.LogInformation("Car with ID: {id} fetched successfully for user ID: {userId}", id, userId);
            }

            return car;
        }
        public async Task AddCarAsync(Car car)
        {
            try
            {
                _logger.LogInformation("Adding car: {@Car}", car);
                _context.Cars.Add(car);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Car added Succesfully.");
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error adding car: {@car}", car);
                throw;
            }
        }
        public async Task UpdateCarAsync(Car car)
        {
            
            _context.Cars.Update(car);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCarAsync(int carId)
        {
            var car = await _context.Cars.FindAsync(carId);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Car with id {carId} deleted.", carId);
            }
            else
            {
                _logger.LogWarning("Car with id {carId} not found.", carId);
            }
        }
        public async Task HandleInvalidSubmit(int id)
        {
            _logger.LogInformation($"Invalid submit detected. Car id : {id}");
        }
    }
}
