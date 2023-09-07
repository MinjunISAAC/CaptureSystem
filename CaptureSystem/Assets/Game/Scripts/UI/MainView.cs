// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;
using UnityEngine.UI;

namespace InGame.ForUI
{
    public class MainView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private Button _BTN_Capture = null;
        [SerializeField] private Button _BTN_Gallery = null;

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void OnDestroy()
        {
            _BTN_Capture.onClick.RemoveAllListeners();
            _BTN_Gallery.onClick.RemoveAllListeners();
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void OnInit(Action onClickCaptureModeBtn, Action onClickGalleryModeBtn)
        {
            _BTN_Capture.onClick.AddListener
            (
                () => onClickCaptureModeBtn()
            );

            _BTN_Gallery.onClick.AddListener
            (
                () => onClickGalleryModeBtn()
            );
        }
    }
}