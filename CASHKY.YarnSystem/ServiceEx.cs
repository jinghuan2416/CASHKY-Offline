using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace CASHKY.YarnSystem
{
    public static class ServiceEx
    {
        public static IServiceCollection AddYarnServices(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<YarnCategory.YarnCategoryView>();
            serviceDescriptors.AddScoped<YarnCategory.YarnCategoryViewModel>();

            serviceDescriptors.AddScoped<YarnWarehousing.YarnWarehousingView>();
            serviceDescriptors.AddScoped<YarnWarehousing.YarnWarehousingViewModel>();

            serviceDescriptors.AddDbContextFactory<ORM.YarnDbContext>();

            serviceDescriptors.AddAutoMapper(Assembly.GetExecutingAssembly());

            return serviceDescriptors;
        }


    }


    public class LableProfile : AutoMapper.Profile
    {
        public LableProfile()
        {
            this.CreateMap<YarnCategory.YarnCategoryEntity, YarnCategory.YarnCategoryEntity>().ReverseMap();

            this.CreateMap<YarnWarehousing.YarnWarehousingEntity, YarnWarehousing.YarnWarehousingEntity>().ReverseMap();
            this.CreateMap<YarnWarehousing.InputYarnWarehousingModel, YarnWarehousing.InputYarnWarehousingModel>().ReverseMap();
            this.CreateMap<YarnWarehousing.YarnWarehousingEntity, YarnWarehousing.InputYarnWarehousingModel>().ReverseMap();

            this.CreateMap<YarnWarehousing.SearchYarnWarehousingModel, YarnWarehousing.InputYarnWarehousingModel>().ReverseMap();
            this.CreateMap<YarnWarehousing.SearchYarnWarehousingModel, YarnWarehousing.YarnWarehousingEntity>().ReverseMap();
        }
    }


}
