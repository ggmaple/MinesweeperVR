using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeColliderController : MonoBehaviour
{
    public void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.RHandTrigger))
        {
            GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
