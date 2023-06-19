using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [Tooltip("Radius at which mallows begin to cook.")]
    public float cookRadius;
    [Tooltip("Radius at which mallows instantly ignite/burn.")]
    public float flameRadius;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// returns 3d point as percent value between or cooking max and min
    /// </summary>
    public float CookStrength(Vector3 position)
    {
        //distance from inner sphere
        float distFromMin = Vector3.Distance(position,transform.position) - flameRadius;

        float totalDistance = cookRadius - flameRadius;

        float percentage = distFromMin / totalDistance;
        if (distFromMin <= 0) percentage = 0;
        if(distFromMin/totalDistance > 1) percentage = 1;

        return 1 - percentage;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, cookRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, flameRadius);
    }
}
