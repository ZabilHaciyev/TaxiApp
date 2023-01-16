using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using TaxiApp.Data.Context;
using TaxiApp.Model;
using TaxiApp.Models.Abstract;

namespace TaxiApp.Services
{
    public class UserRepoService<T> : IUserRepoService<T> where T : Entity
    {
        //*
        private readonly IDataContext dataContext;
        private readonly IMongoCollection<T> collection;
        public UserRepoService(IDataContext dataContext)
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

        public T Get(string mail)
        {
            var filter = Builders<T>.Filter.Eq("Email", mail);

            var jResults = collection.Find(filter).FirstOrDefault().ToJson();

            return BsonSerializer.Deserialize<T>(jResults);

        }

        public void Update(T entity)
        {
            var filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
            collection.ReplaceOne(filter, entity);
        }
    }
}
