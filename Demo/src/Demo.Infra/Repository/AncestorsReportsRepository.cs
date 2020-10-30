﻿using Demo.Domain.Entities;
using Demo.Infra.Contracts.MongoDB;
using Demo.Infra.Contracts.Repository;
using Demo.Infra.Repository.Base;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Demo.Infra.Repository
{
    public class AncestorsReportsRepository
        : BaseRepository<AncestorsReport>
        , IAncestorsReportsRepository
    {
        public AncestorsReportsRepository(IMongoDBContext context)
            : base(context)
        {

        }

        public virtual async Task<AncestorsReport> GetAncestorsByIdAsync(string id)
            => (await DbSet.FindAsync(Builders<AncestorsReport>.Filter.Eq("_id", id))).FirstOrDefault();
    }
}
