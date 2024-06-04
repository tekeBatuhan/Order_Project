
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.DataAccess;
using Core.Entities.Dtos;
using Entities.Concrete;
namespace DataAccess.Abstract
{
    public interface IColorRepository : IEntityRepository<Color>
    {
        Task<List<SelectionItem>> GetColorsLookUp();
    }
}