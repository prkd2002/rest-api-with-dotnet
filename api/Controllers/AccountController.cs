using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Interfaces;
using api.Models;
using api.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController:ControllerBase
    {
        private readonly UserManager<AppUser> _userMananger;
        private readonly ItokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, ItokenService itokenService, SignInManager<AppUser> signInManager)
        {
            _userMananger = userManager;
            _tokenService = itokenService;
            _signInManager = signInManager; 
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto )
        {
            try{
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);
                var appUser = new AppUser 
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                    
                };

                var createdUser = await _userMananger.CreateAsync(appUser,registerDto.Password );
                if(createdUser.Succeeded)
                {
                    var roleResult = await _userMananger.AddToRoleAsync(appUser, "User");
                    if(roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto{
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else 
                {
                    return StatusCode(500, createdUser.Errors);

                }

            }catch(Exception e)
            {
                return StatusCode(500, e);

            }
            
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _userMananger.Users.FirstOrDefaultAsync(user => user.UserName.ToLower() == loginDto.UserName.ToLower());

            if(user == null)
            {
                return Unauthorized("Invalid Username !");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if(!result.Succeeded) return Unauthorized("Username not found and/or Password Incorrect !");
            
            return Ok(
                 new NewUserDto {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                 }
            );
        }
    }
}