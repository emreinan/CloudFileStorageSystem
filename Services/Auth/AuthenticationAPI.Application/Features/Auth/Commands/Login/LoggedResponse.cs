﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAPI.Application.Features.Auth.Commands.Login;

public class LoggedResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
