﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMetadataAPI.Core.Jwt;

public class TokenOptions
{
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public string SecurityKey { get; set; }
    public int AccessTokenExpiration { get; set; }
    public int RefreshTokenExpiration { get; set; }
}
