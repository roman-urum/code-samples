using LightInject;

namespace VitalsService.DataAccess.Document
{
    public class DocumentCompositionRoot : ICompositionRoot
    {
        /// <summary>
        /// Initializes implementations for data access/ef layer.
        /// </summary>
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IDocumentRepositoryFactory, DocumentRepositoryFactory>();
        }
    }
}