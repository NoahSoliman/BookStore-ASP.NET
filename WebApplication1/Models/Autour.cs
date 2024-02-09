using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class Author
    {
        public int Id { get; set; }


        [MaxLength(20)]
        [MinLength(5)]
        public string FullName { get; set; }
    }
}
