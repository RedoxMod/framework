using Jint.Native.Function;

namespace RedoxMod.Core.Plugins
{
    public sealed class RegisteredHook
    {   
        public string Name { get; }

        public Function Function { get; }

        public RegisteredHook(string name, Function function)
        {
            Name = name;
            Function = function;
        }
    }
}