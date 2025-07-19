using InsureFlowAI.BLL.Abstract;
using InsureFlowAI.BLL.Concrete;
using InsureFlowAI.BLL.Services;
using InsureFlowAI.DAL.Abstract;
using InsureFlowAI.DAL.EntityFramework;
using InsureFlowAI.Web.Controllers;
using System.Net.Http;
using System.Web.Mvc;
using Unity;
using Unity.Lifetime;
using Unity.Mvc5;

namespace InsureFlowAI.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            
            container.RegisterType<HttpClient>(new ContainerControlledLifetimeManager());
            
            container.RegisterType<SocialMediaController>();
            container.RegisterType<CultureController>();
            
            container.RegisterType<ICarouselService, CarouselManager>();
            container.RegisterType<ICarouselDal, EfCarouselDal>();

            container.RegisterType<IServicesService, ServicesManager>();
            container.RegisterType<IServicesDal, EfServicesDal>();

            container.RegisterType<IFAQService, FAQManager>();
            container.RegisterType<IFAQDal, EfFAQDal>();

            
            container.RegisterType<IAIImageGenerationService, AIImageGenerationManager>();
            container.RegisterType<IAIFAQGenerationService, AIFAQGenerationManager>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}