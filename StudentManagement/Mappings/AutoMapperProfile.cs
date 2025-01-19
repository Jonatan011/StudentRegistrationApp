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
            // A�ade otros mapeos seg�n sea necesario.
        }
    }
}
