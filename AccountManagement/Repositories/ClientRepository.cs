using AccountManagement.Data;
using AccountManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext dbContext;
        public ClientRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await dbContext.Clients.AsNoTracking().ToListAsync();
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            return await dbContext.Clients.FindAsync(id);
        }

        public async Task<Client> GetByEmailAsync(string email)
        {
            return await dbContext.Clients.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Client> GetByUsernameAsync(string username)
        {
            return await dbContext.Clients.FirstOrDefaultAsync(c => c.Username == username );
        }

        public async Task<Client?> GetByPhoneAsync(string phone)
        {
            return await dbContext.Clients.FirstOrDefaultAsync(c => c.Phone == phone);
        }

        public async Task<Client> CreateAsync(Client client)
        {
            dbContext.Clients.Add(client);
            await dbContext.SaveChangesAsync();
            return client;
        }

        public async Task UpdateAsync(Client client)
        {
            dbContext.Clients.Update(client);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Client client)
        {
            dbContext.Clients.Remove(client);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Client> AddAsync(Client client)
        {
            dbContext.Clients.Add(client);
            await dbContext.SaveChangesAsync();
            return client;
        }
    }

}
