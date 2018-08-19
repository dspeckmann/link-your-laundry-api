﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LinkYourLaundry.Services;
using LinkYourLaundry.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LinkYourLaundry.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserService userService;

        public AuthenticationController(UserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        public IActionResult Post([FromBody] LoginViewModel viewModel)
        {
            var token = userService.Login(viewModel);
            if (token != null)
            {
                return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token) });
            }
            else
            {
                return Forbid();
            }
        }
    }
}