using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetMicroservice.Tests.Common
{
    public static class Test
    {
        private static Lazy<IConfiguration> _configuration = new Lazy<IConfiguration>(() =>
        {
            var configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("testsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            return configurationRoot;
        });

        public static string GetSetting(string name)
        {
            return _configuration.Value[name];
        }

        public static async Task<TResult> WaitForAsync<TResult>(Func<Task<TResult>> function, Func<TResult, bool> predicate)
        {
            for (int i = 0; i < 30; i++)
            {
                await Task.Delay(1000);

                var result = await function();
                if (predicate(result))
                {
                    return result;
                }
            }

            throw new Exception("The test timed out.");
        }

        public static async Task<TResult> WaitForAnyAsync<TResult>(Func<Task<TResult>> function)
        {
            return await WaitForAsync(function, r => r is IEnumerable enumerable && enumerable.OfType<object>().Any());
        }

        public static IWebApplication CreateWebApplication(string externalApiEndpointSettingName = "apiEndpoint")
        {
            var baseAddress = GetSetting(externalApiEndpointSettingName);
            if (!string.IsNullOrEmpty(baseAddress))
            {
                return new ExternalWebApplication(new Uri(baseAddress));
            }
            else
            {
                var factory = new WebApplicationFactory<Startup>();
                return new TestWebApplication<Startup>(factory);
            }
        }
    }
}
