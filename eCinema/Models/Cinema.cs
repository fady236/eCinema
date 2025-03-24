using eCinema.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eCinema.Models
{
    public class Cinema : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cinema Logo")]
        [Required(ErrorMessage = "Cinema Logo is Required")]
        public string Logo { get; set; }

        [Display(Name = "Cinema Name")]
        [Required(ErrorMessage = "Cinema Name is Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Cinema Name must be between 3 and 50 chars")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Cinema Description is Required")]
        public string Description { get; set; }

        // ✅ العلاقة بين السينما والأفلام
        public ICollection<Movie> Movies { get; set; } = new List<Movie>(); // تهيئة القائمة مباشرةً
    }
}
