using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GoblinController : MonoBehaviour
{
    public NavMeshAgent agent;

    [SerializeField]
    GameObject[] points;

    float pointDetectionMargin = 1.5f;
    Vector3 destination;

    private void Start()
    {
        destination = points[Random.Range(0, points.Length - 1)].transform.position;
        agent.SetDestination(destination);
        transform.forward = Vector3.left;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, destination) > pointDetectionMargin)
            return;
            
        destination = points[Random.Range(0, points.Length - 1)].transform.position;
        agent.SetDestination(destination);
    }
}
