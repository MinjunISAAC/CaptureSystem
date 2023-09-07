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
using InGame.ForGallery;

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
        [SerializeField] private GalleryManager    _galleryManager    = null;

        [Header("UI Group")]
        [SerializeField] private MainView       _mainView       = null;
        [SerializeField] private CaptureView    _captureView    = null;
        [SerializeField] private GalleryView    _galleryView    = null;

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
        public GalleryManager    GalleryManager    => _galleryManager;

        public MainView          MainView          => _mainView;
        public CaptureView       CaptureView       => _captureView;
        public GalleryView       GalleryView       => _galleryView;

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

            // Gallery Manager 초기화
            var directoryPath = _captureController.PhotoDirectoryPath;
            var fileGroup     = new List<Sprite>();

            _galleryManager.SetPhotoDirectoryPath(directoryPath);
            fileGroup = _galleryManager.LoadPhotoGroup();

            // Main View 초기화
            _mainView.OnInit
            (
                () => 
                { 
                    _camController.ChangeToCamState(CamController.ECamState.CaptureMode);
                    StateMachine.Instance.ChangeState(EStateType.CameraMode, null);
                },
                () => { StateMachine.Instance.ChangeState(EStateType.GalleryMode, null); }
            );
            _mainView.gameObject.SetActive(false);

            // Capture View 초기화
            _captureView.OnInit
            (
                () => { _captureController.Capture(); },
                () =>
                {
                    _camController.ChangeToCamState(CamController.ECamState.Follow_Unit);
                    StateMachine.Instance.ChangeState(EStateType.MoveMode, null); 
                }
            );
            _captureView.gameObject.SetActive(false);

            // Gallery View 초기화
            _galleryView.SetToGallery(fileGroup);
            _galleryView.SetToCloseButton
            (
                () =>
                {
                    StateMachine.Instance.ChangeState(EStateType.MoveMode, null);
                }
            );
            _galleryView.gameObject.SetActive(false);

            // 초기 State 설정
            StateMachine.Instance.ChangeState(EStateType.MoveMode, null);


            yield return null;
        }
    }
}