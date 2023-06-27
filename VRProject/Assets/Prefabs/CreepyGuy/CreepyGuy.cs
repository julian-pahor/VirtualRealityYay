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
    public float stalkDistance;
    public float runSpeed;

    float timer;
    enum State { Wander,Seek,Retreat, Wait}
    State state;

    public List<AudioClip> footsteps = new List<AudioClip>();
    public AudioSource source;
    private HorrorStingerAudio horrorAudio;

    //public Transform goal;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null) Debug.LogWarning("Creepy guy needs a target player to creep on");

        navMeshAgent = GetComponent<NavMeshAgent>();
        visiblityDetector = GetComponent<VisiblityDetector>();
        horrorAudio = GetComponent<HorrorStingerAudio>();
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

                if(visiblityDetector.IsVisible())
                {
                    Debug.Log("Creepy Guy: I'm running away.");
                  
                    GetRandomWaypoint();
                    horrorAudio.active = false;
                    navMeshAgent.speed = speed * runSpeed;
                    state = State.Retreat;
                }

                if (player != null)
                {
                    float dist = Vector3.Distance(player.transform.position, transform.position);
                    if (dist < stalkDistance)
                    { 
                    Debug.Log("Creepy Guy: I'm waiting to spook you");
                    navMeshAgent.speed = 0;
                    state = State.Wait;
                    }

                }

                //if(navMeshAgent.remainingDistance < stalkDistance)
                //{
                //  
                //}

                break;

            case State.Wait:

                if (visiblityDetector.IsVisible())
                {
                    Debug.Log("Creepy Guy: I'm running away.");
                    GetRandomWaypoint();
                    navMeshAgent.speed = speed * 4;
                    state = State.Retreat;
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
                    if (!visiblityDetector.IsVisible())
                    {
                        Debug.Log("Creepy Guy: I'm coming to eat you.");
                        navMeshAgent.destination = player.transform.position;
                        horrorAudio.active = true;
                        state = State.Seek;
                    }
                    else
                        Debug.Log("Creepy Guy: You can see me, so I'm not going to hunt.");
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

    public void FootstepEvent()
    {
        source.clip = footsteps[Random.Range(0, footsteps.Count)];
        source.volume = Random.Range(0.9f, 1.0f);
        source.pitch = Random.Range(0.9f, 1.1f);
        source.Play();
        //Debug.Log("YES HELLO ANIMATION EVENT");
    }
}
