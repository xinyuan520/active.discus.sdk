using Discus.SDK.MongoDB.Repository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.User.Application.Services
{
    public class MongoService : BasicService, IMongoService
    {
        private readonly IMongoRepository _mongoRepository;

        public MongoService(IMongoRepository mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public void GetAsync()
        {
            _mongoRepository.GetAsync();
        }
    }
}
