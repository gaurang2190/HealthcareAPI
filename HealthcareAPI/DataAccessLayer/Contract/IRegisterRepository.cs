using HealthcareAPI.DataAccessLayer.Models;

namespace HealthcareAPI.DataAccessLayer.Contract
{
    public interface IRegisterRepository
    {
        bool RegisterUser(User user);

        User? ValidateUser(string username);

        bool UserExists(string email);

        User GetUserById(int id);
    }
}
