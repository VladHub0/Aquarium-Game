namespace AquariumGame.Features.Fish.Scripts.Catalog
{
    public readonly struct FishSpecies
    {
        public readonly string Key;
        public readonly string DisplayName;
        public readonly int Price;

        public FishSpecies(string key, string displayName, int price)
        {
            Key = key;
            DisplayName = displayName;
            Price = price;
        }
    }
}
