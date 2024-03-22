using AutoMapper;
using HealthHub.Models;
using HealthHub.Models.DTO.MedCardDTO;
using HealthHub.Models.DTO.PatientDoctorRelationDTO;
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
            CreateMap<MedCard, MedCardDTO>().ReverseMap();
            CreateMap<AppUser, AppUserDTO>().ReverseMap();
            CreateMap<PatientDoctorRelation, PatientDoctorRelationDTO>().ReverseMap();
        }
    }
}
