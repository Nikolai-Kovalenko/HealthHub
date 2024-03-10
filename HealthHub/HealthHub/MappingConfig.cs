using AutoMapper;
using HealthHub.Models;
using HealthHub.Models.DTO.ProfileDTO;
using HealthHub.Models.DTO.UserDTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HealthHub
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<DoctorProfile, DoctorProfileDTO>().ReverseMap();
            CreateMap<AppUser, AppUserDTO>().ReverseMap();
        }
    }
}
