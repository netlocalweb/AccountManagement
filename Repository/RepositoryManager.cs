using Contracts;
using Entities;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ITestRepository _testRepository;
        private IClientRepository _clientRepository;
        private ICurrencyRepository _currencyRepository;
        private IBankAccountRepository _bankAccountRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public ITestRepository TestRepository
        {
            get
            {
                if (_testRepository == null)
                    _testRepository = new TestRepository(_repositoryContext);

                return _testRepository;
            }
        }

        public IClientRepository ClientRepository
        {
            get
            {
                if (_clientRepository == null)
                    _clientRepository = new ClientRepository(_repositoryContext);

                return _clientRepository;
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

        public IBankAccountRepository BankAccountRepository
        {
            get
            {
                if (_bankAccountRepository == null)
                    _bankAccountRepository = new BankAccountRepository(_repositoryContext);

                return _bankAccountRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _repositoryContext.SaveChangesAsync();
        }
    }
}