using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using CarMaintenanceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CarMaintenanceApp.Services
{

    public class UserService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly CarMaintenanceContext _context;

        public UserService(AuthenticationStateProvider authenticationStateProvider, CarMaintenanceContext context)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _context = context;
        }

        public async Task<string?> GetUserNameAsync()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if(user.Identity?.IsAuthenticated == true)
            {
                return user.FindFirstValue("FirstName");
            }
            return null;   
        }
        public async Task<bool> DeleteUserAndCarAsync(int userID)
        {
            var user = await _context.UserAccounts.Include(u => u.Cars).FirstOrDefaultAsync(u => u.Id == userID);
            if (user == null)
            {
                return false;
            }

            _context.UserAccounts.Remove(user);
            _context.Cars.RemoveRange(user.Cars);
            
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
