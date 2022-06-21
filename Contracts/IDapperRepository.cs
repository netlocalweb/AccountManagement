using Entities.DTO;
using Entities.Models;
using System.Collections.Generic;

namespace Contracts
{
    public interface IDapperRepository
    {
        IEnumerable<CustomFirstApiDTO> FirstAPI();

        IEnumerable<CustomSecondApiDTO> SecondApi(int id);

        IEnumerable<CustomThirdApiDTO> ThirdApi(int id);

        IEnumerable<CustomFourthApiDTO> FourthAPI(int id);


    }
}
