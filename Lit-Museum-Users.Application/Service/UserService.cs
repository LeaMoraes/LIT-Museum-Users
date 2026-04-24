using Lit_Museum_Users.Application.Dtos;
using Lit_Museum_Users.Application.IService;
using Lit_Museum_Users.Domain.Entity;
using Lit_Museum_Users.Domain.Events.User;
using Lit_Museum_Users.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lit_Museum_Users.Application.Service
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly ILogger<UserService> _logger;
        private readonly IEventStoreRepository _eventStoreRepository;

        public UserService(IUserRepository userRepository, IPasswordHashingService passwordHashingService, ILogger<UserService> logger, IEventStoreRepository eventStoreRepository) 
        {
            _userRepository = userRepository;
            _passwordHashingService = passwordHashingService;
            _logger = logger;
            _eventStoreRepository = eventStoreRepository;
        }

        private async Task SaveEventsAndUpdateVersion(User user, string aggregateId, int expectedVersion)
        {
            var eventCount = user.GetUncommittedEvents().Count();

            await _eventStoreRepository.SaveEventsAsync(aggregateId, "User", user.GetUncommittedEvents(), expectedVersion);
            var newVersion = expectedVersion + eventCount;
            user.Version = newVersion;

            await _userRepository.UpdateAsync(user);
            user.MarkEventsAsCommitted();
        }

        public async Task<User> CreateUserAsync(UserCreateDto userDto)
        {
            _logger.LogInformation("Criando um novo Usuario: {Email}", userDto.Email);

            if (string.IsNullOrEmpty(userDto.Name))
            { throw new ArgumentException("Nome do Usuario não pode estar em branco"); }
            if (userDto.Name.Equals(userDto.Email, StringComparison.OrdinalIgnoreCase))
            { throw new ArgumentException("Nome do Usuario não pode ser igual ao email"); }
            if (!IsValidEmail(userDto.Email))
            { throw new ArgumentException("Formato de email invalido"); }
            if (!IsValidPassword(userDto.Password))
            { throw new ArgumentException("A senha deve ter no minimo 8 caracteres, incluindo letras maiusculas, minusculas, numeros e caracteres especiais"); }

            var existindUser = await _userRepository.GetUserByEmail(userDto.Email);
            if (existindUser != null)
            {
                throw new ArgumentException("Já existe um usuario cadastrado com este email");
            }

            User user = new User();
            var userCreatedEvent = new UserCreatedEvent(userDto.Name, userDto.Email, "User");

            user.Apply(userCreatedEvent);
            user.PasswordHash = _passwordHashingService.HashPassword(userDto.Password);
            var createdUser = await _userRepository.AddAsync(user);
            await SaveEventsAndUpdateVersion(user, createdUser.Id.ToString(), 0);

            _logger.LogInformation("Usuario criado com sucesso: {UserID}", createdUser.Id);
            return createdUser;

        }

        public async Task<User> UpdateUserAsync(int id, UserUpdateDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            if (string.IsNullOrEmpty(userDto.Name))
                throw new ArgumentException("Nome do usuario não pode estar em branco");
            if (string.IsNullOrEmpty(userDto.Email))
                throw new ArgumentException("Formato de email invalido");
            if (!IsValidPassword(userDto.Password))
                throw new ArgumentException("A senha deve ter no minimo 8 caracteres, incluindo letras maiusculas, minusculas, numeros e caracteres especiais");

            var existingUser = await _userRepository.GetUserByEmail(userDto.Email);
            if (existingUser != null && existingUser.Id != id)
                throw new ArgumentException("Ja existe um usuario cadastrado com este email");

            var userUpdatedEvent = new UserUpdatedEvent(userDto.Name, userDto.Email);
            user.Apply(userUpdatedEvent);
            user.PasswordHash = _passwordHashingService.HashPassword(userDto.Password);

            await SaveEventsAndUpdateVersion(user, id.ToString(), user.Version);
            
            return user;

        }

        public async Task<User> ChangePasswordAsync(int id, UserChangePasswordDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            if (string.IsNullOrEmpty(userDto.Password))
                throw new ArgumentException("A senha atual não pode estar vazia");

            if (string.IsNullOrEmpty(userDto.NewPassword))
                throw new ArgumentException("A nova senha não pode estar vazia");
            if (!IsValidPassword(userDto.NewPassword))
                throw new ArgumentException("A senha deve ter no minimo 8 caracteres, incluindo letras maiusculas, minusculas, numeros e caracteres especiais");
            if (_passwordHashingService.VerifyPassword(userDto.Password, user.PasswordHash))
                throw new ArgumentException("A senha atual informada não esta correta");

            var passwordChangeEvent = new UserPasswordChangedEvent();
            user.Apply(passwordChangeEvent);
            user.PasswordHash = _passwordHashingService.HashPassword(userDto.NewPassword);

            await SaveEventsAndUpdateVersion(user, id.ToString(), user.Version);
            return user;
        }

        public async Task<bool> ChangeRole(int id, string role)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null) return false;

            if (!IsValidRole(role))
                throw new ArgumentException("O nivel de acesso informado não existe");

            var roleChangeEvent = new UserRoleChangedEvent(role);
            user.Apply(roleChangeEvent);

            await SaveEventsAndUpdateVersion(user, id.ToString(), user.Version);
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            var userDeletedEvent = new UserDeletedEvent();
            user.Apply(userDeletedEvent);

            await SaveEventsAndUpdateVersion(user, id.ToString(), user.Version);
            return true;
        }

        public async Task<List<User>> GetAll()
        {
            _logger.LogInformation("Buscando todos os usuarios");
            var users = await _userRepository.GetAllAsync();
            _logger.LogInformation("Encontrado {Count} usuarios", users.Count);
            return users;
        }

        public async Task<User> GetById(int id)
        {
            _logger.LogInformation("Buscando usuario com ID: {UsuarioId}", id);
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null) { _logger.LogInformation("Usuario encontrado: {UserName}", user.Name); }
            else { _logger.LogWarning("Usuario com ID {UsuarioId} não encontrado", id); }
            return user;
        }

        

        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) { return false; }
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        public bool IsValidPassword(string password)
        {

            if (string.IsNullOrEmpty(password)) { return false; }
            if (password.Length < 8) { return false; }
            if (!Regex.IsMatch(password, "[A-Z]")) { return false; }
            if (!Regex.IsMatch(password, "[a-z]")) { return false; }
            if (!Regex.IsMatch(password, "[0-9]")) { return false; }
            if (!Regex.IsMatch(password, "[!@#$%^&*(),.?:{}|<>]")) { return false; }
            return true;
        }

        public bool IsValidRole(string role)
        {
            if (role.Equals("Admin") || role.Equals("User")) { return true; }
            return false;
        }

    }
}
