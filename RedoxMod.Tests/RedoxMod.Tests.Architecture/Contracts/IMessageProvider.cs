using RedoxMod.Architecture;

namespace RedoxMod.Tests.Architecture.Contracts
{
    public interface IMessageProvider: IService
    {
        void Greet();

        void Goodbye();
    }
}