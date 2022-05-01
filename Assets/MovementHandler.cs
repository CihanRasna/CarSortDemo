using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    [SerializeField] private LotHandler lotHandler;
    [SerializeField] private CarManager carManager;

    public bool left;
    public bool right;
    private void Update()
    {
        if (left)
        {
            left = false;
            //var pos = lotHandler.LeftLineParks[0].transform.position;
            lotHandler.LotCleaner(1,out var pos);
            carManager.MoveSelectedCar(1, out var car);
            car.StartMovement(pos);
        }

        if (right)
        {
            right = false;
            //var pos = lotHandler.RightLineParks[0].transform.position;
            lotHandler.LotCleaner(2, out var pos);
            carManager.MoveSelectedCar(2, out var car);
            car.StartMovement(pos);
        }
    }
}
