using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject buttonPanel;
    [SerializeField] private GameObject succeedPanel;
    [SerializeField] private GameObject failedPanel;

    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    
    

    private void Awake()
    {
        SessionManager.Instance.currentStatus.AddListener(StatusListener);
    }
    public void IsLeftButtonTapped(bool isTrue)
    {
        var level = SessionManager.Instance.currentLevel;
        level.MovementHandler.ButtonBehaviour(isTrue);
    }

    private void StatusListener(SessionManager.GameState state)
    {
        if (state == SessionManager.GameState.Started)
        {
            Debug.Log("Started");
            succeedPanel.SetActive(false);
            failedPanel.SetActive(false);
        }
        
        if (state == SessionManager.GameState.Succeed)
        {
            succeedPanel.SetActive(true);
        }

        if (state == SessionManager.GameState.Failed)
        {
            failedPanel.SetActive(true);
        }
    }
}
