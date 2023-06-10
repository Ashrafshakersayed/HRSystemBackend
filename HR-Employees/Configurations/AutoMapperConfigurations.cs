using AutoMapper;

namespace HR_Employees.Configurations
{
    public class AutoMapperConfigurations
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));
            return config.CreateMapper();
        }
    }
}
