using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly RepositoryContext _repositoryContext;

        public ClientRepository(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void Create(Client client)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var client = _repositoryContext.Clients.Find(id);
            if (client != null)
            {
                _repositoryContext.Clients.Remove(client);
                _repositoryContext.SaveChanges();
            }
        }

        public ICollection<Client> FindAll()
        {
            return _repositoryContext.Clients.ToList();
        }

        public Client FindById(int id)
        {
            return _repositoryContext.Clients.Find(id);
        }

        public bool Update(Client entity)
        {
            var existingClient = _repositoryContext.Clients.Find(entity.Id);

            // Update the DateModified
            existingClient.DateModified = DateTime.Now;

            //whatever to-change properties do i shof me von

            // Save the changes
            _repositoryContext.SaveChanges();
            return true;
        }

    }
}
