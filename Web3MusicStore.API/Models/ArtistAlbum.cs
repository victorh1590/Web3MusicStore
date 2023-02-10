using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web3MusicStore.API.Models;

public class ArtistAlbum
{
    [Required]
    [ForeignKey("FK_ArtistAlbum_AlbumId")]
    public int AlbumId { get; set; }

    [Required] public ICollection<Album> Albums { get; set; } = default!;
    
    [Required]
    [ForeignKey("FK_ArtistAlbum_ArtistId")]
    public int ArtistId { get; set; }

    [Required] 
    public ICollection<Artist> Artists { get; set; } = default!;
}