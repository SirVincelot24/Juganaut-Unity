using System.Linq;
using JetBrains.Annotations;
using logic;
using setup;
using space;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.SceneManagement;
using WorldItems;

public class GameManager : MonoBehaviour
{
    [CanBeNull] public IGameOverReason GameOverReason;
    [CanBeNull] public IWinningReason WinningReason;

    public InputActionReference debugKey;
    public SettingsManager settingsManager;
    public SoundManager soundManager;
    public World World;
    public bool IsRunning { get; private set; } = true;
    public IntVariable diamondCount;
    public IntVariable diamondsInGame;

    public void GameOver(IGameOverReason reason)
    {
        EventBus.Publish(new GameOverEvent(reason));
        if (!IsRunning) return;
        GameOverReason = reason;
        soundManager.StopMusic();
        soundManager.PlaySfx(SfxType.GameOver);
    }

    public void Win(IWinningReason reason)
    {
        EventBus.Publish(new WinningEvent(reason));
        WinningReason = reason;
        GameOverReason = null;
        IsRunning = false;
        soundManager.StopMusic();
        soundManager.PlaySfx(SfxType.Win);
    }

    private void Update()
    {
        if (debugKey.action.triggered)
        {
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Game")
            return;
        
        World = WorldBuilder.CreateWorld(
            settingsManager.Width, settingsManager.Height,
            settingsManager.Diamonds,
            settingsManager.Monster,
            settingsManager.Bombs,
            settingsManager.Rocks,
            new Coord(Mathf.FloorToInt(settingsManager.Width / 2), Mathf.FloorToInt(settingsManager.Height / 2))
            );
        diamondsInGame.Value = World.Count(type => type == WorldItemType.Diamond);
        World.SpawnItems(0.5f);
        
    }
}