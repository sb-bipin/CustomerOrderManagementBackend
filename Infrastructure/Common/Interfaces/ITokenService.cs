using Infrastructure.Identity;

public interface ITokenService
{
    string GenerateJwtToken(ApplicationUser user);
}
