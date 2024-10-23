

using HealthcareAPI.BusinessAccessLayer.Models;

namespace HealthcareAPI.BusinessAccessLayer.Contract
{
    public interface IHealthcareProfessionalService
    {
        ServiceResponse<IEnumerable<HealthcareProfessional>> GetHealthcareProfessional();
    }
}
