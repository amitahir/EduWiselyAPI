using EduWisely.DTO.RequestDTO;
using EduWisely.DTO.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWisely.Interfaces.IServices
{
    public interface IAuthService : IBaseService
    {
        Task<LoginResponse> Login(string email, string password);
        Task<RegisterResponse> Register(RegisterRequest request);

    }
}
