using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shtock : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Drum d;

        if(collision.gameObject.TryGetComponent<Drum>(out d))
        {
            d.PlayDrum();
        }
    }
}
