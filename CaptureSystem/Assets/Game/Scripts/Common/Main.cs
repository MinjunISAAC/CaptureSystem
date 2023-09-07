// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForUnit.Manage;
using InGame.ForState;
using InGame.ForCam;
using InGame.ForUI;
using InGame.ForCapture;

namespace InGame
{
    public class Main : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Manage Group")]
        [SerializeField] private UnitController    _unitController    = null;
        [SerializeField] private CamController     _camController     = null;
        [SerializeField] private CaptureController _captureController = null;

        [Header("UI Group")]
        [SerializeField] private MainView       _mainView       = null;
        [SerializeField] private CaptureView    _captureView    = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public static Main NullableInstance
        {
            get;
            private set;
        } = null;

        public UnitController    UnitController    => _unitController;
        public CaptureController CaptureController => _captureController;
        public CamController     CamController     => _camController;

        public MainView          MainView          => _mainView;
        public CaptureView       CaptureView       => _captureView;

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void Awake()
        {
            NullableInstance = this;
        }

        private IEnumerator Start()
        {
            // Cam Controller 초기화
            var targetUnit = _unitController.TargetUnit;
            _camController.OnInit(targetUnit);

            // Unit Controller 초기화
            _unitController.OnInitToUnit();

            // Capture Controller 초기화
            var captureCamera = _camController.CaptureCamera;
            _captureController.SetCaptureInfo  ();
            _captureController.SetCaptureCamera(captureCamera);
            _captureController.SetCaptureView  (_captureView);

            // Main View 초기화
            _mainView.OnInit
            (
                () => 
                { 
                    _camController.ChangeToCamState(CamController.ECamState.CaptureMode);
                    StateMachine.Instance.ChangeState(EStateType.CameraMode, null);
                }
            );

            // Capture View 초기화
            _captureView.OnInit
            (
                () => 
                { 
                    _captureController.Capture();  
                },
                () => { StateMachine.Instance.ChangeState(EStateType.MoveMode, null); }
            );

            // 초기 State 설정
            StateMachine.Instance.ChangeState(EStateType.MoveMode, null);


            yield return null;
        }
    }
}