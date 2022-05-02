using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SessionManager : Singleton<SessionManager>
{
    public enum GameState
    {
        Started,
        Succeed,
        Failed
    }

    private GameState _state;
    
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

    private void Start()
    {
        State = GameState.Started;
    }
}
