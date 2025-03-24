using eCinema.Data;
using System.ComponentModel.DataAnnotations;

namespace eCinema.Models
{
    public class NewMovieVM
    {
        public int Id { get; set; }

        [Display(Name="Movie Name")]
        [Required(ErrorMessage ="Name Is Required")]
        public string Name { get; set; }
        [Display(Name = "Movie Description")]
        [Required(ErrorMessage = "Description Is Required")]
        public string Description { get; set; }
        [Display(Name = "Price in $")]
        [Required(ErrorMessage = "Price Is Required")]
        public double Price { get; set; }
        [Display(Name = "Movie Poster Url")]
        [Required(ErrorMessage = "Movie Poster Url Is Required")]
        public string ImageUrl { get; set; }
        [Display(Name = "Movie Start Date")]
        [Required(ErrorMessage = "Movie Start Date Is Required")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Movie End Date")]
        [Required(ErrorMessage = "Movie End Date Is Required")]
        public DateTime EndDate { get; set; }
        [Display(Name = "Select A Catergory")]
        [Required(ErrorMessage = "Movie Category Is Required")]
        public MovieCategory MovieCategory { get; set; }
        //Relationship
        [Display(Name = "Select Actor(s)")]
        [Required(ErrorMessage = "Movie Actor(s) Is(Are) Required")]
        public List<int> ActorIds { get; set; }
        //Cinema
        [Display(Name = "Select A Cinema")]
        [Required(ErrorMessage = "Movie Cinema Is Required")]
        public int CinemaId { get; set; }
        [Display(Name = "Select A Producer")]
        [Required(ErrorMessage = "Movie Producer Is Required")]
        public int ProducerId { get; set; }

    }
}
