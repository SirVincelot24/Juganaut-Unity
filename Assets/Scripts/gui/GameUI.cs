using ScenePersistence;
using UnityEngine;
using UnityEngine.UIElements;

namespace gui
{
    public class GameUI : MonoBehaviour
    {
        public GameManager gameManager;
        
        private UIDocument _uiDocument;
        
        private Button _stopButton;
        private Label _gameOverDesc;
        private Label _winDesc;
        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            
            _stopButton = _uiDocument.rootVisualElement.Q<Button>("Stop");
            _gameOverDesc = _uiDocument.rootVisualElement.Q<Label>("GameOverDesc");
            _winDesc = _uiDocument.rootVisualElement.Q<Label>("WinDesc");
            
            _stopButton.RegisterCallback<ClickEvent>(_ => SceneChanger.ChangeSceneNow("MainMenu"));
        }
    }
}