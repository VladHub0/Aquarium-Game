namespace AquariumGame.Infrastructure.Services
{
    /// <summary>Адреса и лейблы Addressables, используемые на старте игры.</summary>
    public static class AssetKeys
    {
        // Лейбл ассетов, которые грузятся заранее (рыбки, UI, конфиги)
        public const string PreloadLabel = "preload";

        // Адрес префаба рыбы в Addressables
        public const string FishPrefab = "Fish";

        // Адреса сцен в Addressables (или имена сцен в Build Settings)
        public const string MainMenuScene = "MainMenu";
        // Игровая сцена аквариума — пока это сцена фичи рыб
        public const string GameplayScene = "FishScene";
    }
}
