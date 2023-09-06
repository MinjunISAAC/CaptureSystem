// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForUnit.Manage;
using InGame.ForState;

namespace InGame
{
    public class Main : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Manage Group")]
        [SerializeField] private UnitController _unitController = null;

        [Header("ETC Group")]
        [SerializeField] private Transform      _unitSpawnPoint = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public static Main NullableInstance
        {
            get;
            private set;
        } = null;

        public UnitController UnitController => _unitController;

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void Awake()
        {
            NullableInstance = this;
        }

        private IEnumerator Start()
        {
            StateMachine.Instance.ChangeState(EStateType.WalkMode, null);
            yield return null;
        }
    }
}