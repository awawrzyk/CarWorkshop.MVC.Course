using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Infrastructure
{
    public interface ICarWorkshopRepository
    {
        Task Create(Domain.Entitites.CarWorkshop carWorkshop);
        Task<Domain.Entitites.CarWorkshop?> GetByName(string name);
        Task<IEnumerable<Domain.Entitites.CarWorkshop>> GetAll();
        Task<Domain.Entitites.CarWorkshop> GetByEncodedName(string encodedName);
        Task Commit();
    }
}
