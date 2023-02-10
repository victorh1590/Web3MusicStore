using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web3MusicStore.API.Models;

public class Song
{
  [Key]
  public int Id { get; set; }

  [Required]
  [Column(TypeName = "NVARCHAR(100)")]
  public string Name { get; set; } = default!;

  [Required]
  [Column(TypeName = "TIME")]
  public TimeSpan Duration { get; set; }

  [Required]
  [ForeignKey("FK_AlbumId")]
  public int AlbumId { get; set; }
  
  [Required]
  public Album Album { get; set; } = default!;
}