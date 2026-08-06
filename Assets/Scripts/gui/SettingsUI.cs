using UnityEngine;
using UnityEngine.UIElements;

namespace gui
{
    public class SettingsUI : MonoBehaviour
    {
        public MenuStateHandler menuStateHandler;
        public SettingsManager settingsManager;
        
        private UIDocument _uiDocument;

        private Button _backButton;
        
        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            _uiDocument.enabled = true;
            _uiDocument.rootVisualElement.visible = false;
            
            _backButton = _uiDocument.rootVisualElement.Q<Button>("Back");

            _uiDocument.rootVisualElement.dataSource = settingsManager; 
            
            _backButton.RegisterCallback<ClickEvent>(_ =>
            {
                Debug.Log("Back button clicked");
                menuStateHandler.CloseSettingsMenu();
            });
        }
    }
}