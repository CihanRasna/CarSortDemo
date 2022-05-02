using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    [SerializeField] private Material carMaterial1;
    [SerializeField] private Material carMaterial2;
    [SerializeField] private Material parkingLotMaterial1;
    [SerializeField] private Material parkingLotMaterial2;

    [SerializeField] private Color _color1;
    [SerializeField] private Color _color2;
    
    private void Start()
    {
        carMaterial1.color = parkingLotMaterial1.color = _color1;
        carMaterial2.color = parkingLotMaterial2.color = _color2;
    }
}
