using AutoMapper;
using CollegeApp.data;
using CollegeApp.Models;

namespace CollegeApp.Configration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // Mapping from Student to StudentDTO
            CreateMap<Student, StudentDTO>()
                .ForMember(dest => dest.AdmissionDetails, opt => opt.MapFrom(src => src.AdmissionDetails))
                .ForMember(dest => dest.CasteCertificateDetails, opt => opt.MapFrom(src => src.StudentCasteCertificateDetails));

            // Mapping from AdmissionDetails to AdmissionDetailsDTO and vice versa
            CreateMap<AdmissionDetails, AdmissionDetailsDTO>()
                .ForMember(dest => dest.Student_ID, opt => opt.MapFrom(src => src.Student_ID))
                .ReverseMap();  

            // Mapping from StudentDTO to Student for reverse operation
            CreateMap<StudentDTO, Student>()
                .ForMember(dest => dest.AdmissionDetails, opt => opt.MapFrom(src => src.AdmissionDetails))
                .ForMember(dest => dest.StudentCasteCertificateDetails, opt => opt.MapFrom(src => src.CasteCertificateDetails));

            // Mapping from CasteCertificateDetails to CasteCertificateDetailsDTO and vice versa
            CreateMap<StudentCasteCertificateDetails, CasteCertificateDetailsDTO>()
                .ReverseMap();  
        }
    }

}
