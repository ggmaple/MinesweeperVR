using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagColliderController : MonoBehaviour
{
    public void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.LHandTrigger))
        {
            GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
