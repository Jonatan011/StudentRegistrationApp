/*using AutoMapper;
namespace StudentManagement.Common.Helpers
{
    public static class CustomMapper
    {
        private static readonly Lazy<IMapper> _lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();  // Aseg�rate de que AutoMapperProfile est� bien definido
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => _lazy.Value;
    }
}
*/