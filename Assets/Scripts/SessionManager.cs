using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class SessionManager : Singleton<SessionManager>
{
    [SerializeField] private List<Level> levels;
    public Level currentLevel;
    
    public enum GameState
    {
        Started,
        Succeed,
        Failed,
        None
    }

    private GameState _state = GameState.None;
    
    public UnityEvent<GameState> currentStatus;
    public GameState State
    {
        get => _state;
        set
        {
            _state = value;
            currentStatus.Invoke(_state);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        currentLevel = Instantiate(levels[0], Vector3.zero,Quaternion.identity);
    }

    private void Start()
    {
        State = GameState.Started;
    }

    public void LoadNextLevel() // Same Level actually
    {
        Destroy(currentLevel.gameObject);
        currentLevel = Instantiate(levels[0], Vector3.zero,Quaternion.identity);
        State = GameState.Started;
        DOTween.KillAll();
    }
}
