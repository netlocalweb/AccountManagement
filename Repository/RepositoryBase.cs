using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext;

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            RepositoryContext = repositoryContext;
        }

        public string TestMethodFromBase()
        {
            return "This is a test method from RepositoryBase";
        }

        public string ClientsMethodFromBase()
        {
            return "This is a Clients method form RepositoryBase";
        }

        //Method that reads all database objects - READ ALL
        public IEnumerable<T> GetAllRecords()
        {
            var testAll = RepositoryContext.Clients;
            return (IEnumerable<T>)testAll;
        }

        //Method that reads database object by Id - READ BY ID
        public string GetRecordById(int tmp)
        {
            var testbyId = RepositoryContext.Clients.Where(x => x.Id == tmp).FirstOrDefault();
            return testbyId.ToString();
        }


        //Method to add a new object in database - CREATE
        public void CreateRecord()
        {
            RepositoryContext.Clients.Add(new Entities.Models.Clients());
        }

        //Method to remove an object by id from database - DELETE
        public void RemoveRecord(int testbyId)
        {
            var test = RepositoryContext.Clients.Where(x => x.Id == testbyId).FirstOrDefault();

            RepositoryContext.Clients.Remove(test);
        }
    }
}
