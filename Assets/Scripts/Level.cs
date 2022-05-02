using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public MovementHandler MovementHandler;
    public LotHandler LotHandler;
    public CarManager CarManager;


    public void CheckIfLevelFinished()
    {
        var lotCount = LotHandler.LeftLineParks.Count + LotHandler.RightLineParks.Count;
        var carCount = CarManager.LeftLineCars.Count + CarManager.RightLineCars.Count;
        if (lotCount == 0 && carCount == 0)
        {
            SessionManager.Instance.State = SessionManager.GameState.Succeed;
        }
    }
}
