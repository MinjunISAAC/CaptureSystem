// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;
using UnityEngine.UI;

namespace InGame.ForUI
{
    public class CaptureView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("UI Group")]
        [SerializeField] private Button    _BTN_Capture       = null;

        [Header("Animate Group")]
        [SerializeField] private Animation _captureScreenAnim = null;
        [SerializeField] private Animation _blackOutAnim      = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Const
        private const string SCREEN_SHOW = "Camera_Screen_Show";
        private const string SCREEN_HIDE = "Camera_Screen_Hide";
        private const string BLACK_OUT   = "Camera_BlackOut";

        // ----- Private
        private Coroutine _co_VisiableScreen = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void VisiableToScreen(bool isShow, Action cameraAction, Action doneCallBack)
        {
            if (_co_VisiableScreen == null)
            {
                _co_VisiableScreen = StartCoroutine(_Co_VisiableToScreen(isShow, cameraAction, doneCallBack));
                return;
            }
        }

        public void OnInit()
        {
            _BTN_Capture.onClick.AddListener
            (
                () => 
                {
                    _ShowBlackOut();
                }
            );
        }

        // ----- Private
        private void _ShowBlackOut()
        {
            _blackOutAnim.clip = _blackOutAnim.GetClip(BLACK_OUT);
            _blackOutAnim.Play();
        }

        // --------------------------------------------------
        // Functions - Coroutine
        // --------------------------------------------------
        private IEnumerator _Co_VisiableToScreen(bool isShow, Action cameraAction, Action doneCallBack)
        {
            cameraAction?.Invoke();

            if (isShow)
            {
                _captureScreenAnim.gameObject.SetActive(true);
                _captureScreenAnim.clip = _captureScreenAnim.GetClip(SCREEN_SHOW);
                _captureScreenAnim.Play();
            }
            else
            {
                _captureScreenAnim.clip = _captureScreenAnim.GetClip(SCREEN_HIDE);
                _captureScreenAnim.Play();

                var clipTime = _captureScreenAnim.clip.length;
                
                yield return new WaitForSeconds(clipTime);
            }

            doneCallBack?.Invoke();
            _co_VisiableScreen = null;
        }
    }
}