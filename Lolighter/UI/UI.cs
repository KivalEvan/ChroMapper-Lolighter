using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

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

            RectTransform rectTransform = _lolighterMenu.AddComponent<RectTransform>();
            rectTransform.localScale = new Vector3(1, 1, 1);
            rectTransform.sizeDelta = new Vector2(200, 100);
            rectTransform.pivot = new Vector2(0, 0);
            rectTransform.anchorMin = rectTransform.anchorMax = new Vector2(0, 1);
            rectTransform.anchoredPosition = new Vector3(0, 0, 0);

            Image image = _lolighterMenu.AddComponent<Image>();
            image.sprite = PersistentUI.Instance.Sprites.Background;
            image.type = Image.Type.Sliced;
            image.color = new Color(0.25f, 0.25f, 0.25f, 1);

            _lolighterMenu.SetActive(false);
            _extensionBtn.Click = () =>
            {
                _lolighterMenu.SetActive(!_lolighterMenu.activeSelf);
                _lolighter.Light();
            };
        }
    }
}
