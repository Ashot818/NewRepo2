using DotnetAuth.Domain.Entities;

namespace dotnetBlazorFullStuck.ApiService.Services.Abstract
{
    public interface ITokenService
    {
        Task<string> GenerateToken(ApplicationUser user);
        string GenerateRefreshToken();
    }
}
