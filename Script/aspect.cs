using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aspect : MonoBehaviour
{
    public Camera main;
    public float a;

    private void Start()
    {
        a = main.aspect;
    }
}
