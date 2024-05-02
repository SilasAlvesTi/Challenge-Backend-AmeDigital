namespace Planetas_StarWars.Models
{
    public record Planet(Guid Id, string? Name, string? Wheater, string? Terrain, int MovieAppearances);
}