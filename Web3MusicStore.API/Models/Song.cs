using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web3MusicStore.API.Models;

public class Song
{
  [Key]
  public int Id { get; set; } = default!;

  [Required]
  [Column(TypeName = "NVARCHAR(100)")]
  public string Name { get; set; } = default!;

  [Required]
  [Column(TypeName = "TIME")]
  public SongTime Duration { get; set; } = default!;

  [Required]
  public ICollection<Album> Album { get; set; } = default!;

  [Required]
  [ForeignKey("AlbumId")]
  public int AlbumId { get; set; } = default!;
}