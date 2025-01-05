using CloudFileStorageMVC.Dtos.User;

namespace CloudFileStorageMVC.Services.User;

public interface IUserService
{
    Task<List<UserDto>> GetUsersAsync();
}
