using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject buttonPanel;
    [SerializeField] private GameObject succeedPanel;
    [SerializeField] private GameObject failedPanel;

    void Start()
    {
        SessionManager.Instance.currentStatus.AddListener(StatusListener);
    }

    private void StatusListener(SessionManager.GameState state)
    {
        if (state == SessionManager.GameState.Started)
        {
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
