using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.SDK.Core.Interfaces
{
    public interface IServiceInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// 
        /// </summary>
        public string ServiceName { get; }

        /// <summary>
        /// 
        /// </summary>
        public string AppAssemblyName { get; }

        /// <summary>
        /// 
        /// </summary>
        public string CorsPolicy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShortName { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Version { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// 
        /// </summary>
        public Assembly StartAssembly { get; }
    }
}
