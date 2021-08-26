using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaycastShooting : MonoBehaviour
{
    public GameObject player;
    public Animator animate;

    public GameObject playerHand;
    public GameObject throwable;
    public float throwForce;
    public bool isHolding = false;

    public float range = 15f;
    public LayerMask hitLayer;
    public GameObject startingPoint;

    public Camera thirdPersonCam;


    void Update()
    {
        //call method when the E key is pressed.
        if (Input.GetKeyDown(KeyCode.E))
        {
            outGoesTheRaycast();
            
        }
        
        
        if(isHolding == true)
        {
            //when holding the throwable object set its position to the players hand and keep it still
            throwable.GetComponent<Rigidbody>().velocity = Vector3.zero;
            throwable.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            throwable.GetComponent<Rigidbody>().useGravity = false;
            throwable.transform.rotation = playerHand.transform.rotation;
            throwable.transform.position = playerHand.transform.position;
            throwable.transform.parent = playerHand.transform;

            //get rid of text when holding the throwable object.
            if(GameObject.FindGameObjectWithTag("FloatingRockText") != null)
            {
                GameObject.FindGameObjectWithTag("FloatingRockText").SetActive(false);
            }
            //pressing the F key while holding is true throws the object forward.
            if (Input.GetKeyDown(KeyCode.F))
            {
                throwable.GetComponent<Rigidbody>().AddForce(playerHand.transform.forward * throwForce);
                isHolding = false;
            }
        }
        //object stays where it was spawned if isholding == false and keeps it false until changed.
        else
        {
            throwable.transform.parent = null;
            throwable.GetComponent<Rigidbody>().useGravity = true;
            isHolding = false;
        }
    }
    //sends out raycast to check if it is a throwable object, if true then run above, if(isholding == true), script.
    void outGoesTheRaycast()
    {
        if (Physics.Raycast(startingPoint.transform.position, startingPoint.transform.forward, out RaycastHit hit, range, hitLayer))
        {
            isHolding = true;
        }
    }

}
