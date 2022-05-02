using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    [SerializeField] private LotHandler lotHandler;
    [SerializeField] private CarManager carManager;
    [SerializeField] private Transform leftBar;
    [SerializeField] private Transform rightBar;
    [SerializeField] private Transform leftButton;
    [SerializeField] private Transform rightButton;

    private bool leftSideValid = true;
    private bool rightSideValid = true;

    public void ButtonBehaviour(bool isLeftButtonTapped)
    {
        if (isLeftButtonTapped)
        {
            if (!leftSideValid) return;
            leftSideValid = false;
            var rotationEuler = leftBar.localRotation.eulerAngles;
            leftBar.DOLocalRotate(new Vector3(rotationEuler.x, rotationEuler.y, -60f), 1f).SetLoops(2, LoopType.Yoyo)
                .OnComplete((() => leftSideValid = true));
            leftButton.DOLocalMoveY(-5, 0.3f).SetLoops(2, LoopType.Yoyo);
        }
        else
        {
            if (!rightSideValid) return;
            rightSideValid = false;
            var rotationEuler = rightBar.localRotation.eulerAngles;
            rightBar.DOLocalRotate(new Vector3(rotationEuler.x, rotationEuler.y, -60f), 1f).SetLoops(2, LoopType.Yoyo)
                .OnComplete((() => rightSideValid = true));
            rightButton.DOLocalMoveY(-5, 0.3f).SetLoops(2, LoopType.Yoyo);
        }

        lotHandler.LotSelector(isLeftButtonTapped, out var pos);
        carManager.MoveSelectedCar(isLeftButtonTapped, out var car);
        car.StartMovement(pos);
    }
}