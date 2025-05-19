using CarMaintenanceApp.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace CarMaintenanceApp.Services
{
    public class AuthServices
    {
        private readonly CarMaintenanceContext _context;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILogger<UserService> _logger;

        public AuthServices(CarMaintenanceContext context, AuthenticationStateProvider authenticationStateProvider, IPasswordHasher<ApplicationUser> passwordHasher, ILogger<UserService> logger)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _passwordHasher = passwordHasher;
            _logger = logger;
            _context = context;
        }
        public async Task<bool> RegisterAsync(CarMaintenanceContext context, RegisterModel model) 
        {
            var user = new ApplicationUser
            {
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Password = _passwordHasher.HashPassword(null, model.Password)
            };

            await context.UserAccounts.AddAsync(user);
            var result = await context.SaveChangesAsync();

            return result > 0;
        }
        public async Task<bool> RegisterUserAsync(ApplicationUser user, string password)
        {
            if (await _context.UserAccounts.AnyAsync(u => u.UserName == user.UserName))
            {
                _logger.LogInformation("User already exists.");
                return false;
            }

            user.Password = _passwordHasher.HashPassword(user, password);
            _context.UserAccounts.Add(user);
            await _context.SaveChangesAsync();
            _logger.LogInformation("User registered.");
            return true;
        }

        public async Task<ApplicationUser?> AuthenticateUserAsync(string username, string password)
        {
            var user = await _context.UserAccounts.SingleOrDefaultAsync(u => u.UserName == username);
            if (user == null)
            {
                _logger.LogWarning("User with username {username} not found.", username);
                return null;
            }
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            if (result == PasswordVerificationResult.Success)
            {
                _logger.LogInformation("User {username} authenticated.", username);
                return user;
            }
            _logger.LogWarning("User {username} not authenticated.", username);
            return null;
        }
    }
}
