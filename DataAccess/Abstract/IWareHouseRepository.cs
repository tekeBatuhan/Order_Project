
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Dtos;
using Entities.Concrete;
namespace DataAccess.Abstract
{
    public interface IWareHouseRepository : IEntityRepository<WareHouse>
    {
        Task<List<SelectionItem>> GetWareHousesLookUp();

    }
}