using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform[] waypoints;
    int waypointsIndex;
    Vector2 target;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, target) < 1)
        {
            IterateWaypointIndex();
            UpdateDestination();
        }
    }
    void UpdateDestination()
    {
        target = waypoints[waypointsIndex].position;
        agent.SetDestination(target);
    }
    void IterateWaypointIndex()
    {
        waypointsIndex++;
        if(waypointsIndex == waypoints.Length)
        {
            waypointsIndex =0;
        }
    }

    void OnTriggerStay(UnityEngine.Collider other)
    {
        if (other.gameObject)
        {
            
        }

    }
}    
