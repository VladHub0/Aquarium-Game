using AquariumGame.Features.AssetManagement.Scripts.Keys;

namespace AquariumGame.Features.Fish.Scripts.Catalog
{
    public static class FishCatalog
    {
        public static readonly FishSpecies[] All =
        {
            new FishSpecies(AssetKeys.FishPrefab, "Clownfish", 100),
            new FishSpecies(AssetKeys.FishTangPrefab, "Blue Tang", 150),
            new FishSpecies(AssetKeys.FishGrammaPrefab, "Royal Gramma", 200),
        };
    }
}
