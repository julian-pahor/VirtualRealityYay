using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EyeFlicker : MonoBehaviour
{
    private Color alpha;
    private float timeTillNext;
    private float minTime = 0;
    private float maxTime = 2;
    // Start is called before the first frame update
    void Start()
    {
        alpha = gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartFlicker()
    {
        timeTillNext = Random.Range(minTime, maxTime);


    }

    //IEnumerator FlickerTimer()
    //{
    //    WaitForSeconds(timeTillNext);
    //}
}
