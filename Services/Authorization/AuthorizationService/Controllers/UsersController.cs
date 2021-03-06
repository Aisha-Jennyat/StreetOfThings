﻿using ApplicationLogic.AppUserQueries;
using CommonLibrary;
using HostingHelpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationService.Controllers
{
    [Route("api/Users")]
    public class UsersController : MyControllerBase
    {
        IUserGetter _userGetter;

        public UsersController  (IUserGetter userGetter)
        {
            _userGetter = userGetter;
        }

        [Route("listFromIds")]
        [HttpPost]
        public async Task<IActionResult> GetUsersByIds([FromBody]string[] usersIds)
        {
            var users = _userGetter.GetUserByIds(usersIds.ToList());
            return Ok(users);
        }

        [Route("list")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            List<UserForAdministrationDto> users = await _userGetter.GetUsersAsync();
            return Ok(users);
        }

        [Route("V1.1/list")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers1()
        {
            var usersWithStats = await _userGetter.GetUsersWithStatsAsync();
            return Ok(usersWithStats);
        }


    }
}
