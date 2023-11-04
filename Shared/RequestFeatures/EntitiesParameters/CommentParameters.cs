using Shared.ValidationAttributes;

namespace Shared.RequestFeatures.EntitiesParameters
{
    public class CommentParameters : RequestParameters
    {
        public string? SearchedText { get; set; } = "";
    }
}
