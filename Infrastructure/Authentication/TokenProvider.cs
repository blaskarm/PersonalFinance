using Application.Abstractions.Authentication;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication;

public class TokenProvider : ITokenProvider
{
    private readonly IConfiguration _configuration;
    private readonly JwtOptions _jwtOptions;

    public TokenProvider(IOptions<JwtOptions> options, IConfiguration configuration)
    {
        _jwtOptions = options.Value;
        _configuration = configuration;
    }

    public string Create(User user)
    {
        //string secretKey = _configuration["Jwt:SecretKey"]!;
        //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.UserRole.ToString())
            ]),
            //Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<int>("Jwt:ExpirationInMinutes")),
            Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationInMinutes),
            SigningCredentials = credentials,
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            //Issuer = _configuration["Jwt:Issuer"],
            //Audience = _configuration["Jwt:Audience"]
        };

        var handler = new JsonWebTokenHandler();

        string token = handler.CreateToken(tokenDescriptor);

        return token;
    }
}
