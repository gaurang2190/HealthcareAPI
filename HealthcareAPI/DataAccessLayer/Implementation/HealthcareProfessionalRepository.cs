using HealthcareAPI.DataAccessLayer.Contract;
using HealthcareAPI.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareAPI.DataAccessLayer.Implementation
{
    public class HealthcareProfessionalRepository : IHealthcareProfessionalRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public HealthcareProfessionalRepository(ApplicationDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public IEnumerable<HealthcareProfessional> GetHealthcareProfessional()
        {
            var ressult = _dbContext.HealthcareProfessionals.AsNoTracking().ToList();
            return ressult != null ? ressult : new List<HealthcareProfessional>();
        }

        public HealthcareProfessional GetHealthcareProfessionalById(int id)
        {
            var ressult = _dbContext.HealthcareProfessionals.FirstOrDefault(x => x.Id == id);   
            return ressult != null ? ressult : new HealthcareProfessional();
        }
    }
}
