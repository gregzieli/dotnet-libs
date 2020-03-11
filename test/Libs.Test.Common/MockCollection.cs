using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;

namespace Libs.Test.Abstractions
{
    public abstract class MockCollection : IMockCollection
    {
        public MockCollection()
        {
            CreateDefaultMocks();
        }

        public void ClearInvocations()
        {
            IterateThroughMocks(mock => mock.Invocations.Clear());
        }

        public void Register(IServiceCollection services)
        {
            IterateThroughMocks(mock =>
            {
                var mockedType = mock.GetType().GetGenericArguments()[0];

                services.AddTransient(mockedType, x => mock.Object);
            });
        }

        protected abstract void CreateDefaultMocks();

        private void IterateThroughMocks(Action<Mock> fx)
        {
            foreach (var propInfo in GetType().GetProperties())
            {
                fx.Invoke((Mock)propInfo.GetValue(this));
            }
        }
    }
}
