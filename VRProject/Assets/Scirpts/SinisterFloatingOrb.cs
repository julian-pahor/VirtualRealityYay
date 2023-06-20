using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinisterFloatingOrb : MonoBehaviour
{

    VisiblityDetector detector;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        detector = GetComponent<VisiblityDetector>();
    }

    // Update is called once per frame
    void Update()
    {
        if(detector.IsVisible())
        {
            Vector3 direction = (detector.ViewerPosition() - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
