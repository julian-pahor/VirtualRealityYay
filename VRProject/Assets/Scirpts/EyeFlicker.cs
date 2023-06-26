using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EyeFlicker : MonoBehaviour
{
    private float hideTime;
    private float hideMinTime = 6;
    private float hideMaxTime = 20;

    private float appearTime;
    private float appearMinTime = 0.5f;
    private float appearMaxTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartFlicker();
    }

    private void StartFlicker()
    {
        //generates the hide and appear times for the eyes
        hideTime = Random.Range(hideMinTime, hideMaxTime);
        appearTime = Random.Range(appearMinTime, appearMaxTime);

        StartCoroutine(FlickerTimer());

    }

    IEnumerator FlickerTimer()
    {
        //sets transparent
        gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 0);
        yield return new WaitForSeconds(hideTime);

        //sets visible
        gameObject.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 1);
        yield return new WaitForSeconds(appearTime);

        StartFlicker();
        
        StopCoroutine(FlickerTimer());
       
    }
}
