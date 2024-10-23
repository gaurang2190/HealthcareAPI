using AutoMapper;
using HealthcareAPI.BusinessAccessLayer.Models;

namespace HealthcareAPI.BusinessAccessLayer.MappingProfiles
{
    public class BalMappingProfile :  Profile
    {
        public BalMappingProfile()
        {
            CreateMap<UserModel, HealthcareAPI.DataAccessLayer.Models.User>().ReverseMap();
            CreateMap<HealthcareProfessional, HealthcareAPI.DataAccessLayer.Models.HealthcareProfessional>().ReverseMap();
            CreateMap<Appointment, HealthcareAPI.DataAccessLayer.Models.Appointment>().ReverseMap();
        }
    }
}
