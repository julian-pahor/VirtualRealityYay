using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreepyGuy : MonoBehaviour
{
    private Vector3 destination;
    private NavMeshAgent navMeshAgent;

    [SerializeField] List<GameObject> waypointGOs = new List<GameObject>();

    //public Transform goal;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        GetRandomWaypoint();       
    }

    private void GetRandomWaypoint()
    {
        int rand = Random.Range(0, waypointGOs.Count);
        destination = waypointGOs[rand].transform.position;
        navMeshAgent.destination = destination;
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent.remainingDistance > 0.1f)
        {
            return;
        }
        else
        {
            GetRandomWaypoint();
        }
    }
}
