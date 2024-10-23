using System.ComponentModel.DataAnnotations;

namespace HealthcareAPI.DataAccessLayer.Models
{
    public class HealthcareProfessional
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Specialty { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }

    }
}
