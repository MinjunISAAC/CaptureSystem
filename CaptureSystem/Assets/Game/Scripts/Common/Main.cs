// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForUnit.Manage;
using InGame.ForState;
using InGame.ForCam;

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

            yield return null;
        }
    }
}