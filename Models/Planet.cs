using Planetas_StarWars.Services;

namespace Planetas_StarWars.Models
{
    public class Planet
    {
        public Guid Id { get; private set; }
        public string? Name { get; private set; }
        public string? Wheater { get; private set; }
        public string? Terrain { get; private set; }
        public int MovieAppearances { get; private set; }

        public Planet(Guid id, string name, string? wheater, string? terrain)
        {
            Id = id;
            Name = name;
            Wheater = wheater;
            Terrain = terrain;
        }

        public void AddMovieAppearances(int movieAppearances)
        {
            MovieAppearances = movieAppearances;
        }
    }
}