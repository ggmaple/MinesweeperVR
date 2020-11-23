using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace jp.yzroid.CsgUnitySweeper
{
    public class RightController : MonoBehaviour
    {
        [SerializeField]
        private GameObject pickaxe;
        [SerializeField]
        public bool Right = false;


        public void Update()
        {
            if (OVRInput.Get(OVRInput.RawButton.RHandTrigger) && Right)
            {
                pickaxe.SetActive(true);
            }
            else
            {
                pickaxe.SetActive(false);
            }
        }

        public void OnRight()
        {
            Right = true;
            Debug.Log("OnRight 呼び出し");
        }

        public void OffRight()
        {
            Right = false;
            Debug.Log("OffRight 呼び出し");
        }
    }
}

