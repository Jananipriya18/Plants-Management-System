using dotnetapp.Models;

namespace dotnetapp.Services
{
    public interface IAuthService
    {
        Task<(int, string)> Registeration(User model, string role);
        Task<(int, string)> Login(LoginModel model);
    }
}
