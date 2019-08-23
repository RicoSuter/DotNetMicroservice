using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetMicroservice.Tests
{
    public static class Test
    {
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
    }
}
