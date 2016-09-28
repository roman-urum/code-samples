using System.Configuration;
using HealthLibrary.Common;
using HealthLibrary.DomainLogic.Services.Implementations;
using HealthLibrary.DomainLogic.Services.Interfaces;
using HealthLibrary.Web.Api.Helpers;
using HealthLibrary.Web.Api.Helpers.Implementations;
using HealthLibrary.Web.Api.Helpers.Interfaces;
using HealthLibrary.Web.Api.Models.Mappings;
using HealthLibrary.Web.Api.Security;
using LightInject;
using StackExchange.Redis;
using WebApi.OutputCache.Core.Cache;

namespace HealthLibrary.Web.Api
{
    /// <summary>
    /// LightInject composition root for Api.
    /// </summary>
    public class WebApiCompositionRoot : ICompositionRoot
    {
        /// <summary>
        /// Composes services by adding services to the <paramref name="serviceRegistry" />.
        /// </summary>
        /// <param name="serviceRegistry">The target <see cref="T:LightInject.IServiceRegistry" />.</param>
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<CareElementRequestContext>(new PerScopeLifetime());
            serviceRegistry.Register<ICertificateAuthContext, DefaultCertificateAuthContext>(new PerScopeLifetime());
            serviceRegistry.Register<ICareElementContext>(factory => factory.GetInstance<CareElementRequestContext>());
            serviceRegistry.Register<ICareElementRequestContext>(factory => factory.GetInstance<CareElementRequestContext>());

            serviceRegistry.Register<ISelectionAnswerSetControllerHelper, SelectionAnswerSetsControllerHelper>();
            serviceRegistry.Register<IOpenEndedAnswerSetsControllerHelper, OpenEndedAnswerSetsControllerHelper>();
            serviceRegistry.Register<ITextMediaElementsControllerHelper, TextMediaElementsControllerHelper>();
            serviceRegistry.Register<IScaleAnswerSetControllerHelper, ScaleAnswerSetControllerHelper>();
            serviceRegistry.Register<IScaleAnswerSetService, ScaleAnswerSetService>();
            serviceRegistry.Register<IProtocolsControllerHelper, ProtocolsControllerHelper>();
            serviceRegistry.Register<IQuestionElementControllerHelper, QuestionElementControllerHelper>();
            serviceRegistry.Register<IProgramsControllerHelper, ProgramControllerHelper>();
            serviceRegistry.Register<IMeasurementElementsControllerHelper, MeasurementElementsControllerHelper>();
            serviceRegistry.Register<IMediaControllerHelper, MediaControllerHelper>();
            serviceRegistry.Register<ISearchControllerHelper, SearchControllerHelper>();
            serviceRegistry.Register<IGlobalSearchCacheHelper, GlobalSearchCacheHelper>();
            serviceRegistry.Register<ITagsControllerHelper, TagsControllerHelper>();
            serviceRegistry.Register<ITagsSearchCacheHelper, TagsSearchCacheHelper>();
            serviceRegistry.Register<IAssessmentElementsControllerHelper, AssessmentElementsControllerHelper>();

            serviceRegistry.Register<ICacheProvider, InMemoryCacheProvider>(new PerContainerLifetime());

            serviceRegistry.Register<ConnectionMultiplexer>(factory =>
                ConnectionMultiplexer.Connect(
                    ConfigurationManager.ConnectionStrings["RedisCache"].ConnectionString, null),
                new PerContainerLifetime());

            serviceRegistry.Register<IDatabase>(factory =>
                factory.GetInstance<ConnectionMultiplexer>().GetDatabase(-1, null));

            serviceRegistry.Register<IApiOutputCache, StackExchangeRedisOutputCacheProvider>();

            // Automapper mappings
            serviceRegistry.Register<ScaleAnswerSetMapping>();
            serviceRegistry.Register<SelectionAnswerSetMapping>();
            serviceRegistry.Register<TextMediaElementMapping>();
            serviceRegistry.Register<ProtocolMapping>();
            serviceRegistry.Register<TagMapping>();
            serviceRegistry.Register<MeasurementMapping>();
            serviceRegistry.Register<AssessmentMapping>();
            serviceRegistry.Register<CommonMapping>();
            serviceRegistry.Register<MediaMapping>();
            serviceRegistry.Register<OpenEndedAnswerSetMapping>();
            serviceRegistry.Register<QuestionElementMapping>();
            serviceRegistry.Register<ProgramMapping>();

            serviceRegistry.Register<IMediaFileHelper, MediaFileHelper>();
        }
    }
}