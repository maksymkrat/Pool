using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Pool.API.Services.IServicec;
using Pool.Shared.Models;

namespace Pool.API.Authentication;

public class JwtAuthenticationManager
{
    public const string JWT_SECURITY_KEY = "some-security-key";
        private const int JWT_TOKEN_VALIDITY_MINS = 20;

        private readonly IUserService _userService;

        public JwtAuthenticationManager(IUserService userService)
        {
            _userService = userService;
        }

        public UserSessionModel? GenerateJwtToken(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                return null;

            //validating the user credentials
             var userAccount = _userService.GetUserAccountByEmail(userName).Result;
            if (userAccount == null || userAccount.PasswordHash != _userService.EncryptPassword(password))
                return null;

            //generating jwt token
            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, userAccount.Email),
                new Claim(ClaimTypes.Role, userAccount.Role),
            });

            var signingCredintials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredintials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken); 

            //returning the user session object
            var userSession = new UserSessionModel
            {
                Id = userAccount.Id,
                FirstName = userAccount.FirstName,
                LastName = userAccount.LastName,
                Username = userAccount.Username,
                Email = userAccount.Email,
                PhoneNumber = userAccount.PhoneNumber,
                Role = userAccount.Role.Trim(),
                Token = token,
                ExpiryIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds
            };
            return userSession;
        }
}