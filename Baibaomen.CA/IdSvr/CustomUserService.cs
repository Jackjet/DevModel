﻿/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Baibaomen.CA.AspId;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.AspNetIdentity;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;

namespace Baibaomen.CA.IdSvr
{
    public static class CustomUserServiceExtensions
    {
        public static void ConfigureCustomUserService(this IdentityServerServiceFactory factory, string connString)
        {
            factory.UserService = new Registration<IUserService, CustomUserService>();
            factory.Register(new Registration<CustomUserManager>());
            factory.Register(new Registration<CustomUserStore>());
            factory.Register(new Registration<CustomContext>(resolver => new CustomContext(connString)));
        }
    }

    public class CustomUserService : AspNetIdentityUserService<CustomUser, int>
    {
        public CustomUserService(CustomUserManager userMgr)
            : base(userMgr)
        {
        }
    }
}