using CASHKY.LableSystem.ProductLable;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace CASHKY.LableSystem
{
    public static class ServiceEx
    {
        public static IServiceCollection AddLableServices(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<LableSystem.ProductLable.ProductLableView>();
            serviceDescriptors.AddScoped<LableSystem.ProductLable.ProductLableViewModel>();
            serviceDescriptors.AddDbContextFactory<ORM.LableDbContent>();
            serviceDescriptors.AddAutoMapper(Assembly.GetExecutingAssembly());

            return serviceDescriptors;
        }

    }


    public class LableProfile : AutoMapper.Profile
    {
        public LableProfile()
        {
            this.CreateMap<ProductLableEntity, ProductLableMappingModel>().ReverseMap();
            this.CreateMap<ProductLableEntity, ProductLableEntity>().ReverseMap();
            this.CreateMap<ProductLableEntity, ProductLableQueryModel>().ReverseMap();
        }
    }
}
