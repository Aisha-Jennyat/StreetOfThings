﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Transaction.DataAccessLayer;
using Transaction.Models;

namespace Transaction.BusinessLogic.Infrastructure
{
    class UserDataManager
    {
        private IRepository<Guid, User> _userRepo;

        private IRepository<Guid, Login> _loginRepo;

        private UserDataGetter _userDataGetter;

        private CurrentUserCredentialsGetter _credentialsGetter;

        public UserDataManager(IRepository<Guid, User> userRepo,
            IRepository<Guid, Login> loginRepo,
            UserDataGetter userDataGetter,
            CurrentUserCredentialsGetter credentialsGetter)
        {
            _userRepo = userRepo;
            _loginRepo = loginRepo;
            _userDataGetter = userDataGetter;
            _credentialsGetter = credentialsGetter;
        }

        public async Task<(Login, User)> AddUserIfNotExisted(string tokenId, string originUserId, string accessToken)
        {
            var usersById = from u in _userRepo.Table
                            where u.UserId == Guid.Parse(originUserId)
                            select u;

            if (!usersById.Any())
            {
                var userDto = await _userDataGetter.GetUserDataByToken(accessToken);

                if (userDto == null)
                {
                    return (null, null);
                }
                var userToAdd = new User
                {
                    UserId = Guid.Parse(originUserId),
                    UserName = userDto.UserName,
                    Logins = new List<Login>
                    {
                        new Login
                        {
                            TokenId = userDto.TokenId,
                            LoginId = Guid.NewGuid()
                        }
                    }
                };

                _userRepo.Add(userToAdd);
                await _userRepo.SaveChangesAsync();
                return (userToAdd.Logins.FirstOrDefault(), userToAdd);
            }
            else
            {
                usersById = usersById.Include(u => u.Logins);
                var theUser = usersById.FirstOrDefault();

                // if The user existed but the login does not exist
                if (!theUser.Logins.Any(t => t.TokenId == tokenId))
                {
                    // add the login 
                    var login = new Login
                    {
                        UserId = theUser.UserId,
                        TokenId = tokenId,
                        Token = accessToken,
                    };
                    _loginRepo.Add(login);
                    await _loginRepo.SaveChangesAsync();
                    return (login, theUser);
                }
                else
                {
                    // the user and the login does exists 
                    return (theUser.Logins.FirstOrDefault(), theUser);
                }
            }
        }


        public async Task<User> AddUserIfNotExisted(string originUserId)
        {
            var usersById = from u in _userRepo.Table
                            where u.UserId == Guid.Parse(originUserId)
                            select u;

            if (!usersById.Any())
            {
                var userDto = await _userDataGetter.GetUserDataById(originUserId);

                if (userDto == null)
                {
                    return (null);
                }
                var userToAdd = new User
                {

                    UserId = Guid.Parse(originUserId),
                    UserName = userDto.UserName,
                    Logins = new List<Login>
                    {
                        new Login
                        {
                            TokenId = userDto.TokenId,
                            LoginId = Guid.NewGuid()
                        }
                    }
                };

                _userRepo.Add(userToAdd);
                await _userRepo.SaveChangesAsync();
                return (userToAdd);
            }
            else
            {
                var theUser = usersById.FirstOrDefault();
                return theUser;
            }
        }

        public async Task<(Login Login, User User)> AddCurrentUserIfNeeded()
        {
            var credentials = _credentialsGetter.GetCuurentUser();
            if (credentials is null)
            {
                return (null, null);
            }
            else
            {
                return await AddUserIfNotExisted(credentials.TokenId, credentials.UserId, credentials.AccessToken);
            }
        }

    }
}
