using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWisely.DTO.ResponseDTO
{
    public class RegisterResponse
    {
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; }
    }
}
