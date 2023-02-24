using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web3MusicStore.API.Models.Enums;

namespace Web3MusicStore.API.Models;

public class Album
{
  [Key]
  public int Id { get; set; }

  [Required]
  [Column(TypeName = "NVARCHAR(200)")]
  public string Name { get; set; } = default!;

  [Required]
  public Genre Genre { get; set; }

  [Required]
  [Column(TypeName = "NVARCHAR(500)")]
  public string Cover { get; set; } = default!;

  [Required]
  [ForeignKey("Fk_ArtistId")]
  public int UserId { get; set; }
  
  [Required]
  public User User { get; set; } = default!;

  public ICollection<Song> Songs { get; set; } = default!;

  [Required]
  public decimal Price { get; set; }
}