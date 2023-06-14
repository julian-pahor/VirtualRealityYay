using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Stick : MonoBehaviour
{
    public Transform stickPoint;
    private bool value;
    

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision");

        Marshmallow m;
        if (other.gameObject.TryGetComponent<Marshmallow>(out m))
        {
            m.gameObject.transform.position = stickPoint.position;
            m.transform.parent = this.transform;
        }
    }
}
