using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    [SerializeField]
    private GameObject _flag;

    bool flag = false;

    public AudioClip audioClip;
    OVRHapticsClip hapticsClip;

    void Start()
    {
        hapticsClip = new OVRHapticsClip(audioClip);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "flag")
        {
            if (!flag)
            {
                //Debug.Log("fffff");
                OVRHaptics.LeftChannel.Mix(hapticsClip);
                _flag.SetActive(true);
                GetComponent<BoxCollider>().enabled = false;
                GetComponent<CapsuleCollider>().enabled = false;
                StartCoroutine("time1");
                

            }
            else if (flag)
            {
                _flag.SetActive(false);
                GetComponent<BoxCollider>().enabled = true;
                GetComponent<CapsuleCollider>().enabled = true;
                StartCoroutine("time2");
            }

        }

    }

    IEnumerator time1()
    {
        yield return new WaitForSeconds(1);
        flag = true;

    }
    IEnumerator time2()
    {
        yield return new WaitForSeconds(1);
        flag = false;

    }

}
