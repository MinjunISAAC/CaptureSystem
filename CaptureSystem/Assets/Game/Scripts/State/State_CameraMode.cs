// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Utility.SimpleFSM;
using InGame.ForUnit.Manage;
using InGame.ForUI;
using InGame.ForCapture;

namespace InGame.ForState
{
    public class State_CameraMode : SimpleState<EStateType>
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Owner
        private Main              _owner             = null;

        // ----- UI
        private CaptureView       _captureView       = null;
        private MainView          _mainView          = null;

        // ----- Manage
        private UnitController    _unitController    = null;
        private CaptureController _captureController = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public override EStateType StateType => EStateType.CameraMode;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        protected override void _Start(EStateType preStateKey, object startParam)
        {
            Debug.Log($"<color=yellow>[State_CameraMode._Start] Camera Mode에 진입하였습니다.</color>");

            #region <Manage Group>
            _owner = Main.NullableInstance;
            if (_owner == null)
            {
                Debug.LogError($"<color=red>[State_CameraMode._Start] Main이 Null 상태입니다.</color>");
                return;
            }

            _captureView = _owner.CaptureView;
            if (_captureView == null)
            {
                Debug.LogError($"<color=red>[State_CameraMode._Start] Capture View가 Null 상태입니다.</color>");
                return;
            }

            _mainView = _owner.MainView;
            if (_mainView == null)
            {
                Debug.LogError($"<color=red>[State_CameraMode._Start] Main View가 Null 상태입니다.</color>");
                return;
            }

            _unitController = _owner.UnitController;
            if (_unitController == null)
            {
                Debug.LogError($"<color=red>[State_CameraMode._Start] Unit Controller가 Null 상태입니다.</color>");
                return;
            }
            #endregion

            // View 진입
            _mainView.   gameObject.SetActive(false);
            _captureView.gameObject.SetActive(true);
            
            _captureView.VisiableToScreen
            (
                true, null
            );

            // Joy Pad 비활성화
            _unitController.UsedJoyPad(false);
        }

        protected override void _Finish(EStateType nextStateKey)
        {
            Debug.Log($"<color=yellow>[State_CameraMode._Start] Camera Mode에 이탈하였습니다.</color>");
        }
    }
}