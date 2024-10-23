using AutoMapper;
using HealthcareAPI.BusinessAccessLayer.Contract;
using HealthcareAPI.BusinessAccessLayer.Models;
using HealthcareAPI.Dto;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IRegisterService _registerService;
        private readonly IMapper _mapper;

        public AuthController(IRegisterService registerService, IMapper mapper)
        {
            _registerService = registerService;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public IActionResult Register(RegisterDto registerDto)
        {
            var register = _mapper.Map<UserModel>(registerDto);
            var response = _registerService.RegisterUserService(register);
            return !response.Success ? BadRequest(response) : Ok(response);
        }


        [HttpPost("Login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var login = _mapper.Map<Login>(loginDto);
            var response = _registerService.LoginUserService(login);
            return !response.Success ? BadRequest(response) : Ok(response);
        }

    }
}
