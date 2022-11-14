using System;
using MagicOnion;
using MagicOnion.Server;
using FallingNumberTask.Shared.Services;

namespace FallingNumberTask.Services
{
    // Implements RPC service in the server project.
    // The implementation class must inehrit `ServiceBase<IMyFirstService>` and `IMyFirstService`
    public class MyFirstService : ServiceBase<ITaskDataService>, ITaskDataService
    {
        // `UnaryResult<T>` allows the method to be treated as `async` method.
        public async UnaryResult<int> SumAsync(int x, int y)
        {
            Console.WriteLine($"Received:{x}, {y}");
            return x + y;
        }
    }
}