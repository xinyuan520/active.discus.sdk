using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.SDK.Redis.Configurations
{
    /// <summary>
    /// Defines an endpoint.
    /// </summary>
    public class ServerEndPoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Adnc.Infra.Redis.Configurations.ServerEndPoint"/> class.
        /// </summary>
        /// <param name="host">Host.</param>
        /// <param name="port">Port.</param>
        public ServerEndPoint(string host, int port)
        {
            Host = host;
            Port = port;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Adnc.Infra.Redis.Core.Configurations.ServerEndPoint"/> class.
        /// </summary>
        public ServerEndPoint()
        {
        }

        /// <summary>
        /// Gets or sets the port.
        /// </summary>
        /// <value>The port.</value>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>The host.</value>
        public string Host { get; set; }
    }
}
