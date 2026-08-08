 using System;
using ScenePersistence;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace gui
{
    public class MenuStateHandler : MonoBehaviour
    {
        public InputActionReference pauseKey;
        
        public SettingsManager settingsManager;
    
        public MenuState menuState;
        
        [Header("UI Documents")]
        public UIDocument settingsUI;
        public UIDocument otherUI;

        private void Start()
        {
            menuState = SceneManager.GetActiveScene().name == "MainMenu" ? MenuState.MainMenu : MenuState.Game;
        }
    
        private void Update()
        {
            if (!pauseKey.action.triggered) return;
            switch (menuState)
            {
                case MenuState.MainMenu:
                    break;
                case MenuState.Game:
                    // open pause menu
                    OpenSettingsMenu();
                    break;
                case MenuState.SettingsMenu:
                    CloseSettingsMenu();
                    menuState = SceneManager.GetActiveScene().name == "MainMenu" ? MenuState.MainMenu : MenuState.Game;
                    break;
                case MenuState.PauseMenu:
                    // Resume
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        
        }
        
        public void OpenSettingsMenu()
        {
            otherUI.rootVisualElement.visible = false;
            settingsUI.rootVisualElement.visible = true;
            settingsManager.LoadSettings();
            menuState = MenuState.SettingsMenu;
        }
        
        public void CloseSettingsMenu()
        {
            otherUI.rootVisualElement.visible = true;
            settingsUI.rootVisualElement.visible = false;
            settingsManager.SaveSettings();
        }
        
        public void StartGame()
        {
            SceneChanger.ChangeSceneNow("Game");
        }
        
        public void QuitGame()
        {
            Application.Quit();
        }
    }

    public enum MenuState
    {
        MainMenu,
        Game,
        PauseMenu,
        SettingsMenu
    }
}