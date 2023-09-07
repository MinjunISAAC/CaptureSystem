// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;
using UnityEngine.UI;

namespace InGame.ForGallery
{
    public class PhotoFrame : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private Image _IMG_Photo = null;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        public void SetPhoto(Sprite photoSprite)
        {
            if (_IMG_Photo == null)
            {
                Debug.LogError($"<color=red>[PhotoFrame.SetPhoto] Photo Frame이 Null 상태입니다.</color>");
                return;
            }

            _IMG_Photo.sprite = photoSprite;
        }
    }
}