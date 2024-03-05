using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using stockApi.Dtos.Account;
using stockApi.Models;

namespace stockApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController:ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto){
            try{
                if(!ModelState.IsValid) return BadRequest(ModelState);
                var appUser = new AppUser{
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };
                var createdUser = await _userManager.CreateAsync(appUser,registerDto.Password);
                if(createdUser.Succeeded){
                    var roleResults = await _userManager.AddToRoleAsync(appUser,"User");
                    if(roleResults.Succeeded){
                        return Ok("User created");
                    }else{
                        return StatusCode(500,roleResults.Errors);
                    }

                }else{
                    return StatusCode(500,createdUser.Errors);
                } 

            }
            catch(Exception e){
                return StatusCode(500,e);

            }
        }
        
    }
}