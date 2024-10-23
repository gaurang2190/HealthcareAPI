using AutoMapper;
using HealthcareAPI.BusinessAccessLayer.Contract;
using HealthcareAPI.BusinessAccessLayer.Models;
using HealthcareAPI.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthcareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _service;
        private readonly IMapper _mapper;

        public AppointmentController(IAppointmentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost("BookAppointment")]
        public IActionResult Register(AppointmentDto appointmentDto)
        {
             var userId = int.Parse(User.FindFirst("id").Value);

            var appointment = _mapper.Map<Appointment>(appointmentDto);
            var response = _service.BookAppointment(appointment, userId);
            return !response.Success ? BadRequest(response) : Ok(response);
        }

        [HttpGet("GetAppointments")]
        public IActionResult GetAppointments()
        {
            var userId = int.Parse(User.FindFirst("id").Value);
            var appointments = _service.GetAppointmentsForUser(userId);
            return Ok(appointments);
        }

        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            var userId = int.Parse(User.FindFirst("id").Value);
            var response = _service.CancelAppointment(id, userId);
             return !response.Success ? BadRequest(response) : Ok(response);
        }
    }
}
