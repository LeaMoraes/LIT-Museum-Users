using Lit_Museum_Users.Application.Dtos;
using Lit_Museum_Users.Core.Entity;
using Lit_Museum_Users.Core.Events.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lit_Museum_Users.Application.IService
{
    public interface IUserService
    {

        Task<List<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> CreateUserAsync(UserCreateDto userDto);
        Task<User> UpdateUserAsync(int id, UserUpdateDto userDto);
        Task<User> ChangePasswordAsync(int id, UserChangePasswordDto userDto);
        Task<bool> ChangeRole(int id, string role);
        Task<bool> DeleteUserAsync(int id);

    }
}
