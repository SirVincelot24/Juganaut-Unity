using UnityEngine;
using UnityEngine.UIElements;

namespace gui
{
    public class MainMenuUI : MonoBehaviour
    {
        public MenuStateHandler menuStateHandler;
        
        private UIDocument _uiDocument;

        private VisualElement _root;
        private Button _startButton;
        private Button _settingsButton;
        private Button _quitButton;
        
        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();

            _root = _uiDocument.rootVisualElement;
            _startButton = _root.Q<Button>("Start");
            _settingsButton = _root.Q<Button>("Settings");
            _quitButton = _root.Q<Button>("Quit");

            DeviceChange.OnResolutionChange += ApplyOrientation;
            
            _startButton.RegisterCallback<ClickEvent>(_ => menuStateHandler.StartGame());
            _settingsButton.RegisterCallback<ClickEvent>(_ => menuStateHandler.OpenSettingsMenu());
            _quitButton.RegisterCallback<ClickEvent>(_ => menuStateHandler.QuitGame());
        }
        
        private void ApplyOrientation(Vector2 resolution)
        {
            var isLandscape = resolution.x >= resolution.y;
            
            _root.EnableInClassList("landscape", isLandscape);
            _root.EnableInClassList("portrait", !isLandscape);
        }
    }
}