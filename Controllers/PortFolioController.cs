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
        [HttpPost]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepository.GetBySymbolAsync(symbol);

            if (stock == null) return BadRequest("Stock not found");

            var userPortfolio = await _portFolioRepository.GetUserPortFolio(appUser);

            if (userPortfolio.Any(e => e.Symbol.ToLower() == symbol.ToLower())) return BadRequest("Cannot add same stock to portfolio");

            var portfolioModel = new Portfolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id
            };

            await _portFolioRepository.CreateAsync(portfolioModel);

            if (portfolioModel == null)
            {
                return StatusCode(500, "Could not create");
            }
            else
            {
                return Ok("Portfolio Created!");
            }
        }

        [HttpDelete]
        public async Task<IActionResult>DeletePortfolio(string symbol){
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio =  await _portFolioRepository.GetUserPortFolio(appUser);
             var filteredStock = userPortfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower()).ToList();

            if (filteredStock.Count() == 1)
            {
                await _portFolioRepository.DeletePortfolio(appUser, symbol);
            }
            else
            {
                return BadRequest("Stock not in your portfolio");
            }

            return Ok();




        }


    }
}