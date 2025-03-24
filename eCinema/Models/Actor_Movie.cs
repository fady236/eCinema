using eCinema.Models;

public class Actor_Movie
{
    public int ActorId { get; set; }
    public int MovieId { get; set; }

    // العلاقة بين Actor و Movie
    public Actor Actor { get; set; }
    public Movie Movie { get; set; }
}
