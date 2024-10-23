using HealthcareAPI.DataAccessLayer.Contract;
using HealthcareAPI.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthcareAPI.DataAccessLayer.Implementation
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AppointmentRepository(ApplicationDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public bool AddAppointment(Appointment appointment)
        {
            bool result = false;
            if (appointment != null)
            {
                _dbContext.Appointments.Add(appointment);
                _dbContext.SaveChanges();
                result = true;
            }
            return result;
        }

        public Appointment GetAppointmentById(int id, int userId)
        {
            return _dbContext.Appointments.FirstOrDefault(a => a.Id == id && a.UserId == userId);
        }

        public List<Appointment> GetAppointmentsByUserId(int userId)
        {
            return _dbContext.Appointments
           .Where(a => a.UserId == userId)
           .Include(a => a.HealthcareProfessional)
           .ToList();
        }

        public bool IsProfessionalAvailable(int professionalId, DateTime startTime, DateTime endTime)
        {
           var data = _dbContext.Appointments.FirstOrDefault(a => a.HealthcareProfessionalId == professionalId &&
                           a.Status == "Booked" &&
                           a.AppointmentStartTime < endTime &&
                           a.AppointmentEndTime > startTime);
            if(data != null) { 
                return false;
            }
            return true;
        }

        public bool UpdateAppointment(Appointment appointment)
        {
            bool result = false;
            if (appointment != null)
            {
                _dbContext.Appointments.Update(appointment);
                _dbContext.SaveChanges();
                result = true;
            }
            return result;
        }
    }
}
