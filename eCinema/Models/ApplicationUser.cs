using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCinema.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }

        [NotMapped] // هذه الخاصية لن يتم حفظها في قاعدة البيانات
        public List<string> Roles { get; set; } = new List<string>();
    }
}
