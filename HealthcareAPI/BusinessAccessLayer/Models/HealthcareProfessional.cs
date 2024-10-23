using System.ComponentModel.DataAnnotations;

namespace HealthcareAPI.BusinessAccessLayer.Models
{
    public class HealthcareProfessional
    {
        public int Id { get; set; }
      
        public string Name { get; set; }
      
        public string Specialty { get; set; }
    }
}
