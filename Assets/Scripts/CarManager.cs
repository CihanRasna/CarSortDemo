using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    [SerializeField] private List<Car> leftLineCars;
    public List<Car> LeftLineCars => leftLineCars;
    [SerializeField] private List<Car> rightLineCars;
    public List<Car> RightLineCars => rightLineCars;

    [SerializeField] private Material leftCarMaterial;
    [SerializeField] private Material rightCarMaterial;

    [HideInInspector] public Vector3 _initialPosLeft;
    [HideInInspector] public Vector3 _initialPosRight;

    private void Start()
    {
        _initialPosLeft = leftLineCars[0].transform.position;
        _initialPosRight = rightLineCars[0].transform.position;
    }

    public Material CarMaterial(Car car)
    {
        return car.carType == Car.CarType.leftLine ? leftCarMaterial : rightCarMaterial;
    }

    public void MoveSelectedCar(bool isLeftButtonTapped, out Car car)
    {
        if (isLeftButtonTapped && leftLineCars.Count > 0)
        {
            car = leftLineCars[0];
            leftLineCars.RemoveAt(0);
        }
        else if (rightLineCars.Count > 0)
        {
            car = rightLineCars[0];
            rightLineCars.RemoveAt(0);
        }
        else
        {
            car = null;
        }
    }
}