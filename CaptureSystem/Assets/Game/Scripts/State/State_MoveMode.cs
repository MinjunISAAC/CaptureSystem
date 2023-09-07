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

namespace InGame.ForState
{
    public class State_MoveMode : SimpleState<EStateType>
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Owner
        private Main           _owner          = null;

        // ----- UI
        private MainView       _mainView       = null;
        private CaptureView    _captureView    = null;
        private GalleryView    _galleryView    = null;

        // ----- Manage
        private UnitController _unitController = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public override EStateType StateType => EStateType.MoveMode;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        protected override void _Start(EStateType preStateKey, object startParam)
        {
            Debug.Log($"<color=yellow>[State_MoveMode._Start] Walk Mode에 진입하였습니다.</color>");

            #region <Manage Group>
            _owner = Main.NullableInstance;
            if (_owner == null)
            {
                Debug.LogError($"<color=red>[State_WalkMode._Start] Main이 Null 상태입니다.</color>");
                return;
            }

            _mainView = _owner.MainView;
            if (_mainView == null)
            {
                Debug.LogError($"<color=red>[State_WalkMode._Start] Main View가 Null 상태입니다.</color>");
                return;
            }

            _captureView = _owner.CaptureView;
            if (_captureView == null)
            {
                Debug.LogError($"<color=red>[State_WalkMode._Start] Capture View가 Null 상태입니다.</color>");
                return;
            }

            _galleryView = _owner.GalleryView;
            if (_captureView == null)
            {
                Debug.LogError($"<color=red>[State_WalkMode._Start] Gallery View가 Null 상태입니다.</color>");
                return;
            }

            _unitController = _owner.UnitController;
            if (_unitController == null)
            {
                Debug.LogError($"<color=red>[State_WalkMode._Start] Unit Controller가 Null 상태입니다.</color>");
                return;
            }
            #endregion

            // View 진입
            _mainView.   gameObject.SetActive(true);
            _captureView.gameObject.SetActive(false);
            _galleryView.gameObject.SetActive(false);

            // Joy Pad 활성화
            _unitController.UsedJoyPad(true);
        }

        protected override void _Finish(EStateType nextStateKey)
        {
            Debug.Log($"<color=yellow>[State_MoveMode._Start] Walk Mode에 이탈하였습니다.</color>");
        }
    }
}