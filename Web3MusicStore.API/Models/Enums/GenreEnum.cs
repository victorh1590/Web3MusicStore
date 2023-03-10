using System.ComponentModel;
namespace Web3MusicStore.API.Models.Enums;

public enum Genre
{
    Other,
    Pop,
    [Description("Hip Hop")] HipHop,
    Blues,
    Jazz,
    [Description("Rhythm & Blues")] RnB,
    Soul,
    Funk,
    Electronic,
    Dance,
    Country,
    Folk,
    Reggae,
    [Description("World Music")] WorldMusic,
    [Description("Latin Music")] Latin,
    Punk,
    [Description("Classical Music")] Classical,
    Opera,
    Gospel,
    [Description("Electronic Dance Music")] EDM,
    Rap,
    [Description("Alternative Music")] Alternative,
    [Description("Indie Music")] Indie,
    Acoustic,
    [Description("Progressive Music")] Progressive,
    [Description("Classic Music")] Classic,
    Vaporwave,
    [Description("Futuristic Funk")] FuturisticFunk,
    Synthwave,
    Outrun,
    Retrowave,
    Dreamwave,
    Chillwave,
    Dreampunk,
    [Description("Lo-Fi Hip-Hop")] LoFiHipHop
}