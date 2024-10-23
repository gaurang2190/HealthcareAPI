using HealthcareAPI.BusinessAccessLayer.Models;

namespace HealthcareAPI.BusinessAccessLayer.Contract
{
    public interface IRegisterService
    {
        ServiceResponse<string> RegisterUserService(UserModel register);

        ServiceResponse<string> LoginUserService(Login login);
        
        
    }
}
