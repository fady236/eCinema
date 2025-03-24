using eCinema.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eCinema.Models
{
    public class Producer : IEntityBase
    {
        [Key] // ✅ تحديد المفتاح الأساسي
        public int Id { get; set; }

        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile Picture is Required")]
        public string ProfilePicUrl { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 chars")] // ✅ نفس التقييد المستخدم في `Actor.cs`
        public string FullName { get; set; }

        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Biography is Required")]
        public string Biography { get; set; }

        public List<Movie> Movies { get; set; } = new List<Movie>(); // ✅ ضمان عدم حدوث `NullReferenceException`
    }
}
