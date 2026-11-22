using AquariumGame.Infrastructure.Services;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace AquariumGame.Editor
{
    /// <summary>
    /// Создаёт настройки Addressables (если их нет) и регистрирует префаб рыбы
    /// с адресом AssetKeys.FishPrefab и лейблом AssetKeys.PreloadLabel.
    /// Запускается автоматически при открытии проекта и через меню Aquarium/Setup Addressables.
    /// </summary>
    [InitializeOnLoad]
    public static class AddressablesSetup
    {
        private const string FishPrefabPath = "Assets/AquariumGame/Features/Fish/Prefabs/Fish.prefab";

        static AddressablesSetup()
        {
            EditorApplication.delayCall += () => EnsureSetup(false);
        }

        [MenuItem("Aquarium/Setup Addressables")]
        private static void SetupFromMenu() => EnsureSetup(true);

        private static void EnsureSetup(bool verbose)
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode)
                return;

            var settings = AddressableAssetSettingsDefaultObject.GetSettings(true);
            if (settings == null)
            {
                Debug.LogError("[AddressablesSetup] Не удалось создать настройки Addressables.");
                return;
            }

            var guid = AssetDatabase.AssetPathToGUID(FishPrefabPath);
            if (string.IsNullOrEmpty(guid))
            {
                Debug.LogWarning($"[AddressablesSetup] Префаб рыбы не найден: {FishPrefabPath}");
                return;
            }

            var entry = settings.FindAssetEntry(guid);
            if (entry != null
                && entry.address == AssetKeys.FishPrefab
                && entry.labels.Contains(AssetKeys.PreloadLabel))
            {
                if (verbose)
                    Debug.Log("[AddressablesSetup] Addressables уже настроены.");
                return;
            }

            settings.AddLabel(AssetKeys.PreloadLabel);
            entry = settings.CreateOrMoveEntry(guid, settings.DefaultGroup);
            entry.address = AssetKeys.FishPrefab;
            entry.SetLabel(AssetKeys.PreloadLabel, true, true);

            settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, entry, true);
            AssetDatabase.SaveAssets();

            Debug.Log($"[AddressablesSetup] Префаб рыбы зарегистрирован: '{AssetKeys.FishPrefab}' (label '{AssetKeys.PreloadLabel}').");
        }
    }
}
