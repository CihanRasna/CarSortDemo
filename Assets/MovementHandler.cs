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
        var manager = SessionManager.Instance;
        //if (!leftSideValid || !rightSideValid) return;

        if (isLeftButtonTapped && carManager.LeftLineCars.Count <= 0 || !isLeftButtonTapped && carManager.RightLineCars.Count <= 0)
        {
            return;
        }

        if (isLeftButtonTapped && leftSideValid)
        {
            leftSideValid = false;
            var rotationEuler = leftBar.localRotation.eulerAngles;
            leftBar.DOLocalRotate(new Vector3(rotationEuler.x, rotationEuler.y, -60f), 0.5f).SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    if (carManager.LeftLineCars.Count > 0 && manager.State == SessionManager.GameState.Started)
                    {
                        carManager.LeftLineCars[0].transform.DOLocalMove(carManager._initialPosLeft, 0.2f)
                            .OnComplete(() => leftSideValid = true).SetEase(Ease.InOutSine);
                    }
                    else
                    {
                        leftSideValid = true;
                    }
                });
                    
            leftButton.DOLocalMoveY(-5, 0.3f).SetLoops(2, LoopType.Yoyo);
        }
        else if (!isLeftButtonTapped && rightSideValid)
        {
            rightSideValid = false;
            var rotationEuler = rightBar.localRotation.eulerAngles;
            rightBar.DOLocalRotate(new Vector3(rotationEuler.x, rotationEuler.y, -60f), 0.5f).SetLoops(2, LoopType.Yoyo)
                .SetEase(Ease.InOutSine)
                .OnComplete(() =>
                {
                    if (carManager.RightLineCars.Count > 0 && manager.State == SessionManager.GameState.Started)
                    {
                        carManager.RightLineCars[0].transform.DOLocalMove(carManager._initialPosRight, 0.2f)
                            .OnComplete(() => rightSideValid = true).SetEase(Ease.InOutSine);
                    }
                    else
                    {
                        rightSideValid = true;
                    }
                });

            rightButton.DOLocalMoveY(-5, 0.3f).SetLoops(2, LoopType.Yoyo);
        }
        else return;
        
        
        lotHandler.LotSelector(isLeftButtonTapped, out var parkingLot);
        carManager.MoveSelectedCar(isLeftButtonTapped, out var car);

        if (parkingLot == null || car == null)
        {
            Debug.Log("CAN'T PARK");
            SessionManager.Instance.State = SessionManager.GameState.Failed;
            return;
        }

        car.StartMovement(parkingLot);
    }
}