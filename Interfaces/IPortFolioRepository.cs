using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using stockApi.Models;

namespace stockApi.Interfaces
{
    public interface IPortFolioRepository
    {
         Task<List<Stock>>GetUserPortFolio(AppUser user);
         Task<Portfolio> CreateAsync(Portfolio portfolio);
    }
}