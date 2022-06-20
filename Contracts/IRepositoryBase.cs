using System.Collections.Generic;

namespace Contracts
{
    public interface IRepositoryBase<T>
    {
        string TestMethodFromBase();

        string ClientsMethodFromBase();

        IEnumerable<T> GetAllRecords();

        string GetRecordById(int id);

        void CreateRecord();

        void RemoveRecord(int testbyId);

    }
}