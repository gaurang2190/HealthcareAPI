using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthcareAPI.DataAccessLayer.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int HealthcareProfessionalId { get; set; }
        public DateTime AppointmentStartTime { get; set; }
        public DateTime AppointmentEndTime { get; set; }
        public string Status { get; set; }  // Booked, Completed, Cancelled
        public virtual User User { get; set; }
        public virtual HealthcareProfessional HealthcareProfessional { get; set; }
    }
}
