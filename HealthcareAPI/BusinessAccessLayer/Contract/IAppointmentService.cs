
using HealthcareAPI.BusinessAccessLayer.Models;

namespace HealthcareAPI.BusinessAccessLayer.Contract
{
    public interface IAppointmentService
    {
        ServiceResponse<List<Appointment>> GetAppointmentsForUser(int userId);
        ServiceResponse<string> BookAppointment(Appointment appointment, int userId);
        ServiceResponse<string> CancelAppointment(int id, int userId);
    }
}
