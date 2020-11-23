using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeManager : MonoBehaviour
{
    public AudioClip audioClip;
    OVRHapticsClip hapticsClip;

    void Start()
    {
        hapticsClip = new OVRHapticsClip(audioClip);
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "rock")
        {
            FindObjectOfType<jp.yzroid.CsgUnitySweeper.BlockManager>().OnLeftClick(collision.transform);
            Debug.Log(FindObjectOfType<jp.yzroid.CsgUnitySweeper.BlockManager>());
            OVRHaptics.RightChannel.Mix(hapticsClip);
        }
    }
}
