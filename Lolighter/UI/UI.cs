using System.IO;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Options = Lolighter.Items.Options;

namespace Lolighter.UserInterface
{
    public class UI
    {
        private GameObject _lolighterMenu;
        private readonly Lolighter _lolighter;
        private readonly ExtensionButton _extensionBtn = new ExtensionButton();

        public UI(Lolighter lolighter)
        {
            this._lolighter = lolighter;

            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Lolighter.Icon.png");
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, (int)stream.Length);

            Texture2D texture2D = new Texture2D(256, 256);
            texture2D.LoadImage(data);

            _extensionBtn.Icon = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0, 0), 100.0f);
            _extensionBtn.Tooltip = "Lolighter";
            ExtensionButtons.AddButton(_extensionBtn);
        }

        public void AddMenu(MapEditorUI mapEditorUI)
        {
            CanvasGroup parent = mapEditorUI.MainUIGroup[5];
            _lolighterMenu = new GameObject("Lolighter Menu");
            _lolighterMenu.transform.parent = parent.transform;

            AttachTransform(_lolighterMenu, 240, 225, 1, 1, 0, 0, 1, 1);

            Image image = _lolighterMenu.AddComponent<Image>();
            image.sprite = PersistentUI.Instance.Sprites.Background;
            image.type = Image.Type.Sliced;
            image.color = new Color(0.24f, 0.24f, 0.24f);

            AddLabel(_lolighterMenu.transform, "Strobes", "Strobes", new Vector2(-60, -15));
            AddCheckbox(_lolighterMenu.transform, "Allow Back Strobe", "Back/Top", new Vector2(-60, -35), Options.AllowBackStrobe, (check) =>
            {
                Options.AllowBackStrobe = check;
            });
            AddCheckbox(_lolighterMenu.transform, "Allow Neon Strobe", "Neon Ring", new Vector2(-60, -50), Options.AllowNeonStrobe, (check) =>
            {
                Options.AllowNeonStrobe = check;
            });
            AddCheckbox(_lolighterMenu.transform, "Allow Side Strobe", "Bot/Back/Side", new Vector2(-60, -65), Options.AllowSideStrobe, (check) =>
            {
                Options.AllowSideStrobe = check;
            });
            AddCheckbox(_lolighterMenu.transform, "Allow Extra Strobe", "Extra", new Vector2(-60, -80), Options.AllowExtraStrobe, (check) =>
            {
                Options.AllowExtraStrobe = check;
            });
            AddCheckbox(_lolighterMenu.transform, "Allow Extra2 Strobe", "Extra2", new Vector2(-60, -95), Options.AllowExtra2Strobe, (check) =>
            {
                Options.AllowExtra2Strobe = check;
            });
            AddLabel(_lolighterMenu.transform, "Other Features", "Other Features", new Vector2(-60, -110));
            AddCheckbox(_lolighterMenu.transform, "Allow Fade", "Use Fade", new Vector2(-60, -130), Options.AllowFade, (check) =>
            {
                Options.AllowFade = check;
            });
            AddCheckbox(_lolighterMenu.transform, "Allow Spin Zoom", "Spin/Zoom", new Vector2(-60, -145), Options.AllowSpinZoom, (check) =>
            {
                Options.AllowSpinZoom = check;
            });
            AddCheckbox(_lolighterMenu.transform, "Allow Boost", "Use Boost", new Vector2(-60, -160), Options.AllowBoostColor, (check) =>
            {
                Options.AllowBoostColor = check;
            });
            AddCheckbox(_lolighterMenu.transform, "Only Common Event", "Only Common Event", new Vector2(-60, -175), Options.OnlyCommonEvent, (check) =>
            {
                Options.OnlyCommonEvent = check;
            });
            AddCheckbox(_lolighterMenu.transform, "Clear Lighting", "Clear Lighting", new Vector2(-60, -210), Options.ClearLighting, (check) =>
            {
                Options.ClearLighting = check;
            });

            AddLabel(_lolighterMenu.transform, "Color Swap", "Color Swap", new Vector2(60, -15));
            AddTextInput(_lolighterMenu.transform, "Color Swap Speed", "Speed (in beat)", new Vector2(60, -35), Options.ColorSwap.ToString(), (value) =>
            {
                float res;
                if (float.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out res))
                {
                    Options.ColorSwap = res;
                }
            });
            AddTextInput(_lolighterMenu.transform, "Color Offset", "Offset (in beat)", new Vector2(60, -65), Options.ColorOffset.ToString(), (value) =>
            {
                float res;
                if (float.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out res))
                {
                    Options.ColorOffset = res;
                }
            });
            AddLabel(_lolighterMenu.transform, "Downlight", "Downlight", new Vector2(60, -95));
            AddTextInput(_lolighterMenu.transform, "Downlight Speed", "Speed (in beat)", new Vector2(60, -110), Options.DownlightSpeed.ToString(), (value) =>
            {
                float res;
                if (float.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out res))
                {
                    Options.DownlightSpeed = res;
                }
            });
            AddTextInput(_lolighterMenu.transform, "Downlight Spam", "Spam (in beat)", new Vector2(60, -140), Options.DownlightSpamSpeed.ToString(), (value) =>
            {
                float res;
                if (float.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out res))
                {
                    Options.DownlightSpamSpeed = res;
                }
            });
            AddTextInput(_lolighterMenu.transform, "Downlight On", "Turn On (in beat)", new Vector2(60, -170), Options.DownlightOnSpeed.ToString(), (value) =>
            {
                float res;
                if (float.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out res))
                {
                    Options.DownlightOnSpeed = res;
                }
            });

            AddButton(_lolighterMenu.transform, "Lolight", "Light", new Vector2(15, -205), () =>
            {
                _lolighter.Light();
            });
            AddButton(_lolighterMenu.transform, "Lodownlight", "Downlight", new Vector2(80, -205), () =>
            {
                _lolighter.Downlight();
            });

            _lolighterMenu.SetActive(false);
            _extensionBtn.Click = () =>
            {
                _lolighterMenu.SetActive(!_lolighterMenu.activeSelf);
            };
        }

        // i ended up copying Top_Cat's CM-JS UI helper, too useful to make my own tho
        // after askin TC if it's one of the only way, he let me use this
        private void AddButton(Transform parent, string title, string text, Vector2 pos, UnityAction onClick)
        {
            var button = Object.Instantiate(PersistentUI.Instance.ButtonPrefab, parent);
            MoveTransform(button.transform, 60, 25, 0.5f, 1, pos.x, pos.y);

            button.name = title;
            button.Button.onClick.AddListener(onClick);

            button.SetText(text);
            button.Text.enableAutoSizing = false;
            button.Text.fontSize = 12;
        }
        private void AddLabel(Transform parent, string title, string text, Vector2 pos, Vector2? size = null)
        {
            var entryLabel = new GameObject(title + " Label", typeof(TextMeshProUGUI));
            var rectTransform = ((RectTransform)entryLabel.transform);
            rectTransform.SetParent(parent);

            MoveTransform(rectTransform, 110, 24, 0.5f, 1, pos.x, pos.y);
            var textComponent = entryLabel.GetComponent<TextMeshProUGUI>();

            textComponent.name = title;
            textComponent.font = PersistentUI.Instance.ButtonPrefab.Text.font;
            textComponent.alignment = TextAlignmentOptions.Center;
            textComponent.fontSize = 16;
            textComponent.text = text;
        }

        private void AddTextInput(Transform parent, string title, string text, Vector2 pos, string value, UnityAction<string> onChange)
        {
            var entryLabel = new GameObject(title + " Label", typeof(TextMeshProUGUI));
            var rectTransform = ((RectTransform)entryLabel.transform);
            rectTransform.SetParent(parent);

            MoveTransform(rectTransform, 100, 16, 0.5f, 1, pos.x, pos.y + 5);
            var textComponent = entryLabel.GetComponent<TextMeshProUGUI>();

            textComponent.name = title;
            textComponent.font = PersistentUI.Instance.ButtonPrefab.Text.font;
            textComponent.alignment = TextAlignmentOptions.Center;
            textComponent.fontSize = 10;
            textComponent.text = text;

            var textInput = Object.Instantiate(PersistentUI.Instance.TextInputPrefab, parent);
            MoveTransform(textInput.transform, 100, 20, 0.5f, 1, pos.x, pos.y - 10);
            textInput.GetComponent<Image>().pixelsPerUnitMultiplier = 3;
            textInput.InputField.text = value;
            textInput.InputField.onFocusSelectAll = false;
            textInput.InputField.textComponent.alignment = TextAlignmentOptions.Left;
            textInput.InputField.textComponent.fontSize = 10;

            textInput.InputField.onValueChanged.AddListener(onChange);
        }

        private void AddCheckbox(Transform parent, string title, string text, Vector2 pos, bool value, UnityAction<bool> onClick)
        {
            var entryLabel = new GameObject(title + " Label", typeof(TextMeshProUGUI));
            var rectTransform = ((RectTransform)entryLabel.transform);
            rectTransform.SetParent(parent);
            MoveTransform(rectTransform, 80, 16, 0.5f, 1, pos.x + 10, pos.y + 6);
            var textComponent = entryLabel.GetComponent<TextMeshProUGUI>();

            textComponent.name = title;
            textComponent.font = PersistentUI.Instance.ButtonPrefab.Text.font;
            textComponent.alignment = TextAlignmentOptions.Left;
            textComponent.fontSize = 12;
            textComponent.text = text;

            var original = GameObject.Find("Strobe Generator").GetComponentInChildren<Toggle>(true);
            var toggleObject = Object.Instantiate(original, parent.transform);
            MoveTransform(toggleObject.transform, 100, 25, 0.5f, 1, pos.x, pos.y);

            var toggleComponent = toggleObject.GetComponent<Toggle>();
            var colorBlock = toggleComponent.colors;
            colorBlock.normalColor = Color.white;
            toggleComponent.colors = colorBlock;
            toggleComponent.isOn = value;

            toggleComponent.onValueChanged.AddListener(onClick);
        }

        private RectTransform AttachTransform(GameObject obj, float sizeX, float sizeY, float anchorX, float anchorY, float anchorPosX, float anchorPosY, float pivotX = 0.5f, float pivotY = 0.5f)
        {
            RectTransform rectTransform = obj.AddComponent<RectTransform>();
            rectTransform.localScale = new Vector3(1, 1, 1);
            rectTransform.sizeDelta = new Vector2(sizeX, sizeY);
            rectTransform.pivot = new Vector2(pivotX, pivotY);
            rectTransform.anchorMin = rectTransform.anchorMax = new Vector2(anchorX, anchorY);
            rectTransform.anchoredPosition = new Vector3(anchorPosX, anchorPosY, 0);

            return rectTransform;
        }

        private void MoveTransform(Transform transform, float sizeX, float sizeY, float anchorX, float anchorY, float anchorPosX, float anchorPosY, float pivotX = 0.5f, float pivotY = 0.5f)
        {
            if (!(transform is RectTransform rectTransform)) return;

            rectTransform.localScale = new Vector3(1, 1, 1);
            rectTransform.sizeDelta = new Vector2(sizeX, sizeY);
            rectTransform.pivot = new Vector2(pivotX, pivotY);
            rectTransform.anchorMin = rectTransform.anchorMax = new Vector2(anchorX, anchorY);
            rectTransform.anchoredPosition = new Vector3(anchorPosX, anchorPosY, 0);
        }
    }
}
