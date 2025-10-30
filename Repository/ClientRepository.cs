using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    internal sealed class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }
        //Merr te gjithe klientet 
        public async Task<IEnumerable<Client>> GetAllClientsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .Include(c => c.User)
            .ToListAsync();
        //Merr klientin sipas id
        public async Task<Client?> GetClientByIdAsync(int id, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(id), trackChanges)
            .Include(c => c.User)
            .SingleOrDefaultAsync();

        public void CreateClient(Client client) => Create(client);
        public void UpdateClient(Client client) => Update(client);

        //bllokon User ne vend që ta fshije
        public void DeleteClient(Client client)
        {
            if (client.User != null)
            {
                client.User.IsLockedOut = true;
                client.User.LockoutDate = DateTime.UtcNow;
            }
            Update(client);
        }
        //Merr klientin sipas userId
        public async Task<Client?> GetClientByUserIdAsync(string userId, bool trackChanges)
        {
            return await FindByCondition(c => c.UserId.Equals(userId), trackChanges)
                        .Include(c => c.User)
                        .SingleOrDefaultAsync();
        }
    }
}
