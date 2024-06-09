using AutoMapper;
using CollegeApp.data;
using CollegeApp.Models;

namespace CollegeApp.Configration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Student, StudentDTO>().ReverseMap();
        }
    }
}
