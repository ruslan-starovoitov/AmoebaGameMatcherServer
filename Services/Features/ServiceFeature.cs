using Microsoft.Extensions.DependencyInjection;

namespace Services.Features
{
    public abstract class ServiceFeature
    {
        public abstract void Add(IServiceCollection serviceCollection);
    }
}