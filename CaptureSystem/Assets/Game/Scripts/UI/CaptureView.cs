// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

namespace InGame.ForUI
{
    public class CaptureView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private Animation _animation = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Const
        private const string SCREEN_SHOW = "Camera_Screen_Show";
        private const string SCREEN_HIDE = "Camera_Screen_Hide";

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

        // --------------------------------------------------
        // Functions - Coroutine
        // --------------------------------------------------
        private IEnumerator _Co_VisiableToScreen(bool isShow, Action cameraAction, Action doneCallBack)
        {
            cameraAction?.Invoke();

            if (isShow)
            {
                _animation.gameObject.SetActive(true);
                _animation.clip = _animation.GetClip(SCREEN_SHOW);
                _animation.Play();
            }
            else
            {
                _animation.clip = _animation.GetClip(SCREEN_HIDE);
                _animation.Play();

                var clipTime = _animation.clip.length;
                
                yield return new WaitForSeconds(clipTime);
            }

            doneCallBack?.Invoke();
            _co_VisiableScreen = null;
        }
    }
}