namespace Shared.RequestFeatures.EntitiesParameters
{
    public class DirectorParameters : RequestParameters
    {
        private string? _searchedName;
        public string? SearchedName
        {
            get
            {
                return _searchedName == null ? "" : _searchedName;
            }
            set
            {
                _searchedName = value;
            }
        }
    }
}
