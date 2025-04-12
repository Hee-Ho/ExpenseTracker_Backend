﻿using ExpenseTrackerAPI.Database;
using ExpenseTrackerAPI.DTOs;
using ExpenseTrackerAPI.Models;
using ExpenseTrackerAPI.Repo;
using ExpenseTrackerAPI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace ExpenseTrackerAPI.Controllers
{
    [Route("api/LoginAction")]
    [ApiController]
    public class LoginActionController : ControllerBase
    {
        private readonly PasswordHashing _hashing;
        private readonly UserRepo _userRepo;

        public LoginActionController(PasswordHashing hashing, UserRepo userRepo)
        {
            _hashing = hashing;
            _userRepo = userRepo;
        }

        [HttpGet("test")]
        public ActionResult Index()
        {
            return Ok();
        }

        /// <summary>
        /// New account registration
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [HttpPost("registation")]
        public async Task<IActionResult> registerUser([FromBody] NewAccountDto newUser)
        {
            UserAccount newAccount = CreateUserAccount(newUser);

            var (status, userID) = await _userRepo.CreateAccountAsync(newAccount);

            if (status == 1) 
            {
                return Created();
            }
            else
            {
                return BadRequest("Username or Email already exist");
            }
        }

        /// <summary>
        /// Account login authentication
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        public async Task<IActionResult> loginVerification([FromBody] LoginDto login)
        {
            var (status, uid, username, hashedpassword) = await _userRepo.UserLoginEmailAsync(login.Email);
            if (status < 1 || !_hashing.VerifyPassword(login.Password, hashedpassword))
            {
                return Unauthorized("Invalid Credential");
            }

            var userResponse = new LoginResponseDto
            {
                Id = uid,
                username = username
            };
            return Ok(userResponse);
        }

        //create useraccount from the given information
        private UserAccount CreateUserAccount(NewAccountDto newUserDto)
        {
            return new UserAccount
            {
                Username = newUserDto.Username,
                Email = newUserDto.Email,
                HashedPassword = _hashing.HashPassword(newUserDto.Password)
            };
        }

    }

}
