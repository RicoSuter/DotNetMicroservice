using System;
using System.Net.Http;

namespace DotNetMicroservice.Tests.Common
{
    public interface IWebApplication : IDisposable
    {
        HttpClient CreateClient();
    }
}
