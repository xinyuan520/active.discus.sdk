using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.User.Application.Contracts.Services
{
    public interface IMessageService : IService
    {
        Task Publish();

    }
}
