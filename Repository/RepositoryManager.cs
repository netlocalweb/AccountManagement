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
        private Lazy<ICurrencyRepository> _currencyRepository;
        private Lazy<ICategoryRepository> _categoryRepository;
        private Lazy<IProductRepository> _productRepository;
        private Lazy<IBankAccountRepository> _bankAccountRepository;
        private Lazy<IBankTransactionRepository> _bankTransactionRepository;
        public RepositoryManager(RepositoryContext context)
        {
            _context = context;
            _clientRepository = new Lazy<IClientRepository>(() => new ClientRepository(_context));
            _currencyRepository = new Lazy<ICurrencyRepository>(() => new CurrencyRepository(_context));
            _categoryRepository = new Lazy<ICategoryRepository>(() => new CategoryRepository(_context));
            _productRepository = new Lazy<IProductRepository>(() => new ProductRepository(_context));
            _bankAccountRepository = new Lazy<IBankAccountRepository>(() => new BankAccountRepository(_context));
            _bankTransactionRepository = new Lazy<IBankTransactionRepository>>(() => new BankTransactionRepository(_context));
        }
        public IClientRepository Client => _clientRepository.Value;
        public ICurrencyRepository Currency => _currencyRepository.Value;
        public ICategoryRepository Category => _categoryRepository.Value;
        public IProductRepository Product => _productRepository.Value;
        public IBankAccountRepository BankAccount => _bankAccountRepository.Value;
        public IBankTransactionRepository BankTransaction => _bankTransactionRepository.Value;

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
