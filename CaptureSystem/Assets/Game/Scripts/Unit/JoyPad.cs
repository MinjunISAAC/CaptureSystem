// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

namespace InGame.ForUnit.ForUI
{
    public class JoyPad : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Activate")]
        [SerializeField] private bool          _isActived = true;    // 활성화 여부 

        [Header("JoyStick RectTransform")]
        [SerializeField] private RectTransform _canvasRect  = null;
        [SerializeField] private RectTransform _frameRect   = null;    // Joy Pad 외각 프레임 
        [SerializeField] private RectTransform _stickRect   = null;    // Joy Pad 중앙 스틱 
        [SerializeField] private RectTransform _blindArea_0 = null;
        [SerializeField] private RectTransform _blindArea_1 = null;

        [Header("Origin Move Speed")]
        [Range(0f, 50f)][SerializeField] private float _originMoveValue   = 0.125f;  // 기본 속도 값 
        [Range(0f, 1f)] [SerializeField] private float _originRotateValue = 1f;      // 기본 속도 값 

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        #region <Private>
        private Unit    _targetUnit     = null;

        private Vector3 _movePos        = new Vector3();

        private float   _joyStickRadius = 0.0f;
        private float   _moveSpeed      = 0.0f;
        private float   _rotateSpeed    = 0.0f;
        private float   _moveFactor     = 1f;

        private bool    _isMove         = false;
        private bool    _isDragging     = false;
        #endregion

        // --------------------------------------------------
        // Property
        // --------------------------------------------------
        public float         MoveSpeed => _moveSpeed;
        public RectTransform FrameRect => _frameRect;

        // --------------------------------------------------
        // Move Factors Event
        // --------------------------------------------------
        public event Action<bool> OnUsedJoyStickEvent;
        public void UsedJoyStickEvent(bool isUsed)
        {
            if (OnUsedJoyStickEvent != null)
                OnUsedJoyStickEvent(isUsed);
        }

        // --------------------------------------------------
        // Function - Event
        // --------------------------------------------------
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (null == _targetUnit)                 return;
                if (!_isActived)                         return;
                
                _isDragging         = true;
                _frameRect.position = Input.mousePosition;
                
                if (!_BlindToTouch(_frameRect.anchoredPosition)) return;

                _OnTouch(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                if (null == _targetUnit)                         return;
                if (!_isActived)                                 return;
                if (!_BlindToTouch(_frameRect.anchoredPosition)) return;
                if (Vector3.Magnitude(_targetUnit.UnitRigidBody.velocity) >= 0.25f && _targetUnit.UnitState != Unit.EUnitState.Walk)
                    _targetUnit.ChangeToUnitState(Unit.EUnitState.Walk);

                _frameRect.gameObject.SetActive(true);
                _OnTouch(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (null == _targetUnit)                         return;
                if (!_isActived)                                 return;
                if (!_BlindToTouch(_frameRect.anchoredPosition)) return;

                _isDragging = false;

                _targetUnit.UnitRigidBody.velocity = Vector3.zero;
                _targetUnit.ChangeToUnitState(Unit.EUnitState.Idle);

                _frameRect.gameObject.SetActive(false);
                _stickRect.localPosition = Vector2.zero;
            }
        }

        // --------------------------------------------------
        // Function - Nomal
        // --------------------------------------------------
        public void OnInit(Unit targetUnit)
        {
            _joyStickRadius = _frameRect.rect.width * 0.5f;

            if (null != _targetUnit)
                return;

            _targetUnit  = targetUnit;
            _moveSpeed   = _originMoveValue;
            _rotateSpeed = _originRotateValue;

            OnUsedJoyStickEvent += (isUsed) => { _isActived = isUsed; _stickRect.localPosition = Vector2.zero; _movePos = Vector3.zero; };
        }

        private void _OnTouch(Vector2 touchVec)
        {
            Vector2 vec       = new Vector2(touchVec.x - _frameRect.position.x, touchVec.y - _frameRect.position.y);
            Vector2 vecNormal = vec.normalized;
            Vector3 force     = new Vector3(vecNormal.x, 0f, vecNormal.y) * _moveSpeed * _moveFactor * 2.5f;

            _stickRect.localPosition = Vector2.ClampMagnitude(vec, _joyStickRadius);

            if (_isDragging) _targetUnit.UnitRigidBody.velocity = force;
            else             _targetUnit.UnitRigidBody.velocity = Vector3.zero;


            if (null == _targetUnit)
                return;

            var rotateVec                  = Quaternion.Euler(new Vector3(0f, Mathf.Atan2(vecNormal.x, vecNormal.y) * Mathf.Rad2Deg, 0f));
            _targetUnit.transform.rotation = Quaternion.Lerp(_targetUnit.transform.rotation, rotateVec, _rotateSpeed);
        }

        private bool _BlindToTouch(Vector3 inputPos)
        {
            if ((inputPos.x >= _blindArea_0.anchoredPosition.x - _blindArea_0.rect.width / 2f &&
                 inputPos.y <= _blindArea_0.anchoredPosition.y + _blindArea_0.rect.height / 2f) || 
                (inputPos.x <= _blindArea_1.anchoredPosition.x + _blindArea_1.rect.width / 2f &&
                 inputPos.y >= _blindArea_1.anchoredPosition.y - _blindArea_1.rect.height / 2f))
                return false;
            else
                return true;
        }

        public void SetSpeed(float speed)
        {
            _moveSpeed = speed;
        }
    }
}