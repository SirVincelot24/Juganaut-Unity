using UnityEngine;
using UnityEngine.UIElements;

namespace gui
{
    public class SettingsUI : MonoBehaviour
    {
        public MenuStateHandler menuStateHandler;
        public SettingsManager settingsManager;
        public ThemeStyleSheet[]  themeStyleSheets;

        private UIDocument _uiDocument;

        private Button _backButton;
        private Button _resetButton;
        
        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            _uiDocument.enabled = true;
            _uiDocument.rootVisualElement.visible = false;
            _uiDocument.rootVisualElement.dataSource = settingsManager;

            settingsManager.OnThemeChanged = ChangeTheme;

            _backButton = _uiDocument.rootVisualElement.Q<Button>("Back");
            _resetButton = _uiDocument.rootVisualElement.Q<Button>("Reset");
            
            _backButton.RegisterCallback<ClickEvent>(_ => menuStateHandler.CloseSettingsMenu());
            _resetButton.RegisterCallback<ClickEvent>(_ => settingsManager.ResetWorldSettings());
        }
        
        private void ChangeTheme(Theme theme)
        {
            _uiDocument.panelSettings.themeStyleSheet = theme switch
            {
                Theme.Dark => themeStyleSheets[1],
                Theme.Light => themeStyleSheets[0],
                _ => themeStyleSheets[1]
            };
        }
    }
}