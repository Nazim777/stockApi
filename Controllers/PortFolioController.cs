using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using stockApi.Extensions;
using stockApi.Interfaces;
using stockApi.Models;

namespace stockApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/portfolio")]
    public class PortFolioController:ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortFolioRepository _portFolioRepository;
        public PortFolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortFolioRepository portFolioRepository)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portFolioRepository = portFolioRepository;
            
        }

        [HttpGet]
        public async Task<IActionResult> GetPortfolio(){
            var userName = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(userName);
            var portfolio = await _portFolioRepository.GetUserPortFolio(appUser);
            return Ok(portfolio);
        }
        
    }
}