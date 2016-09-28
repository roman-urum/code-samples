using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using AutoMapper;
using CustomerService.Common;
using CustomerService.DomainLogic;
using CustomerService.Web.Api.Models.Mapping;
using LightInject;
using LightInject.ServiceLocation;
using Microsoft.Practices.ServiceLocation;

namespace CustomerService.Web.Api
{
    using Isg.EntityFramework.Interceptors;

    /// <summary>
    /// WebApiApplication.
    /// </summary>
    public class WebApiApplication : HttpApplication
    {
        /// <summary>
        /// Application_s the start.
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            RegisterCompositionRoot();

            InterceptorProvider.SetInterceptorProvider(ServiceLocator.Current.GetInstance<IInterceptorProvider>());

            AutomapperConfig.RegisterRules();

            AreaRegistration.RegisterAllAreas();

            CreateDirectories();
        }

        private void CreateDirectories()
        {
            Directory.CreateDirectory(HttpContext.Current.Server
                .MapPath(Settings.UploadImagesFolder));
        }

        private void RegisterCompositionRoot()
        {
            var serviceContainer = new ServiceContainer();

            serviceContainer.RegisterApiControllers();
            serviceContainer.EnablePerWebRequestScope();
            serviceContainer.EnableWebApi(GlobalConfiguration.Configuration);

            serviceContainer.RegisterFrom<WebApiCompositionRoot>();

            ServiceLocator.SetLocatorProvider(() => new LightInjectServiceLocator(serviceContainer));
        }
    }
}