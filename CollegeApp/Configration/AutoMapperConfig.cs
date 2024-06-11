using AutoMapper;
using CollegeApp.data;
using CollegeApp.Models;

namespace CollegeApp.Configration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Student, StudentDTO>()
                .ForMember(dest => dest.AdmissionDetails, opt => opt.MapFrom(src => src.AdmissionDetails))
                .ForMember(dest => dest.CasteCertificateDetails, opt => opt.MapFrom(src => src.StudentCasteCertificateDetails));

            CreateMap<AdmissionDetails, AdmissionDetailsDTO>()
                .ForMember(dest => dest.Student_ID, opt => opt.MapFrom(src => src.Student_ID));

            CreateMap<StudentDTO, Student>();
            CreateMap<AdmissionDetailsDTO, AdmissionDetails>();
            CreateMap<StudentCasteCertificateDetails, CasteCertificateDetailsDTO>().ReverseMap();
        }
    }
}
