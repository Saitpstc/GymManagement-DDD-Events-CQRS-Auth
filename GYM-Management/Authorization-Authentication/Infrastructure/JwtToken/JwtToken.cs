﻿namespace Authorization_Authentication.Infrastructure.JwtToken;

public class JwtToken
{
    public JwtToken(string token, DateTime? expireDate)
    {
        Token = token;
        TokenExpireDate = expireDate;
    }

    public DateTime? TokenExpireDate { get; }
    public string Token { get; }
}