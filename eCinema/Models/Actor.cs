using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using eCinema.Data.Base;

namespace eCinema.Models
{
    public class Actor : IEntityBase
    {
        [Key]
        public int Id { get; set; } 

        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile Picture is Required")]
        public string ProfilePicUrl { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 chars")]
        public string FullName { get; set; }

        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Biography is Required")]
        public string Biography { get; set; }


        public ICollection<Actor_Movie> Actor_Movies { get; set; } = new List<Actor_Movie>(); 
    }
}
