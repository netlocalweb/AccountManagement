using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class TestRepository : RepositoryBase<TestEntity>, ITestRepository
    {
        public TestRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public string TestMethod()
        {
            return "This is a test method from TestRepository";
        }
    }
}