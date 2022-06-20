using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IDapperRepository
    {
        IEnumerable<Clients> GetAll();
        Clients GetById(int id);
    }
}
