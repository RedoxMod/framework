using System;
using System.Collections.Generic;
using System.Text;

namespace RedoxMod.API.Configuration
{
    public interface IConfigurable
    {
        IConfiguration Configuration { get; }
    }
}
