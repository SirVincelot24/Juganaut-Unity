using JetBrains.Annotations;
using logic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [CanBeNull] public IGameOverReason GameOverReason;
    [CanBeNull] public IWinningReason WinningReason;
    
    public bool IsRunning { get; private set; }

    public int diamondCount;

    public void GameOver(IGameOverReason reason)
    {
        if (!IsRunning) return;
        GameOverReason = reason;
    }

    public void Win(IWinningReason reason)
    {
        WinningReason = reason;
        GameOverReason = null;
        IsRunning = false;
    }
}