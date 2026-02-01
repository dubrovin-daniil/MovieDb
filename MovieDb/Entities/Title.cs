namespace MoviesDB.Entities
{
    public class Title
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public string Description { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
