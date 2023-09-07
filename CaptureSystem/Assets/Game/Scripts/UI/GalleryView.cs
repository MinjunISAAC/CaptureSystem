// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;
using UnityEngine.UI;

// ----- User Defined
using InGame.ForGallery;
using System;

namespace InGame.ForUI
{
    public class GalleryView : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Photo Frame Origin")]
        [SerializeField] private PhotoFrame _photoFrameOrigin = null;

        [Header("UI Group")]
        [SerializeField] private Button    _BTN_Close         = null;
        [SerializeField] private Transform _photoParents      = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private List<PhotoFrame> _photoFrameList = new List<PhotoFrame>();

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void SetToGallery(List<Sprite> photoList)
        {
            for (int i = 0; i < _photoFrameList.Count; i++)
            {
                var photoFrame = _photoFrameList[i];
                Destroy(photoFrame.gameObject);
            }

            _photoFrameList.Clear();

            for (int i = 0; i < photoList.Count; i++)
            {
                var photoFile  = photoList[i];
                var photoFrame = Instantiate(_photoFrameOrigin, _photoParents);

                _photoFrameList.Add(photoFrame);

                photoFrame.SetPhoto(photoFile);
            }
        }

        public void SetToCloseButton(Action onClickCloseBtn)
        {
            _BTN_Close.onClick.AddListener(() => { onClickCloseBtn?.Invoke(); });
        }
    }
}