using System;
using JetBrains.Annotations;
using logic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [CanBeNull] public IGameOverReason GameOverReason;
    [CanBeNull] public IWinningReason WinningReason;

    public InputActionReference debugKey;
    
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
}