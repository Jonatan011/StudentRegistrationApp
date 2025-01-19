using AutoMapper;
using StudentManagement.Data.Entities;
using StudentManagement.Models;

namespace StudentManagement.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, StudentDto>();
            // Añade otros mapeos según sea necesario.
        }
    }
}
