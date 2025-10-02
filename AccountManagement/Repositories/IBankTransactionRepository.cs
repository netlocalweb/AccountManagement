using AccountManagement.API.Models;
using AccountManagement.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Repositories
{
    public interface IBankTransactionRepository
    {
        Task<BankTransactionReadDto> AddTransactionAsync(BankTransactionCreateDto dto);
        Task<BankTransactionReadDto?> GetTransactionByIdAsync(int id);
        Task<IEnumerable<BankTransactionReadDto>> GetTransactionsByAccountAsync(int bankAccountId);
    }
}
