using Lit_Museum_Users.Application.IService;

namespace Lit_Museum_Users.Application.Service
{
    public class PasswordHashingService : IPasswordHashingService
    {

        public PasswordHashingService()
        {
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public bool VerifyPassword(string password, string hashedPassword)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            }
            catch (BCrypt.Net.SaltParseException)
            {
                return false;
            }
        }
    }
}
