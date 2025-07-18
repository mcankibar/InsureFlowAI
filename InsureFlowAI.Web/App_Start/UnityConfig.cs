using System.Web.Mvc;
using InsureFlowAI.BLL.Concrete;
using InsureFlowAI.BLL.Services;
using InsureFlowAI.DAL.Abstract;
using InsureFlowAI.DAL.EntityFramework;
using Unity;
using Unity.Mvc5;

namespace InsureFlowAI.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            
            container.RegisterType<ICarouselService, CarouselManager>();
            container.RegisterType<ICarouselDal, EfCarouselDal>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}