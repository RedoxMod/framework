using RedoxMod.Architecture;

namespace RedoxMod.Tests.Architecture.Contracts
{
    public interface ILogger : IService
    {
        void Log(string message);
    }
}