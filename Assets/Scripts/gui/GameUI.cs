using System;
using logic;
using ScenePersistence;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.UIElements;

namespace gui
{
    public class GameUI : MonoBehaviour
    {
        public GameManager gameManager;
        
        private UIDocument _uiDocument;
        
        private Button _stopButton;
        private Label _diamondLabel;
        private VisualElement _gameOverContainer;
        private VisualElement _winningContainer;
        private Label _gameOverDesc;
        private Label _winDesc;
        private void Awake()
        {
            _uiDocument = GetComponent<UIDocument>();
            
            _stopButton = _uiDocument.rootVisualElement.Q<Button>("Stop");
            _diamondLabel = _uiDocument.rootVisualElement.Q<Label>("DiamondLabel");
            _gameOverContainer = _uiDocument.rootVisualElement.Q<VisualElement>("GameOverContainer");
            _winningContainer = _uiDocument.rootVisualElement.Q<VisualElement>("WinContainer");
            _gameOverDesc = _uiDocument.rootVisualElement.Q<Label>("GameOverDesc");
            _winDesc = _uiDocument.rootVisualElement.Q<Label>("WinDesc");
            
            _stopButton.RegisterCallback<ClickEvent>(_ => SceneChanger.ChangeSceneNow("MainMenu"));

            _diamondLabel.dataSourceType = typeof(LocalizedString);
            _gameOverDesc.dataSourceType = typeof(LocalizedString);
            _winDesc.dataSourceType = typeof(LocalizedString);
            _uiDocument.rootVisualElement.dataSource = gameManager;

            var diamondLabelText = new LocalizedString("Game", "diamondCount");
            diamondLabelText.Add("diamondCount", gameManager.diamondCount);
            diamondLabelText.Add("diamondsInGame", gameManager.diamondsInGame);
            _diamondLabel.SetBinding("text", diamondLabelText);
        }
        
        private void OnEnable()
        {
            EventBus.Subscribe<GameOverEvent>(OnGameOver);
            EventBus.Subscribe<WinningEvent>(OnWinning);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe<GameOverEvent>(OnGameOver);
            EventBus.Unsubscribe<WinningEvent>(OnWinning);
        }
        
        private void OnGameOver(GameOverEvent gameOverEvent)
        {
            _gameOverContainer.visible = true;
            _gameOverDesc.SetBinding("text", FormatReason(gameOverEvent.Reason));
        }
        
        private void OnWinning(WinningEvent winningEvent)
        {
            _winningContainer.visible = true;
            _winDesc.SetBinding("text", FormatReason(winningEvent.Reason)); ;
        }

        private LocalizedString FormatReason(IWinningReason reason)
        {
            LocalizedString reasonText;
            switch(reason)
            {
                case AllDiamondsCollected collectedReason: 
                    var diamonds = new IntVariable { Value = collectedReason.DiamondsCollected };
                    reasonText = new LocalizedString("Game", "win.all_diamonds");
                    reasonText.Add("diamonds", diamonds);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(reason), reason, null);
            }
            return reasonText;
        }

        private LocalizedString FormatReason(IGameOverReason reason)
        {
            return new LocalizedString("Game", reason switch
            {
                RockHitsPlayer =>  "death.rock_hits_player",
                PlayerWalksIntoMonster => "death.player_walks_into_monster",
                MonsterCatchesPlayer => "death.monster_catches_player",
                Explosion => "death.explosion",
                _ => throw new ArgumentOutOfRangeException(nameof(reason), reason, null)
            });
        }
    }
}