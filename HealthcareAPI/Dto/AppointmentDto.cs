namespace HealthcareAPI.Dto
{
    public class AppointmentDto
    {
        public int UserId { get; set; }
        public int HealthcareProfessionalId { get; set; }
        public DateTime AppointmentStartTime { get; set; }
        public DateTime AppointmentEndTime { get; set; }
        public string Status { get; set; }
    }
}
