using EduWisely.Database.Entities;
using EduWisely.DTO.RequestDTO;
using EduWisely.DTO.ResponseDTO;
using EduWisely.Interfaces.IServices;
using EduWisely.UoW;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EduWisely.Services.Services
{
    public partial class AuthService : BaseService, IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public AuthService(IUnitOfWork _unitOfWork, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager) : base(_unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<LoginResponse> Login(string email, string password)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(email);
                if (user == null) return new LoginResponse
                {
                    Message = "Invalid email or password",
                    Success = false
                };

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                var roles = await _userManager.GetRolesAsync(user);
                var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x));
                claims.AddRange(roleClaims);
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("thisismycustomSecretkeyforauthentication"));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                var expires = DateTime.Now.AddMinutes(30);

                var token = new JwtSecurityToken(
                    issuer: "",
                    audience: "",
                    claims: claims,
                    expires: expires,
                    signingCredentials: creds
                    );


                return new LoginResponse
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    Message = "login Success..",
                    Email = user?.Email,
                    Success = true,
                    UserId = user?.Id.ToString()
                };
            }
            catch (Exception ex)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }


        public async Task<RegisterResponse> Register(RegisterRequest request)
        {
            try
            {
                var userExists = await _userManager.FindByEmailAsync(request.Email);
                if(userExists != null){
                    return new RegisterResponse
                    {
                        Message = "user alredy exists!..",
                        Success = false
                    };
                }

                userExists = new ApplicationUser
                {
                    Fullname = request.FullName,
                    Email = request.Email,
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    UserName = request.Email
                };

                var creteUserResult = await _userManager.CreateAsync(userExists, request.Password);
                if (!creteUserResult.Succeeded) {
                    return new RegisterResponse
                    {
                        Message = $"User Registration failed.. {creteUserResult?.Errors?.First()?.Description}",
                        Success = false
                    };
                }


                var addUserToRole = await _userManager.AddToRoleAsync(userExists, "STUDENT");
                if (!addUserToRole.Succeeded)
                {
                    return new RegisterResponse
                    {
                        Message = $"User Create Success but could not add Role .. {addUserToRole?.Errors?.First()?.Description}",
                        Success = false
                    };
                }

                return new RegisterResponse
                {
                    Success = true,
                    Message = "User Registration Successfully..."
                };
            }
            catch (Exception ex)
            {
                return new RegisterResponse
                {
                    Message = ex.Message,
                    Success = false
                };
            }
        }

        

    }
}
