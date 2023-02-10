using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web3MusicStore.API.Models;

public class AlbumSong
{
    [Required]
    [ForeignKey("FK_AlbumSong_AlbumId")]
    public int AlbumId { get; set; }

    [Required] 
    public ICollection<Album> Albums { get; set; } = default!;
    
    [Required]
    [ForeignKey("FK_AlbumSong_SongId")]
    public int SongId { get; set; }

    [Required] 
    public ICollection<Song> Songs { get; set; } = default!;
}