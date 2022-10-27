using SimpleWebApi.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWebApi.Tests.Context
{
    public abstract class TestBase : IDisposable
    {
        protected readonly DatabaseContext Context;

        public TestBase()
        {
            Context = ContextFactory.Create();
        }

        public void Dispose()
        {
            ContextFactory.Destroy(Context);
        }
    }
}
