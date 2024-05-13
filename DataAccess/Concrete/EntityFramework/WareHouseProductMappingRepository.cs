
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class WareHouseProductMappingRepository : EfEntityRepositoryBase<WareHouseProductMapping, ProjectDbContext>, IWareHouseProductMappingRepository
    {
        public WareHouseProductMappingRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
