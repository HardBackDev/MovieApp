using Entities.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared.Dtos.MovieDtos;
using Shared.ValidationAttributes;

namespace Shared.RequestFeatures.EntitiesParameters
{
    public class MovieParameters : RequestParameters
    {
        public MovieParameters()
        {
            OrderBy = nameof(MovieDto.ReleaseDate);
        }

        public string? SearchedTitle { get; set; } = "";
        public string? SearchedGenres { get; set; } = "";
        [BindNever]
        public List<string>? SearchedGenresList => string.IsNullOrEmpty(SearchedGenres) ? new() : SearchedGenres.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();


        public DateTime MinDateRelease { get; set; } = DateTime.MinValue;
        [IsGreaterThan(nameof(MinDateRelease))]
        public DateTime MaxDateRelease { get; set; } = DateTime.MaxValue;

        public int MinGoodRating { get; set; }
        [IsGreaterThan(nameof(MinGoodRating))]
        public int MaxGoodRating { get; set; } = int.MaxValue;

    }
}
