using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingLot : MonoBehaviour
{
    public enum ParkingLotState
    {
        leftLine,
        rightLine
    }

    public ParkingLotState lotType;
}
