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

namespace InGame
{
    public class Main : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Manage Group")]
        [SerializeField] private UnitController _unitController = null;
        [SerializeField] private CamController  _camController  = null;

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

        public UnitController UnitController => _unitController;

        public CamController  CamController  => _camController;

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void Awake()
        {
            NullableInstance = this;
        }

        private IEnumerator Start()
        {
            // State 초기화
            StateMachine.Instance.ChangeState(EStateType.MoveMode, null);

            // Cam Controller 초기화
            var targetUnit = _unitController.TargetUnit;
            _camController.OnInit(targetUnit);

            // Main View 초기화
            _mainView.OnInit
            (
                () =>
                { 
                    _captureView.gameObject.SetActive(true);
                    _captureView.VisiableToScreen
                    (
                        true, 
                        () => 
                        { 
                            _camController.ChangeToCamState(CamController.ECamState.CaptureMode);
                            _unitController.UsedJoyPad(false);
                        }, 
                        null
                    );
                }
            );

            yield return null;
        }
    }
}