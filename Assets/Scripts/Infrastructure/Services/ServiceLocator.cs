using System;
using System.Collections.Generic;

namespace TowerDefense.Infrastructure
{
    public sealed class ServiceLocator
    {
        private readonly Dictionary<Type, IService> services = new Dictionary<Type, IService>();

        public void Register<TService>(TService service) where TService : class, IService
        {
            var type = typeof(TService);
            if (!services.ContainsKey(type))
                services[type] = service;
        }

        public TService Release<TService>() where TService : class, IService
        {
            var type = typeof(TService);
            if (services.TryGetValue(type, out var service))
                return service as TService;

            throw new InvalidOperationException($"Doesn't have {typeof(TService)}");
        }
    }
}
