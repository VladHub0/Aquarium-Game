using UnityEngine;

namespace AquariumGame.Features.UI.Scripts.Skin
{
    [CreateAssetMenu(menuName = "Aquarium/UI Skin", fileName = "UiSkin")]
    public sealed class UiSkin : ScriptableObject
    {
        [Header("Text")]
        [SerializeField] private Font font;
        [SerializeField] private Color textColor = Color.white;
        [SerializeField] private Color outlineColor = new Color(0.32f, 0.15f, 0.05f, 1f);
        [SerializeField] private Vector2 outlineDistance = new Vector2(3f, -3f);
        [SerializeField] private Color shadowColor = new Color(0f, 0f, 0f, 0.35f);
        [SerializeField] private Vector2 shadowDistance = new Vector2(0f, -5f);
        [SerializeField] private Color titleColor = new Color(1f, 0.84f, 0.2f, 1f);

        [Header("Hud")]
        [SerializeField] private Sprite hudBar;
        [SerializeField] private Sprite slot;
        [SerializeField] private Sprite emptySlot;
        [SerializeField] private Sprite activeSlot;
        [SerializeField] private Sprite priceTag;
        [SerializeField] private Sprite coinsDisplay;
        [SerializeField] private Sprite timerDisplay;
        [SerializeField] private Sprite coin;
        [SerializeField] private Color coinsTextColor = new Color(0.55f, 1f, 0.35f, 1f);
        [SerializeField] private Color timerTextColor = new Color(0.45f, 0.9f, 1f, 1f);
        [SerializeField] private Color warningTextColor = new Color(1f, 0.35f, 0.3f, 1f);
        [SerializeField] private Color disabledTint = new Color(0.55f, 0.55f, 0.62f, 1f);

        [Header("Windows")]
        [SerializeField] private Sprite primaryButton;
        [SerializeField] private Sprite secondaryButton;
        [SerializeField] private Sprite accentButton;
        [SerializeField] private Sprite panel;
        [SerializeField] private Color dimColor = new Color(0.05f, 0.02f, 0.15f, 0.65f);

        public Font Font => font;

        public Color TextColor => textColor;

        public Color OutlineColor => outlineColor;

        public Vector2 OutlineDistance => outlineDistance;

        public Color ShadowColor => shadowColor;

        public Vector2 ShadowDistance => shadowDistance;

        public Color TitleColor => titleColor;

        public Sprite HudBar => hudBar;

        public Sprite Slot => slot;

        public Sprite EmptySlot => emptySlot;

        public Sprite ActiveSlot => activeSlot;

        public Sprite PriceTag => priceTag;

        public Sprite CoinsDisplay => coinsDisplay;

        public Sprite TimerDisplay => timerDisplay;

        public Sprite Coin => coin;

        public Color CoinsTextColor => coinsTextColor;

        public Color TimerTextColor => timerTextColor;

        public Color WarningTextColor => warningTextColor;

        public Color DisabledTint => disabledTint;

        public Sprite PrimaryButton => primaryButton;

        public Sprite SecondaryButton => secondaryButton;

        public Sprite AccentButton => accentButton;

        public Sprite Panel => panel;

        public Color DimColor => dimColor;
    }
}
