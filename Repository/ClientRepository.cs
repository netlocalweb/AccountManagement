using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    internal sealed class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
       
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .Include(c => c.User)
            .ToListAsync();

        public async Task<Client?> GetClientByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(id), trackChanges)
            .Include(c => c.User)
            .SingleOrDefaultAsync();

        public void CreateClient(Client client) => Create(client);
        public void UpdateClient(Client client) => Update(client);
        public void DeleteClient(Client client) => Delete(client);
       

    }
}
