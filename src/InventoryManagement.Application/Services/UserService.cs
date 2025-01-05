using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Interfaces;
using InventoryManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }
        public async Task<UserDto> Authenticate(string  username, string password) 
        {
            var user = await _userRepository.Authenticate(username, password);
            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Role= user.Role
            };
        }
    }
}
