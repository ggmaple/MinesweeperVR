using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftController : MonoBehaviour
{
    [SerializeField]
    private GameObject flag;
    [SerializeField]
    public bool Left = false;

    public void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.LHandTrigger) && Left)
        {
            flag.SetActive(true);
        }
        else
        {
            flag.SetActive(false);
        }
    }

    public void OnLeft()
    {
        Left = true;
        //Debug.Log("OnRight 呼び出し");
    }

    public void OffLeft()
    {
        Left = false;
        //Debug.Log("OffRight 呼び出し");
    }
}
