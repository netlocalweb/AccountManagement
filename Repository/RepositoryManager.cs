using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ITestRepository _testRepository;

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
    }
}