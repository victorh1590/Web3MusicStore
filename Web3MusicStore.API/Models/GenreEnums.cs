using System.ComponentModel;
namespace Web3MusicStore.API.Models;

public enum Genre
{
    Pop,
    [Description("Hip Hop")] HipHop,
    Blues,
    Jazz,
    RnB,
    Soul,
    Funk,
    Electronic,
    Dance,
    Country,
    Folk,
    Reggae,
    WorldMusic,
    Latin,
    Punk,
    Classical,
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