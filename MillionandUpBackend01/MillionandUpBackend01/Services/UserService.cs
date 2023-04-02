using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MillionandUpBackend01.Dtos.User;
using MillionandUpBackend01.Entities;
using MillionandUpBackend01.Exceptions;

namespace MillionandUpBackend01.Services
{
    public class UserService : IUserService {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserService(DataContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUserAsync(string username) {
            try {
                User? user = await _context.Users.Where(user => user.Username == username).FirstAsync();
                return _mapper.Map<UserDto>(user);
            } catch (Exception) {
                throw new RecordNotFoundException("RecordNotFound");
            }
        }

        public async Task<UserDto> CreateUserAsync(UserCreateDto newUser) {
            try {
                User? oldUser = await _context.Users.Where(user => user.Username == newUser.Username).FirstAsync();
                throw new RecordExistException("RecordExist");
            } catch (Exception) {
                User user = _mapper.Map<User>(newUser);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return _mapper.Map<UserDto>(user);
            }
        }
    }
}
