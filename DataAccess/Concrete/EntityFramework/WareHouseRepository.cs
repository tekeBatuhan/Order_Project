
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using Core.Entities.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace DataAccess.Concrete.EntityFramework
{
    public class WareHouseRepository : EfEntityRepositoryBase<WareHouse, ProjectDbContext>, IWareHouseRepository
    {
        public WareHouseRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<List<SelectionItem>> GetWareHousesLookUp()
        {
            var lookUp = await (from entity in Context.WareHouses
                                select new SelectionItem()
                                {
                                    Id = entity.Id,
                                    Label = entity.Name
                                }).ToListAsync();
            return lookUp;
        }
    }
}
