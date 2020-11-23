using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using jp.yzroid.CsgUnitySweeper;


public class BGMController : MonoBehaviour
{
    private SceneMain mSceneMain;
   

    public AudioClip BGM;
    AudioSource audioSource;
    

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

}