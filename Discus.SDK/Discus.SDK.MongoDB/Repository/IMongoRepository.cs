﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discus.SDK.MongoDB.Repository
{
    public interface IMongoRepository
    {
        Task<BsonDocument> GetAsync();
    }
}
