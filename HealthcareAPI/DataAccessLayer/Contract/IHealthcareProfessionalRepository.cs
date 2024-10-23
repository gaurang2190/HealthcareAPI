using HealthcareAPI.DataAccessLayer.Models;

namespace HealthcareAPI.DataAccessLayer.Contract
{
    public interface IHealthcareProfessionalRepository
    {
        IEnumerable<HealthcareProfessional> GetHealthcareProfessional();
        HealthcareProfessional GetHealthcareProfessionalById(int id);
    }
}
