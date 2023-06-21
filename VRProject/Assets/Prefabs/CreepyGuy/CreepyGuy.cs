using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreepyGuy : MonoBehaviour
{
    private Vector3 destination;
    private NavMeshAgent navMeshAgent;

    [SerializeField] List<GameObject> waypointGOs = new List<GameObject>();

    VisiblityDetector visiblityDetector;

    public Transform player;

    public float speed;
    public float huntTime;

    float timer;

    enum State { Wander,Seek,Retreat}
    State state;

    //public Transform goal;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null) Debug.LogWarning("Creepy guy needs a target player to creep on");

        navMeshAgent = GetComponent<NavMeshAgent>();
        visiblityDetector = GetComponent<VisiblityDetector>();
        navMeshAgent.speed = speed;
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
        switch(state)
        {
            case State.Seek:

                if(visiblityDetector.IsVisible() || navMeshAgent.remainingDistance < 1)
                {
                    
                    state = State.Retreat;
                    GetRandomWaypoint();
                    navMeshAgent.speed = speed * 4;
                }


                break;
            case State.Retreat:

                if (navMeshAgent.remainingDistance < 0.1f)
                {
                    navMeshAgent.speed = speed;
                    state = State.Wander;
                }

                break;
            case State.Wander:

                timer += Time.deltaTime;
                if(timer > huntTime)
                {
                    //don't try this if we have no player
                    if (player == null)
                        return;

                    timer = 0;
                    state = State.Seek;
                    navMeshAgent.destination = player.transform.position;
                }

                //thank you gamedev dustin
                if (navMeshAgent.remainingDistance > 0.1f)
                {
                    return;
                }
                else
                {
                    GetRandomWaypoint();
                }
                break;
        }

      
    }
}
