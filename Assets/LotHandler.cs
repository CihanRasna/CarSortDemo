using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LotHandler : MonoBehaviour
{
    [SerializeField] private List<ParkingLot> leftLineParks;
    [SerializeField] private List<ParkingLot> rightLineParks;
    [SerializeField] private Material lotType1Material;
    [SerializeField] private Material lotType2Material;
    
    public List<ParkingLot> LeftLineParks => leftLineParks;
    public List<ParkingLot> RightLineParks => rightLineParks;

    public void LotSorter(ParkingLot lot)
    {
        if (lot.lotType == ParkingLot.ParkingLotState.leftLine)
        {
            leftLineParks.Insert(0,lot);
        }
        else
        {
            rightLineParks.Insert(0,lot);
        }
    }

    public void LotCleaner(int a, out Vector3 parkingLot)
    {
        if (a == 1)
        {
            parkingLot = leftLineParks[0].transform.position;
            leftLineParks.RemoveAt(0);
        }
        else
        {
            parkingLot = rightLineParks[0].transform.position;
            rightLineParks.RemoveAt(0);
        }
    }
}
