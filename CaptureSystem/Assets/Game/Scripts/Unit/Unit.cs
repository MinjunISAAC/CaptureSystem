// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

namespace InGame.ForUnit
{
    public class Unit : MonoBehaviour
    {
        // --------------------------------------------------
        // Unit State Type
        // --------------------------------------------------
        public enum EUnitState
        {
            Unknown = 0,
            Idle    = 1,
            Walk    = 2,
            Capture = 3,
        }

        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private Rigidbody _rb       = null;
        [SerializeField] private Animator  _animator = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        [SerializeField] private EUnitState _unitState       = EUnitState.Unknown;
        private Coroutine  _co_CurrentState = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public EUnitState UnitState     => _unitState;
        public Rigidbody  UnitRigidBody => _rb;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public void OnInit()
        {

        }

        public void ChangeToUnitState(EUnitState unitState, float duration = 0.0f, Action doneCallBack = null) => _ChangeToUnitState(unitState, duration, doneCallBack);

        // ---- State 
        private void _ChangeToUnitState(EUnitState unitState, float duration = 0.0f, Action doneCallBack = null)
        {
            if (!Enum.IsDefined(typeof(EUnitState), unitState))
            {
                Debug.LogError($"[Unit._ChangeToUnitState] {Enum.GetName(typeof(EUnitState), unitState)}은 정의되어있지 않은 Enum 값입니다.");
                return;
            }

            if (_unitState == unitState)
                return;

            _unitState = unitState;

            if (_co_CurrentState != null)
                StopCoroutine(_co_CurrentState);

            switch (_unitState)
            {
                case EUnitState.Idle:    _State_Idle();    break;
                case EUnitState.Walk:    _State_Walk();    break;
                case EUnitState.Capture: _State_Capture(); break;
            }
        }

        private void _State_Idle()    => _co_CurrentState = StartCoroutine(_Co_Idle());

        private void _State_Walk()    => _co_CurrentState = StartCoroutine(_Co_Walk());

        private void _State_Capture() => _co_CurrentState = StartCoroutine(_Co_Capture());

        // --------------------------------------------------
        // Functions - Coroutine
        // --------------------------------------------------
        private IEnumerator _Co_Idle()
        {
            _animator.SetTrigger($"Idle");
            yield return null;
        }

        private IEnumerator _Co_Walk()
        {
            _animator.SetTrigger($"Walk");
            yield return null;
        }
        private IEnumerator _Co_Capture()
        {
            yield return null;
        }
    }
}