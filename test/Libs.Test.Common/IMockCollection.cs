using Microsoft.Extensions.DependencyInjection;

namespace Libs.Test.Abstractions
{
    public interface IMockCollection
    {
        /// <summary>
        /// Resets all invocations recorded for the mocks in the collection.
        /// </summary>
        void ClearInvocations();

        /// <summary>
        /// Registers all mocks in the container.
        /// </summary>
        void Register(IServiceCollection services);
    }
}
