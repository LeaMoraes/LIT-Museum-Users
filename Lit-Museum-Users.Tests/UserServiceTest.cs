using Lit_Museum_Users.Application.Dtos;
using Lit_Museum_Users.Application.IService;
using Lit_Museum_Users.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lit_Museum_Users.Tests
{
    public class UserServiceTest : IUserService
    {
        public Task<User> ChangePasswordAsync(int id, UserChangePasswordDto userDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangeRole(int id, string role)
        {
            throw new NotImplementedException();
        }

        public Task<User> CreateUserAsync(UserCreateDto userDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<User> UpdateUserAsync(int id, UserUpdateDto userDto)
        {
            throw new NotImplementedException();
        }
    }
}
