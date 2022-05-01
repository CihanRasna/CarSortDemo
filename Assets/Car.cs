using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.AI;

public class Car : MonoBehaviour
{
    public enum CarType
    {
        leftLine,
        rightLine
    }
    public CarType carType;
    
    public Renderer myRenderer;
    [SerializeField] private NavMeshAgent agent;
    public List<Vector3> pathPoints;
    [SerializeField] private SplineFollower splineFollower;

    public void Start()
    {   
        var mat = GetComponentInParent<CarManager>().CarMaterial(this);
        myRenderer.material = mat;
        splineFollower.spline = gameObject.AddComponent<SplineComputer>();
        splineFollower.spline.space = SplineComputer.Space.World;
        //splineFollower.spline.SetPoint(0, new SplinePoint(transform.position));
    }

    public void StartMovement(Vector3 to)
    {
        StartCoroutine(SetPath(to));
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

        //agent.enabled = false;
        //splineFollower.follow = true;
        yield return null;
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