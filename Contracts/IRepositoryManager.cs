using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ITestRepository TestRepository { get; }

        IClientRepository ClientRepository { get; }

        Task SaveAsync();
    }
}