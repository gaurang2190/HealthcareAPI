using AutoMapper;
using HealthcareAPI.BusinessAccessLayer.Models;
using HealthcareAPI.Dto;

namespace HealthcareAPI.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LoginDto, Login>().ReverseMap();
            CreateMap<RegisterDto, UserModel>().ReverseMap();
            CreateMap<HealthcareProfessionalDto, HealthcareProfessional>().ReverseMap();
            CreateMap<AppointmentDto, Appointment>().ReverseMap();
        }
    }
}
