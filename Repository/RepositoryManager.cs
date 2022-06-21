using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private IClientsRepository _clientsRepository;
        private ICurrencyRepository _currencyRepository;
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;
        private IBankAccountRepository _bankAccountRepository;
       
        

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        
        public IClientsRepository ClientsRepository
        {
            get
            {
                if (_clientsRepository == null)
                    _clientsRepository = new ClientsRepository(_repositoryContext);

                return _clientsRepository;
            }
        }

        public ICurrencyRepository CurrencyRepository
        {
            get
            {
                if (_currencyRepository == null)
                    _currencyRepository = new CurrencyRepository(_repositoryContext);

                return _currencyRepository;
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(_repositoryContext);

                return _categoryRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_repositoryContext);

                return _productRepository;
            }
        }

        public IBankAccountRepository BankAccountRepository
        {
            get
            {
                if (_bankAccountRepository == null)
                    _bankAccountRepository = new BankAccountRepository(_repositoryContext);

                return _bankAccountRepository;
            }
        }


    }
}