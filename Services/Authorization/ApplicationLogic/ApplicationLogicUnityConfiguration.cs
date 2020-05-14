﻿using ApplicationLogic.AppUserCommands;
using ApplicationLogic.AppUserQueries;
using ApplicationLogic.UserConfirmations;
using CommonLibrary;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Text;
using Unity;

namespace ApplicationLogic
{
    public class ApplicationLogicUnityConfiguration : IUnityConfigueration
    {
        public static IUnityContainer Container { get; private set; }
        public void ConfigUnity(IUnityContainer container)
        {
            Container = container;

            Container.RegisterType<IRegisterUserValidator, RegisterUserValidator>();
            Container.RegisterType<IUserRegisterer, RegisterUser>();
            Container.RegisterType<UserLoginManager>();
            Container.RegisterType<ILoginChecker, LoginChecker>();
            Container.RegisterType<IUserService, UserGetter>();
            Container.RegisterType<IEmailConfirmationManager, EmailConfirmation>();
            Container.RegisterType<IStringGenerator, StringGenerator>();


            new DalUnityConfiguration().ConfigUnity(container);
        }
    }
}
