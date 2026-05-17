namespace BurnBook.Models.Entities
{
    public class BurnEntry
    {
        public Guid Id { get; set; }

        public string Nickname { get; set; }

        public string Message { get; set; }

        public string Category { get; set; }

        public DateTime DateCreated { get; set; }
    }
}