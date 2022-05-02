using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
    public List<Vector3> pathPoints;
    [SerializeField] private SplineFollower splineFollower;
    private bool testMe;

    public void Start()
    {   
        var mat = GetComponentInParent<CarManager>().CarMaterial(this);
        myRenderer.material = mat;
        splineFollower.spline = gameObject.AddComponent<SplineComputer>();
        splineFollower.spline.space = SplineComputer.Space.World;
        //splineFollower.spline.SetPoint(0, new SplinePoint(transform.position));
    }

    private void Update()
    {
        if (testMe) return;

        if (agent.pathStatus == NavMeshPathStatus.PathPartial)
        {
            Debug.LogError(gameObject.name +"'S PATH CAN NOT REACHABLE");
        }
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

        agent.enabled = false;
        splineFollower.follow = true;
        _parkingLot.NavMeshObstacle.enabled = true;
        yield return null;
    }

    public void OnReached()
    {
        transform.parent = _parkingLot.transform;
        var rot = transform.localRotation.eulerAngles;
        Destroy(splineFollower.spline);
        Destroy(splineFollower);
        transform.DOLocalRotate(new Vector3(90, rot.y, rot.z), 0.3f);
        testMe = true;
    }

    private void OnDrawGizmos()
    {
        var agentPath = pathPoints;
        foreach (var t in agentPath)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(t, Vector3.one * 2f);
        }
    }
}