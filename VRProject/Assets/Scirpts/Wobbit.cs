using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.OpenXR.Input;

public class Wobbit : MonoBehaviour
{
  
    public static Wobbit instance;

    private void Awake()
    {
        instance = this;
    }


    public InputAction trigger1;
    public InputAction trigger2;


    public Transform rightHand;

    public GameObject heldItem;
    public LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        //started += Test;   
    }

    private void OnEnable()
    {
        trigger1.Enable();
    }

    private void OnDisable()
    {
        trigger1.Disable();
    }

    // Update is called once per frame
    void Update()
    {
      

        if (heldItem != null)
        {
            heldItem.transform.localPosition = new Vector3(0, 0, 0);
            heldItem.transform.localRotation = Quaternion.LookRotation(rightHand.transform.forward);
        }
        if (trigger1.WasReleasedThisFrame())
        {
            Debug.Log("Released primary button");
        }
      
    }

    void Test(InputAction.CallbackContext context)
    {
        Debug.Log("NO IDEA WHAT I'M DOING");

        RaycastHit hit;

        Physics.Raycast(rightHand.transform.position, rightHand.transform.forward, out hit, Mathf.Infinity, mask);
        if(hit.collider != null)
        {
            Debug.Log("hit detected");
            Debug.Log(hit.collider.gameObject.name);
            hit.collider.gameObject.transform.SetParent(rightHand, false);
            hit.collider.gameObject.transform.localPosition = new Vector3 (0,0,0);
            hit.collider.gameObject.transform.localRotation = Quaternion.LookRotation(rightHand.transform.forward);
            heldItem = hit.collider.gameObject;
        }

    }

}
