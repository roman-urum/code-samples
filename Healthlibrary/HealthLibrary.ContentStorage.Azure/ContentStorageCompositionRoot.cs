﻿using HealthLibrary.ContentStorage.Azure.Services.Implementations;
using HealthLibrary.ContentStorage.Azure.Services.Interfaces;
using LightInject;

namespace HealthLibrary.ContentStorage.Azure
{
    public class ContentStorageCompositionRoot : ICompositionRoot
    {
        /// <summary>
        /// Composes services by adding services to the <paramref name="serviceRegistry" />.
        /// </summary>
        /// <param name="serviceRegistry">The target <see cref="T:LightInject.IServiceRegistry" />.</param>
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IContentStorage, Services.Implementations.ContentStorage>(new PerScopeLifetime());
            serviceRegistry.Register<IThumbnailService, ThumbnailService>();
        }
    }
}