using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ParkingLot : MonoBehaviour
{
    public NavMeshObstacle NavMeshObstacle;
    public enum ParkingLotState
    {
        leftLine,
        rightLine
    }

    public ParkingLotState lotType;
}
