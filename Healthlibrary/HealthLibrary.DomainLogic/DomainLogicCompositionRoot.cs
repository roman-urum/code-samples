using HealthLibrary.ApiAccess;
using HealthLibrary.DataAccess;
using HealthLibrary.DomainLogic.Services.Implementations;
using HealthLibrary.DomainLogic.Services.Interfaces;
using LightInject;

namespace HealthLibrary.DomainLogic
{
    public class DomainLogicCompositionRoot : ICompositionRoot
    {
        /// <summary>
        /// Composes services by adding services to the <paramref name="serviceRegistry" />.
        /// </summary>
        /// <param name="serviceRegistry">The target <see cref="T:LightInject.IServiceRegistry" />.</param>
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterFrom<DataAccessCompositionRoot>();
            serviceRegistry.RegisterFrom<ApiAccessCompositionRoot>();

            serviceRegistry.Register<ITokenService, TokenService>();
            serviceRegistry.Register<ISelectionAnswerSetService, SelectionAnswerSetService>();
            serviceRegistry.Register<IOpenEndedAnswerSetsService, OpenEndedAnswerSetsService>();
            serviceRegistry.Register<ITextMediaElementsService, TextMediaElementsService>();
            serviceRegistry.Register<ITagsService, TagsService>();
            serviceRegistry.Register<IMediaService, MediaService>();
            serviceRegistry.Register<IProtocolService, ProtocolService>();
            serviceRegistry.Register<IQuestionElementService, QuestionElementService>();
            serviceRegistry.Register<IProgramService, ProgramService>();
            serviceRegistry.Register<IMeasurementElementsService, MeasurementElementsService>();
            serviceRegistry.Register<IAssessmentElementsService, AssessmentElementsService>();
        }
    }
}
