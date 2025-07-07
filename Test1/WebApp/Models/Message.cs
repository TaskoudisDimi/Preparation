using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Message
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
