using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web3MusicStore.API.Models;

public class Artist
{
    [Key] public int Id { get; set; }

    [Required]
    [Column(TypeName = "NVARCHAR(200)")]
    public string Name { get; set; } = default!;

    public ICollection<Album>? Albums { get; set; }
}