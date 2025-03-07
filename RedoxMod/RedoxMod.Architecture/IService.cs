using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RedoxMod.Architecture
{
    public interface IService
    {
        Task LoadServiceAsync();

        Task UnloadServiceAsync();
    }
}

           