using System.Linq;
using JetBrains.Annotations;
using logic;
using setup;
using space;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [CanBeNull] public IGameOverReason GameOverReason;
    [CanBeNull] public IWinningReason WinningReason;

    public InputActionReference debugKey;
    public SettingsManager settingsManager;
    public World World;
    public bool IsRunning { get; private set; }
    public int diamondCount;

    public void GameOver(IGameOverReason reason)
    {
        EventBus.Publish(new GameOverEvent(reason));
        if (!IsRunning) return;
        GameOverReason = reason;
    }

    public void Win(IWinningReason reason)
    {
        EventBus.Publish(new WinningEvent(reason));
        WinningReason = reason;
        GameOverReason = null;
        IsRunning = false;
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
            new Coord(settingsManager.Width / 2, settingsManager.Height / 2)
            );
        World.SpawnItems();
    }
}