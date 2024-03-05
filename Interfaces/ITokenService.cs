using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using stockApi.Models;

namespace stockApi.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}