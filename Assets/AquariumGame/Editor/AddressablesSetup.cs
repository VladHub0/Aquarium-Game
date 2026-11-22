using System.Collections.Generic;
using System.Linq;
using AquariumGame.Infrastructure.Services;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace AquariumGame.Editor
{
    [InitializeOnLoad]
    public static class AddressablesSetup
    {
        private static readonly AddressableEntry[] Entries =
        {
            new AddressableEntry("Assets/AquariumGame/Features/Fish/Prefabs/Fish.prefab", AssetKeys.FishPrefab, AssetKeys.PreloadLabel),
            new AddressableEntry("Assets/AquariumGame/Features/Fish/Prefabs/FishTang.prefab", AssetKeys.FishTangPrefab, AssetKeys.PreloadLabel),
            new AddressableEntry("Assets/AquariumGame/Features/Fish/Prefabs/FishGramma.prefab", AssetKeys.FishGrammaPrefab, AssetKeys.PreloadLabel),
            new AddressableEntry("Assets/AquariumGame/Scenes/MainMenu.unity", AssetKeys.MainMenuScene, null),
            new AddressableEntry("Assets/AquariumGame/Features/Fish/Scene/FishScene.unity", AssetKeys.GameplayScene, null),
            new AddressableEntry("Assets/AquariumGame/Scenes/Pause.unity", AssetKeys.PauseScene, null),
        };

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

            var changed = RemoveAddressableScenesFromBuildSettings();

            foreach (var entry in Entries)
                changed |= EnsureEntry(settings, entry);

            if (settings.BuildAddressablesWithPlayerBuild != AddressableAssetSettings.PlayerBuildOption.BuildWithPlayer)
            {
                settings.BuildAddressablesWithPlayerBuild = AddressableAssetSettings.PlayerBuildOption.BuildWithPlayer;
                changed = true;
            }

            if (!changed)
            {
                if (verbose)
                    Debug.Log("[AddressablesSetup] Addressables уже настроены.");
                return;
            }

            settings.SetDirty(AddressableAssetSettings.ModificationEvent.BatchModification, null, true, true);
            AssetDatabase.SaveAssets();

            Debug.Log("[AddressablesSetup] Addressables настроены: префабы рыб и сцены MainMenu, FishScene, Pause.");
        }

        private static bool EnsureEntry(AddressableAssetSettings settings, AddressableEntry data)
        {
            var guid = AssetDatabase.AssetPathToGUID(data.Path);
            if (string.IsNullOrEmpty(guid))
            {
                Debug.LogWarning($"[AddressablesSetup] Ассет не найден: {data.Path}");
                return false;
            }

            var existing = settings.FindAssetEntry(guid);
            if (existing != null
                && existing.address == data.Address
                && (data.Label == null || existing.labels.Contains(data.Label)))
                return false;

            if (data.Label != null)
                settings.AddLabel(data.Label);

            var entry = settings.CreateOrMoveEntry(guid, settings.DefaultGroup, false, false);
            entry.address = data.Address;

            if (data.Label != null)
                entry.SetLabel(data.Label, true, true, false);

            return true;
        }

        private static bool RemoveAddressableScenesFromBuildSettings()
        {
            var addressableScenes = new HashSet<string>(Entries
                .Where(entry => entry.Path.EndsWith(".unity"))
                .Select(entry => entry.Path));

            var scenes = EditorBuildSettings.scenes;
            var filtered = scenes.Where(scene => !addressableScenes.Contains(scene.path)).ToArray();

            if (filtered.Length == scenes.Length)
                return false;

            EditorBuildSettings.scenes = filtered;
            return true;
        }

        private readonly struct AddressableEntry
        {
            public readonly string Path;
            public readonly string Address;
            public readonly string Label;

            public AddressableEntry(string path, string address, string label)
            {
                Path = path;
                Address = address;
                Label = label;
            }
        }
    }
}
