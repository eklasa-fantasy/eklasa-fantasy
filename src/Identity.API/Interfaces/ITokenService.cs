using Identity.API.Models;

namespace Identity.API.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(ApplicationUser user);
    }
}
