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
              .ForMember(dest => dest.AdmissionDetails, opt => opt.MapFrom(src => src.AdmissionDetails));

            CreateMap<AdmissionDetails, AdmissionDetailsDTO>()
                .ForMember(dest => dest.Student_ID, opt => opt.MapFrom(src => src.Student_ID));

            CreateMap<StudentDTO, Student>();
            CreateMap<AdmissionDetailsDTO, AdmissionDetails>();
        }
    }
}
