using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

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

    public void LotSelector(bool isLeftButtonTapped, out ParkingLot parkingLot)
    {
        if (isLeftButtonTapped)
        {
            parkingLot = leftLineParks[0];
            leftLineParks.RemoveAt(0);
        }
        else
        {
            parkingLot = rightLineParks[0];
            rightLineParks.RemoveAt(0);
        }
    }
}
