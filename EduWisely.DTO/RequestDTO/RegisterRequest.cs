using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWisely.DTO.RequestDTO
{
    public class RegisterRequest
    {
        public string Email { get; set; } = string.Empty;   
        public string FullName { get; set; } = string.Empty;
        public string Phoneno { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
