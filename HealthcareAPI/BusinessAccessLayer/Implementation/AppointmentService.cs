using AutoMapper;
using HealthcareAPI.BusinessAccessLayer.Contract;
using HealthcareAPI.BusinessAccessLayer.Models;
using HealthcareAPI.DataAccessLayer.Contract;

namespace HealthcareAPI.BusinessAccessLayer.Implementation
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repository;
        private readonly IHealthcareProfessionalRepository _healthRepository;
        private readonly IMapper _mapper;

        public AppointmentService(IAppointmentRepository repository, IHealthcareProfessionalRepository healthRepository, IMapper mapper)
        {
            _repository = repository;
            _healthRepository = healthRepository;
            _mapper = mapper;
        }

        public ServiceResponse<string> BookAppointment(Appointment appointment, int userId)
        {
            var response = new ServiceResponse<string>();
            var message = string.Empty;

            var professional = _healthRepository.GetHealthcareProfessionalById(appointment.HealthcareProfessionalId);
            if (professional == null)
            {
                response.Success = false;
                response.Message = "Healthcare professional not found.";
                return response;
            }

            var isAvailable = _repository.IsProfessionalAvailable(
                                       appointment.HealthcareProfessionalId,
                                       appointment.AppointmentStartTime,
                                       appointment.AppointmentEndTime);

            if (!isAvailable)
            {
                response.Success = false;
                response.Message = "The professional is not available at the selected time.";
                return response;
            }
            var appointmentdata = _mapper.Map<HealthcareAPI.DataAccessLayer.Models.Appointment>(appointment);

           

           var result = _repository.AddAppointment(appointmentdata);
            if (result)
            {
                response.Success = true;
                response.Message = "Appointment booked successfully.";
                return response;
            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong, please try after sometime.";
                return response;
            }
          
        }

        public ServiceResponse<string> CancelAppointment(int id, int userId)
        {
            var response = new ServiceResponse<string>();
            var appointment =  _repository.GetAppointmentById(id, userId);
            if (appointment == null)
            {
                response.Success = false;
                response.Message = "Appointment not found.";
                return response;
                
            }
            var timeUntilAppointment = appointment.AppointmentStartTime - DateTime.Now;
            if (timeUntilAppointment.TotalHours <= 24)
                {
                response.Success = false;
                response.Message = "Cannot cancel appointment within 24 hours.";
                return response;
            }

            appointment.Status = "Cancelled";
            var result = _repository.UpdateAppointment(appointment);
            if (result)
            {
                response.Success = true;
                response.Message = "Appointment cancelled successfully.";
                return response;
            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong, please try after sometime.";
                return response;
            }
        }

        public ServiceResponse<List<Appointment>> GetAppointmentsForUser(int userId)
        {
            var response = new ServiceResponse<List<Appointment>>();

            var appointments = _repository.GetAppointmentsByUserId(userId);
            if (appointments != null)
            {
                response.Data = _mapper.Map<List<Appointment>>(appointments);
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
