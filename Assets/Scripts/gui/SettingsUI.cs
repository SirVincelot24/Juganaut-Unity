using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace gui
{
    public class SettingsUI : MonoBehaviour
    {
        public MenuStateHandler menuStateHandler;
        
        private UIDocument _uiDocument;

        private Button _backButton;
        
        private void OnEnable()
        {
            _uiDocument = GetComponent<UIDocument>();
            _uiDocument.enabled = true;
            _uiDocument.rootVisualElement.visible = false;
            
            _backButton = _uiDocument.rootVisualElement.Q<Button>("Back");
            
            _backButton.RegisterCallback<ClickEvent>(_ => menuStateHandler.CloseSettingsMenu());
        }
    }
}