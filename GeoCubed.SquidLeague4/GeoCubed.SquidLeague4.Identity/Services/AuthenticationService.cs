using GeoCubed.SquidLeague4.Application.Interfaces.Authentication;
using GeoCubed.SquidLeague4.Application.Models.Authentication;
using GeoCubed.SquidLeague4.Application.Models.Authentication.Enum;
using GeoCubed.SquidLeague4.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Identity.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AuthenticationService(
            UserManager<ApplicationUser> userManager,
            IOptions<JwtSettings> jwtSettings,
            SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            this._jwtSettings = jwtSettings.Value;
            this._signInManager = signInManager;
        }

        public async Task<RoleResponse> AddRole(RoleRequest request)
        {
            var user = await this._userManager.FindByNameAsync(request.Username);
            var response = new RoleResponse()
            {
                Username = request.Username,
                Role = request.RoleName,
                Status = RoleStatus.Added,
                Message = "The role was added successfully"
            };

            if (user == null)
            {
                response.Status = RoleStatus.Error;
                response.Message = "User does not exist";
                return response;
            }

            var result = await this._userManager.AddToRoleAsync(user, request.RoleName);
            if (!result.Succeeded)
            {
                response.Status = RoleStatus.Error;
                if (result.Errors.Count() > 0)
                {
                    response.Message = result.Errors.ElementAt(0).Description;
                }
                else
                {
                    response.Message = "There was an issue adding the role";
                }
            }

            return response;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            var user = await this._userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                throw new Exception("Invalid UserName or Password.");
            }

            var result = await this._signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                throw new Exception("Invalid Username or Password.");
            }

            var jwtSecurityToken = await this.GenerateToken(user);
            var response = new AuthenticationResponse()
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UserName = user.UserName
            };

            return response;
        }

        public async Task<DeleteResponse> DeleteAsync(DeleteRequest request)
        {
            var user = await this._userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                return new DeleteResponse
                {
                    Success = false,
                    Message = new List<string>() { $"User [{request.Username}] does not exist." }
                };
            }

            var result = await this._userManager.DeleteAsync(user);
            var response = new DeleteResponse();
            response.Success = result.Succeeded;
            if (result.Errors.Count() > 0)
            {
                foreach (var error in result.Errors)
                {
                    response.Message.Add(error.Description);
                }
            }

            return response;
        }

        public async Task<List<string>> GetRoles(string username)
        {
            var roles = new List<string>();
            var user = await this._userManager.FindByNameAsync(username);
            if (user != null)
            {
                var userRoles = await this._userManager.GetRolesAsync(user);
                roles.AddRange(userRoles);
            }

            return roles;
        }

        public async Task<RegistrationResponse> RegisterAsync(RegistrationRequest request)
        {
            var existingUser = await this._userManager.FindByNameAsync(request.UserName);
            if (existingUser != null)
            {
                throw new Exception($"Username [{request.UserName}] already exists.");
            }

            var user = new ApplicationUser
            {
                UserName = request.UserName
            };

            var result = await this._userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                return new RegistrationResponse() { UserId = user.Id };
            }

            throw new Exception($"{result.Errors}");
        }

        public async Task<RoleResponse> RemoveRole(RoleRequest request)
        {
            var user = await this._userManager.FindByNameAsync(request.Username);
            var response = new RoleResponse()
            {
                Username = request.Username,
                Role = request.RoleName,
                Status = RoleStatus.Removed,
                Message = "The role was added successfully"
            };

            if (user == null)
            {
                response.Status = RoleStatus.Error;
                response.Message = "User does not exist";
                return response;
            }

            var result = await this._userManager.RemoveFromRoleAsync(user, request.RoleName);
            if (!result.Succeeded)
            {
                response.Status = RoleStatus.Error;
                if (result.Errors.Count() > 0)
                {
                    response.Message = result.Errors.ElementAt(0).Description;
                }
                else
                {
                    response.Message = "There was an issue removing the role";
                }
            }

            return response;
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await this._userManager.GetClaimsAsync(user);
            var roles = await this._userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; ++i)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: this._jwtSettings.Issuer,
                audience: this._jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(this._jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
