// Application.Common.Interfaces/ICurrentUserService.cs
namespace Application.Common.Interfaces
{
    public interface ICurrentLoggedInUserService
    {
        Guid? UserId { get; }
    }
}
