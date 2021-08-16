using System.ComponentModel.DataAnnotations;

namespace ChatboxDemo.Models
{
  public class Message
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public string Author { get; set; }

    [Required]
    public string Content { get; set; }
  }
}
