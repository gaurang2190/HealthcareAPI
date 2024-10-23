using HealthcareAPI.DataAccessLayer.Models;

namespace HealthcareAPI.DataAccessLayer.Contract
{
    public interface IAppointmentRepository
    {
        List<Appointment> GetAppointmentsByUserId(int userId);
        Appointment GetAppointmentById(int id, int userId);
        bool IsProfessionalAvailable(int professionalId, DateTime startTime, DateTime endTime);
        bool AddAppointment(Appointment appointment);
        bool UpdateAppointment(Appointment appointment);
    }
}
