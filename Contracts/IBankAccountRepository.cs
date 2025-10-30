using Entities.Models;

namespace Contracts
{
    public interface IBankAccountRepository : IRepositoryBase<BankAccount>
    {
        Task<IEnumerable<BankAccount>> GetAllBankAccountsAsync(bool trackchanges);
        Task<BankAccount?> GetBankAccountByIdAsync(int id ,bool trackchanges);
        Task<IEnumerable<BankAccount>> GetBankAccountByClientId(int clientId, bool trackchanges);
        //Kontrollon nqs kodi ekziston per nje klient te caktuar 
        Task<bool>CodeExistsForClientAsync(string code, int clientId );
        void CreateBankAccount(BankAccount bankAccount);
        void UpdateBankAccount(BankAccount bankAccount);

    }
}
