﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScoutOnline.Core.Auth
{
    public interface IAuthenticationService
    {
        Task Login(string username, string password);
    }
}
