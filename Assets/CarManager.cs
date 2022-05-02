using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    [SerializeField] private List<Car> leftLineCars;
    public List<Car> LeftLineCar => leftLineCars;
    [SerializeField] private List<Car> rightLineCars;
    public List<Car> RightLineCars => rightLineCars;

    [SerializeField] private Material leftCarMaterial;
    [SerializeField] private Material rightCarMaterial;

    public Material CarMaterial(Car car)
    {
        return car.carType == Car.CarType.leftLine ? leftCarMaterial : rightCarMaterial;
    }

    public void MoveSelectedCar(bool isLeftButtonTapped, out Car car)
    {
        if (isLeftButtonTapped)
        {
            car = leftLineCars[0];
            leftLineCars.RemoveAt(0);
        }
        else
        {
            car = rightLineCars[0];
            rightLineCars.RemoveAt(0);
        }
    }
}