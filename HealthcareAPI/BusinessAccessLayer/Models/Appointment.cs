using HealthcareAPI.DataAccessLayer.Models;

namespace HealthcareAPI.BusinessAccessLayer.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int HealthcareProfessionalId { get; set; }
        public DateTime AppointmentStartTime { get; set; }
        public DateTime AppointmentEndTime { get; set; }
        public string Status { get; set; }  // Booked, Completed, Cancelled

        public User? User { get; set; }
        public HealthcareProfessional? HealthcareProfessional { get; set; }
    }
}
