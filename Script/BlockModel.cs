using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace jp.yzroid.CsgUnitySweeper
{
    public class BlockModel : MonoBehaviour
    {
        private BGMController mBGM;
        [SerializeField]
        private Material mOpenedMaterial;
        [SerializeField]
        private NumberChanger mNumChanger;
        [SerializeField]
        public GameObject Explosion;
        public AudioClip Boom;
        AudioSource audioSource;

        public AudioClip audioClip;
        OVRHapticsClip hapticsClip;

        void Start()
        {
            hapticsClip = new OVRHapticsClip(audioClip);
        }


        //-------------
        // ポジション //
        //---------------------------------------------------------------------------------

        public int X { get; private set; }
        public int Y { get; private set; }

        public void SetPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        //----------
        // フラグ //
        //---------------------------------------------------------------------------------

        // 爆弾ブロックの場合はtrue
        public bool HasBomb { get; set; }

        // 開かれたブロックの場合はtrue
        public bool IsOpen { get; private set; }

        // チェック済ブロックの場合はtrue
        public bool IsCheck { get; private set; }

        //-------------
        // アクション //
        //---------------------------------------------------------------------------------

        /// <summary>
        /// ブロックを開く
        /// </summary>
        public void Open(int aroundBombs)
        {
            IsOpen = true;
            mNumChanger.ChangeNumber(aroundBombs);
            GetComponent<Renderer>().material = mOpenedMaterial;
            foreach (Transform child in transform)
            {
                if (child.CompareTag("rock"))
                {
                    Destroy(child.gameObject);
                }
            }
        }

        /// <summary>
        /// チェック済フラグを反転させる
        /// それによってチェックマークを表示・非表示にする
        /// </summary>
        public void ChangeCheckFlg()
        {
            if (IsOpen) return;
            IsCheck = !IsCheck;
            if (IsCheck)
            {
                mNumChanger.ChangeUvToCheck();
            }
            else
            {
                mNumChanger.ChangeUvToBlank();

            }
        }

        /// <summary>
        /// 爆弾を表示する
        /// フラグが立っている場合は特別な爆弾を表示する
        /// </summary>
        /// <param name="flg"></param>
        public void ShowBomb(bool flg)
        {
            audioSource = GetComponent<AudioSource>();
            //岩を破壊する
            foreach (Transform child in transform)
            {
                if (child.CompareTag("rock"))
                {
                    Destroy(child.gameObject);
                }
            }
            if (flg)
            {
                mNumChanger.ChangeUvToBombB();
                Instantiate(Explosion, new Vector3(transform.position.x,transform.position.y + 2.0f,transform.position.z), Quaternion.identity);
                audioSource.PlayOneShot(Boom);
                OVRHaptics.RightChannel.Mix(hapticsClip);
                OVRHaptics.LeftChannel.Mix(hapticsClip);
            }
            else
            {
                mNumChanger.ChangeUvToBombA();
                //Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y + 2.0f, transform.position.z), Quaternion.identity);
            }
        }

    }
}
