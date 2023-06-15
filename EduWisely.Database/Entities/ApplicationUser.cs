using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduWisely.Database.Entities
{
    [CollectionName("users")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
        public string Fullname { get; set; } = string.Empty;
    }
}
