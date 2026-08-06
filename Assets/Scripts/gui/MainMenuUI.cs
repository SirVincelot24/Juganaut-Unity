using UnityEngine;
using UnityEngine.UIElements;

namespace gui
{
    public class MainMenuUI : MonoBehaviour
    {
        public MenuStateHandler menuStateHandler;
        
        private UIDocument _uiDocument;
        
        private Button _startButton;
        private Button _settingsButton;
        private Button _quitButton;
        
        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            
            _startButton = _uiDocument.rootVisualElement.Q<Button>("Start");
            _settingsButton = _uiDocument.rootVisualElement.Q<Button>("Settings");
            _quitButton = _uiDocument.rootVisualElement.Q<Button>("Quit");
            
            _startButton.RegisterCallback<ClickEvent>(_ => menuStateHandler.StartGame());
            _settingsButton.RegisterCallback<ClickEvent>(_ => menuStateHandler.OpenSettingsMenu());
            _quitButton.RegisterCallback<ClickEvent>(_ => menuStateHandler.QuitGame());
        }
    }
}