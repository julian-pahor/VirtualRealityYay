using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [Tooltip("Radius at which mallows begin to cook.")]
    public float cookRadius;
    float currentCookRadius;
    [Tooltip("Radius at which mallows instantly ignite/burn.")]
    public float flameRadius;

    [Range(0f, 1f)]
    public float fireHealth;
    public float healthReturn;
    public float decayRate;

    public Transform flameObject;
    public ParticleSystem stokeBurst;
    
    public Transform glow;
    Vector3 glowScale;

    Vector3 startPos;
    Vector3 endPos;

    public AudioSource source;


    private void Start()
    {
        startPos = flameObject.transform.position;
        endPos = startPos - new Vector3(0, 2, 0);

        glowScale = glow.localScale;
    }
  

    private void Update()
    {
        fireHealth -= Time.deltaTime * decayRate;
        flameObject.transform.position = Vector3.Lerp(endPos,startPos, fireHealth);

        glow.localScale = glowScale * Mathf.Lerp(.01f,1, fireHealth);

        currentCookRadius = Mathf.Lerp(flameRadius,cookRadius,fireHealth); 
        fireHealth = Mathf.Clamp(fireHealth, 0, 1);
    }

    /// <summary>
    /// returns 3d point as percent value between or cooking max and min
    /// </summary>
    public float CookStrength(Vector3 position)
    {
        //distance from inner sphere
        Vector2 length = position - transform.position;
        float distFromMin = Vector3.SqrMagnitude(length) - (flameRadius*flameRadius);

        float totalDistance = (cookRadius*cookRadius) - (flameRadius*flameRadius);

        float percentage = distFromMin / totalDistance;
        if (distFromMin <= 0) percentage = 0;
        if(distFromMin/totalDistance > 1) percentage = 1;

        return 1 - percentage;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, currentCookRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, flameRadius);
    }
   
    private void OnTriggerEnter(Collider other)
    {
        Stick collidingStick = other.GetComponent<Stick>();
        if (collidingStick == null) return;

        source.pitch = Random.Range(0.9f, 1.1f);
        source.volume = Random.Range(0.35f, 0.45f);
        source.Play();

        fireHealth += healthReturn;
        if (stokeBurst != null)
            stokeBurst.Play();
    }
}
