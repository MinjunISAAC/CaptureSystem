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
            Debug.Log($"<color=yellow>[State_GalleryMode._Start] Gallery Mode�� �����Ͽ����ϴ�.</color>");

            #region <Manage Group>
            _owner = Main.NullableInstance;
            if (_owner == null)
            {
                Debug.LogError($"<color=red>[State_GalleryMode._Start] Main�� Null �����Դϴ�.</color>");
                return;
            }

            _mainView = _owner.MainView;
            if (_mainView == null)
            {
                Debug.LogError($"<color=red>[State_GalleryMode._Start] Main View�� Null �����Դϴ�.</color>");
                return;
            }

            _galleryView = _owner.GalleryView;
            if (_galleryView == null)
            {
                Debug.LogError($"<color=red>[State_GalleryMode._Start] Capture View�� Null �����Դϴ�.</color>");
                return;
            }

            _unitController = _owner.UnitController;
            if (_unitController == null)
            {
                Debug.LogError($"<color=red>[State_GalleryMode._Start] Unit Controller�� Null �����Դϴ�.</color>");
                return;
            }

            _galleryManager = _owner.GalleryManager;
            if (_galleryManager == null)
            {
                Debug.LogError($"<color=red>[State_GalleryMode._Start] Gallery Manager�� Null �����Դϴ�.</color>");
                return;
            }
            #endregion

            // View ����
            _mainView.   gameObject.SetActive(false);
            _galleryView.gameObject.SetActive(true);

            // Joy Pad ��Ȱ��ȭ
            _unitController.UsedJoyPad(false);

            // Gallery Manager ���÷���
            var photoGroup = _galleryManager.LoadPhotoGroup();
            Debug.Log($"Photo Group {photoGroup.Count}");
            _galleryView.SetToGallery(photoGroup);
        }

        protected override void _Finish(EStateType nextStateKey)
        {
            Debug.Log($"<color=orange>[State_GalleryMode._Finish] Gallery Mode�� ��Ż�Ͽ����ϴ�.</color>");
        }
    }
}