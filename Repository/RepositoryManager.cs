using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private Lazy<IClientRepository> _clientRepository;

        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _clientRepository = new Lazy<IClientRepository>(() => new ClientRepository(_context));
        }

        public IClientRepository Client => _clientRepository.Value;
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
