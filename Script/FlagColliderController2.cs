using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagColliderController2 : MonoBehaviour
{
    public void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.LHandTrigger))
        {
            GetComponent<CapsuleCollider>().enabled = true;
        }
        else
        {
            GetComponent<CapsuleCollider>().enabled = false;
        }
    }
}

