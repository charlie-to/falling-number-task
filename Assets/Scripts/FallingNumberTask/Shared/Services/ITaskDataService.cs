using System;
using MagicOnion;

namespace FallingNumberTask.Shared.Services
{
    // Defines .NET interface as a Server/Client IDL.
    // The interface is shared between server and client.
    public interface ITaskDataService : IService<ITaskDataService>
    {
        // The return type must be `UnaryResult<T>`.
        UnaryResult<int> SumAsync(int x, int y);
    }
}
