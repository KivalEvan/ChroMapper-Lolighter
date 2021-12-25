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

            AttachTransform(_lolighterMenu, 360, 225, 1, 1, 0, 0, 1, 1);

            Image image = _lolighterMenu.AddComponent<Image>();
            image.sprite = PersistentUI.Instance.Sprites.Background;
            image.type = Image.Type.Sliced;
            image.color = new Color(0.24f, 0.24f, 0.24f);

            AddLabel(_lolighterMenu.transform, "Strobes", "Strobes", new Vector2(0, -15));
            AddCheckbox(_lolighterMenu.transform, "Allow Back Strobe", "Back/Top", new Vector2(0, -35), Options.Light.AllowBackStrobe, (check) =>
            {
                Options.Light.AllowBackStrobe = check;
            });
            AddCheckbox(_lolighterMenu.transform, "Allow Neon Strobe", "Neon Ring", new Vector2(0, -50), Options.Light.AllowNeonStrobe, (check) =>
            {
                Options.Light.AllowNeonStrobe = check;
            });
            AddCheckbox(_lolighterMenu.transform, "Allow Side Strobe", "Bot/Back/Side", new Vector2(0, -65), Options.Light.AllowSideStrobe, (check) =>
            {
                Options.Light.AllowSideStrobe = check;
            });
            AddCheckbox(_lolighterMenu.transform, "Allow Extra Strobe", "Extra", new Vector2(0, -80), Options.Light.AllowExtraStrobe, (check) =>
            {
                Options.Light.AllowExtraStrobe = check;
            });
            AddCheckbox(_lolighterMenu.transform, "Allow Extra2 Strobe", "Extra2", new Vector2(0, -95), Options.Light.AllowExtra2Strobe, (check) =>
            {
                Options.Light.AllowExtra2Strobe = check;
            });
            AddLabel(_lolighterMenu.transform, "Other Features", "Other Features", new Vector2(0, -110));
            AddCheckbox(_lolighterMenu.transform, "Allow Fade", "Use Fade", new Vector2(0, -130), Options.Light.AllowFade, (check) =>
            {
                Options.Light.AllowFade = check;
            });
            AddCheckbox(_lolighterMenu.transform, "Allow Spin Zoom", "Spin/Zoom", new Vector2(0, -145), Options.Light.AllowSpinZoom, (check) =>
            {
                Options.Light.AllowSpinZoom = check;
            });
            AddCheckbox(_lolighterMenu.transform, "Allow Boost", "Use Boost", new Vector2(0, -160), Options.Light.AllowBoostColor, (check) =>
            {
                Options.Light.AllowBoostColor = check;
            });
            AddCheckbox(_lolighterMenu.transform, "Only Common Event", "Only Common Event", new Vector2(0, -175), Options.Light.OnlyCommonEvent, (check) =>
            {
                Options.Light.OnlyCommonEvent = check;
            });
            AddCheckbox(_lolighterMenu.transform, "Ignore Bomb", "Ignore Bomb", new Vector2(0, -190), Options.Light.IgnoreBomb, (check) =>
            {
                Options.Light.IgnoreBomb = check;
            });

            AddLabel(_lolighterMenu.transform, "Color Swap", "Color Swap", new Vector2(120, -15));
            AddTextInput(_lolighterMenu.transform, "Color Swap Speed", "Speed", new Vector2(120, -35), Options.Light.ColorSwap.ToString(), (value) =>
            {
                float res;
                if (float.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out res))
                {
                    Options.Light.ColorSwap = res;
                }
            });
            AddTextInput(_lolighterMenu.transform, "Color Offset", "Offset", new Vector2(120, -55), Options.Light.ColorOffset.ToString(), (value) =>
            {
                float res;
                if (float.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out res))
                {
                    Options.Light.ColorOffset = res;
                }
            });
            AddLabel(_lolighterMenu.transform, "Downlight", "Downlight", new Vector2(120, -75));
            AddTextInput(_lolighterMenu.transform, "Downlight Speed", "Speed", new Vector2(120, -95), Options.Downlight.Speed.ToString(), (value) =>
            {
                float res;
                if (float.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out res))
                {
                    Options.Downlight.Speed = res;
                }
            });
            AddTextInput(_lolighterMenu.transform, "Downlight Spam", "Spam", new Vector2(120, -115), Options.Downlight.SpamSpeed.ToString(), (value) =>
            {
                float res;
                if (float.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out res))
                {
                    Options.Downlight.SpamSpeed = res;
                }
            });
            AddTextInput(_lolighterMenu.transform, "Downlight On", "Turn On", new Vector2(120, -135), Options.Downlight.OnSpeed.ToString(), (value) =>
            {
                float res;
                if (float.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out res))
                {
                    Options.Downlight.OnSpeed = res;
                }
            });
            AddCheckbox(_lolighterMenu.transform, "Clear Lighting", "Clear Existing Light", new Vector2(120, -175), Options.Light.ClearLighting, (check) =>
            {
                Options.Light.ClearLighting = check;
            });

            AddButton(_lolighterMenu.transform, "Lolight", "Light", new Vector2(27.5f, -205), () =>
            {
                _lolighter.Light();
            });
            AddButton(_lolighterMenu.transform, "Lodownlight", "Downlight", new Vector2(92.5f, -205), () =>
            {
                _lolighter.Downlight();
            });

            AddLabel(_lolighterMenu.transform, "Modifier", "Modifier", new Vector2(-120, -15));
            AddButton(_lolighterMenu.transform, "Lolight Bombs", "Bombs", new Vector2(-120, -35), () =>
            {
                _lolighter.Bombs();
            });
            AddButton(_lolighterMenu.transform, "Lolight Inverted", "Inverted", new Vector2(-120, -60), () =>
            {
                _lolighter.Inverted();
            });
            AddButton(_lolighterMenu.transform, "Lolight Loloppe", "Loloppe", new Vector2(-120, -85), () =>
            {
                _lolighter.Loloppe();
            });
            AddButton(_lolighterMenu.transform, "Lolight Sliders", "Sliders", new Vector2(-120, -110), () =>
            {
                _lolighter.Sliders();
            });
            AddCheckbox(_lolighterMenu.transform, "Enable Limiter", "Limiter", new Vector2(-120, -140), Options.Modifier.IsLimited, (check) =>
            {
                Options.Modifier.IsLimited = check;
            });
            AddTextInput(_lolighterMenu.transform, "Limiter In Beat", "", new Vector2(-120, -135), Options.Modifier.Limiter.ToString(), (value) =>
            {
                float res;
                if (float.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out res))
                {
                    Options.Modifier.Limiter = res;
                }
            });
            AddTextInput(_lolighterMenu.transform, "Slider Precision", "Slider Prec.", new Vector2(-120, -155), Options.Modifier.SliderPrecision.ToString(), (value) =>
            {
                float res;
                if (float.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture.NumberFormat, out res))
                {
                    Options.Modifier.SliderPrecision = res;
                }
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

            MoveTransform(rectTransform, 50, 16, 0.5f, 1, pos.x - 27.5f, pos.y);
            var textComponent = entryLabel.GetComponent<TextMeshProUGUI>();

            textComponent.name = title;
            textComponent.font = PersistentUI.Instance.ButtonPrefab.Text.font;
            textComponent.alignment = TextAlignmentOptions.Right;
            textComponent.fontSize = 12;
            textComponent.text = text;

            var textInput = Object.Instantiate(PersistentUI.Instance.TextInputPrefab, parent);
            MoveTransform(textInput.transform, 55, 20, 0.5f, 1, pos.x + 27.5f, pos.y);
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
            MoveTransform(rectTransform, 80, 16, 0.5f, 1, pos.x + 10, pos.y + 5);
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
