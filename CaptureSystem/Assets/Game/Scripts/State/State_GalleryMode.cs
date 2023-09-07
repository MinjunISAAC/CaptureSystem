// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Utility.SimpleFSM;
using InGame.ForUnit.Manage;
using InGame.ForUI;
using InGame.ForCam;
using InGame.ForGallery;

namespace InGame.ForState
{
    public class State_GalleryMode : SimpleState<EStateType>
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Owner
        private Main           _owner          = null;

        // ----- UI
        private MainView       _mainView       = null;
        private GalleryView    _galleryView    = null;

        // ----- Manage
        private UnitController _unitController = null;
        private GalleryManager _galleryManager = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public override EStateType StateType => EStateType.GalleryMode;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        protected override void _Start(EStateType preStateKey, object startParam)
        {
            Debug.Log($"<color=yellow>[State_GalleryMode._Start] Gallery Mode에 진입하였습니다.</color>");

            #region <Manage Group>
            _owner = Main.NullableInstance;
            if (_owner == null)
            {
                Debug.LogError($"<color=red>[State_GalleryMode._Start] Main이 Null 상태입니다.</color>");
                return;
            }

            _mainView = _owner.MainView;
            if (_mainView == null)
            {
                Debug.LogError($"<color=red>[State_GalleryMode._Start] Main View가 Null 상태입니다.</color>");
                return;
            }

            _galleryView = _owner.GalleryView;
            if (_galleryView == null)
            {
                Debug.LogError($"<color=red>[State_GalleryMode._Start] Capture View가 Null 상태입니다.</color>");
                return;
            }

            _unitController = _owner.UnitController;
            if (_unitController == null)
            {
                Debug.LogError($"<color=red>[State_GalleryMode._Start] Unit Controller가 Null 상태입니다.</color>");
                return;
            }

            _galleryManager = _owner.GalleryManager;
            if (_galleryManager == null)
            {
                Debug.LogError($"<color=red>[State_GalleryMode._Start] Gallery Manager가 Null 상태입니다.</color>");
                return;
            }
            #endregion

            // View 진입
            _mainView.   gameObject.SetActive(false);
            _galleryView.gameObject.SetActive(true);

            // Joy Pad 비활성화
            _unitController.UsedJoyPad(false);

            // Gallery Manager 리플레쉬
            var photoGroup = _galleryManager.LoadPhotoGroup();
            Debug.Log($"Photo Group {photoGroup.Count}");
            _galleryView.SetToGallery(photoGroup);
        }

        protected override void _Finish(EStateType nextStateKey)
        {
            Debug.Log($"<color=orange>[State_GalleryMode._Finish] Gallery Mode에 이탈하였습니다.</color>");
        }
    }
}