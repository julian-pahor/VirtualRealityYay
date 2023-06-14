using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Wobbit : MonoBehaviour
{
  

    public InputAction test;
    // Start is called before the first frame update
    void Start()
    {
        test.started += Test;   
    }

    private void OnEnable()
    {
        test.Enable();
    }

    private void OnDisable()
    {
        test.Disable();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void Test(InputAction.CallbackContext context)
    {
        Debug.Log("NO IDEA WHAT I'M DOING");
    }
}
