using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.SDK.Core.Interfaces
{
    public interface IDependencyRegistrar
    {
        public string Name { get; }

        public void AddService();
    }
}
