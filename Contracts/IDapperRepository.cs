using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IDapperRepository
    {
        IEnumerable<TestEntity> GetAll();
        TestEntity GetById(int id);
    }
}
