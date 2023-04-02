using MillionandUpBackend01.Dtos.User;

namespace MillionandUpBackend01.Services {
    public interface IUserService {
        public Task<UserDto> GetUserAsync(string username);
        public Task<UserDto> CreateUserAsync(UserCreateDto newUser);
    }
}
