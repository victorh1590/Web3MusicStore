using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web3MusicStore.API.Models;

public class Album
{
  [Key]
  public int Id { get; set; } = default!;

  [Required]
  [Column(TypeName = "NVARCHAR(200)")]
  public string Name { get; set; } = default!;

  [Required]
  public Genre Genre { get; set; } = default!;

  [Required]
  [Column(TypeName = "NVARCHAR(500)")]
  public string Cover { get; set; } = default!;

  [Required]
  public ICollection<Artist> Artists { get; set; } = default!;

  [Required]
  public ICollection<Song> Songs { get; set; } = default!;

  [Required]
  public decimal Price { get; set; } = default!;
}