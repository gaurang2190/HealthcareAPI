using AutoMapper;
using HealthcareAPI.BusinessAccessLayer.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HealthcareProfessionalController : ControllerBase
    {
        private readonly IHealthcareProfessionalService _service;
        private readonly IMapper _mapper;

        public HealthcareProfessionalController(IHealthcareProfessionalService registerService, IMapper mapper)
        {
            _service = registerService;
            _mapper = mapper;
        }

        [HttpGet("GetAllHealthcareProfessional")]
        public IActionResult GetAllHealthcareProfessional()
        {
            var response = _service.GetHealthcareProfessional();
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
