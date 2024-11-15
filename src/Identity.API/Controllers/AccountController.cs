﻿using Identity.API.Dtos;
using Identity.API.Interfaces;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Trace;

namespace Identity.API.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender<ApplicationUser> _emailSender;
        public AccountController(
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender<ApplicationUser> emailSender
            )
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email.ToLower());

            if (user == null)
            {
                return Unauthorized("Invalid email!");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized("Email not found and/or password incorrect");
            }
            else
            {
                return Ok(
                        new NewUserDto
                        {
                            UserName = user.UserName,
                            Email = user.Email,
                            Token = _tokenService.CreateToken(user)
                        }
                    );
            }

        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new ApplicationUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.EmailAddress,
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                                new NewUserDto
                                {
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
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto){
            try{
                if(ModelState.IsValid){
                    var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == forgotPasswordDto.Email.ToLower());

                    if(user != null && await _userManager.IsEmailConfirmedAsync(user)){
                        await SendForgotPasswordEmail(user.Email, user);

                        return Ok("Password reset link has been sent");
                    }

                    //aby uniknąć enumeracji/brute force
                    return Ok("Password reset link has been sent");
                }

                return BadRequest(ModelState);

            }catch(Exception ex){
                return StatusCode(500, ex);
            }
        }



        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto){
            try
            {
                if(ModelState.IsValid){
                    var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == resetPasswordDto.Email.ToLower());

                    if(user != null){
                        var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);

                        if(result.Succeeded){
                            return Ok("Password has been reset");
                        }

                        foreach(var error in result.Errors){
                            ModelState.AddModelError("", error.Description);
                        }
                        return BadRequest(ModelState);

                    }
                    //aby uniknąć enumeracji/brute force
                    return Ok("Password has been reset");
                }
                return BadRequest(ModelState);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        private async Task SendForgotPasswordEmail(string? email, ApplicationUser? user){
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            //TODO:
            var angularAppBaseUrl = "";

            // Construct the URL to the Angular reset password page
            var passwordResetLink = $"{angularAppBaseUrl}/reset-password?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(token)}";  

            await _emailSender.SendPasswordResetLinkAsync(user, email, passwordResetLink);

        }
    }
}
