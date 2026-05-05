using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await RepositoryContext.Clients
                .Where(c => c.IsActive == true)
                .OrderBy(c => c.Id)
                .ToListAsync();
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            return await RepositoryContext.Clients
                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive == true);
        }

        public async Task<Client> GetClientByEmailAsync(string email)
        {
            return await RepositoryContext.Clients
                .FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Client> GetClientByPhoneAsync(string phone)
        {
            return await RepositoryContext.Clients
                .FirstOrDefaultAsync(c => c.Phone == phone);
        }

        public async Task<Client> GetClientByUsernameAsync(string username)
        {
            return await RepositoryContext.Clients
                .FirstOrDefaultAsync(c => c.Username == username);
        }

        public void CreateClient(Client client)
        {
            RepositoryContext.Clients.Add(client);
        }

        public void UpdateClient(Client client)
        {
            RepositoryContext.Clients.Update(client);
        }

        public void DeleteClient(Client client)
        {
            client.IsActive = false;
            client.DateModified = DateTime.Now;

            RepositoryContext.Clients.Update(client);
        }
    }
}