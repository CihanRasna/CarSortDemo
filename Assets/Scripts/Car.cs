using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Cinemachine;
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.AI;
using Debug = UnityEngine.Debug;

public class Car : MonoBehaviour
{
    public enum CarType
    {
        leftLine,
        rightLine
    }
    public CarType carType;
    public Renderer myRenderer;
    public ParkingLot _parkingLot;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject tick;
    private List<Vector3> pathPoints = new List<Vector3>();
    [SerializeField] private SplineFollower splineFollower;
    public bool reached = false;

    public void Start()
    {   
        var mat = GetComponentInParent<CarManager>().CarMaterial(this);
        myRenderer.material = mat;
        splineFollower.spline = gameObject.AddComponent<SplineComputer>();
        splineFollower.spline.space = SplineComputer.Space.World;
        //splineFollower.spline.SetPoint(0, new SplinePoint(transform.position));
    }

    public void StartMovement(ParkingLot to)
    {
        _parkingLot = to;
        var pos = to.transform.position;
        StartCoroutine(SetPath(pos));
    }

    IEnumerator SetPath(Vector3 to)
    {
        agent.enabled = true;
        agent.SetDestination(to);
        yield return null;
        for (var i = 0; i < agent.path.corners.Length; i++)
        {
            var pos = new Vector3(agent.path.corners[i].x, transform.position.y, agent.path.corners[i].z);
            pathPoints.Add(pos);
            var splinePoint = new SplinePoint(pos);
            splineFollower.spline.SetPoint(i,splinePoint);
        }
        
        if (agent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            splineFollower.follow = false;
            transform.DOScale(Vector3.one * 1.3f, 0.2f).SetLoops(-1,LoopType.Yoyo);
            _parkingLot.transform.DOScale(_parkingLot.transform.localScale * 1.1f, 0.2f).SetLoops(-1, LoopType.Yoyo);
            _parkingLot.GetComponent<SpriteRenderer>().DOColor(Color.red, 0.2f).SetLoops(-1, LoopType.Yoyo);
            SessionManager.Instance.State = SessionManager.GameState.Failed;
            yield break;
        }

        agent.enabled = false;
        splineFollower.follow = true;
        _parkingLot.NavMeshObstacle.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!reached)
        {
            var otherCar = other.GetComponent<Car>();
            if (!otherCar.reached && !reached)
            {
                SessionManager.Instance.State = SessionManager.GameState.Failed;
            }
        }
    }

    public void OnReached()
    {
        reached = true;
        tick.gameObject.SetActive(true);
        transform.parent = _parkingLot.transform;
        var rot = transform.localRotation.eulerAngles;
        Destroy(splineFollower.spline);
        Destroy(splineFollower);
        splineFollower = null;
        transform.DOLocalRotate(new Vector3(90, rot.y, rot.z), 0.3f);
        SessionManager.Instance.currentLevel.CheckIfLevelFinished();
    }

    private void OnDrawGizmos()
    {
        if (!reached)
        {
            var agentPath = pathPoints;
            foreach (var t in agentPath)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(t, 2f);
            }
        }
    }
}