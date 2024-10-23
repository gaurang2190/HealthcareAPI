using AutoMapper;
using HealthcareAPI.BusinessAccessLayer.Contract;
using HealthcareAPI.BusinessAccessLayer.Models;
using HealthcareAPI.DataAccessLayer.Contract;

namespace HealthcareAPI.BusinessAccessLayer.Implementation
{
    public class HealthcareProfessionalService : IHealthcareProfessionalService
    {
        private readonly IHealthcareProfessionalRepository _repository;
        private readonly IMapper _mapper;

        public HealthcareProfessionalService(IHealthcareProfessionalRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ServiceResponse<IEnumerable<HealthcareProfessional>> GetHealthcareProfessional()
        {
            var response = new ServiceResponse<IEnumerable<HealthcareProfessional>>();
            var healthcareProfessional = _repository.GetHealthcareProfessional();
            if (healthcareProfessional != null)
            {
                response.Data = _mapper.Map<IEnumerable<HealthcareProfessional>>(healthcareProfessional);
            }
            else
            {
                response.Success = false;
                response.Message = "No record found!";
            }
            return response;
        }
    }
}
