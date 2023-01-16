using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using TaxiApp.Data.Context;
using TaxiApp.Model;
using TaxiApp.Models.Abstract;

namespace TaxiApp.Services
{
    public class DriverRepoService<T> : IDriverRepoService<T> where T : Entity

    {
        private readonly IDataContext dataContext;
        private readonly IMongoCollection<T> collection;
        public DriverRepoService(IDataContext dataContext)
        {
            this.dataContext = dataContext;
            collection = dataContext.GetDatabase().GetCollection<T>(typeof(T).Name + "s");
        }

        public void Add(T entity)
        {
            if (entity.GetType() != typeof(BsonDocument))
            {
                entity.ToBsonDocument();
            }

            collection.InsertOne(entity);
        }

        public void AddList(List<T> entities)
        {
            foreach (var entity in entities)
            {
                collection.InsertOne(entity);
            }
        }

        //*
        public List<T> GetAll()
        {
            return collection.Find(new BsonDocument()).ToList();
        }

        public void Update(T entity)
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
            collection.ReplaceOne(filter, entity);
        }
    }
}
