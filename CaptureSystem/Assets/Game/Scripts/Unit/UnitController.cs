// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForUnit.ForUI;

namespace InGame.ForUnit.Manage
{
    public class UnitController : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Joy Pad")]
        [SerializeField] private JoyPad    _joyPad            = null;

        [Header("Unit Group")]
        [SerializeField] private Unit      _targetUnit        = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Const
        private const float ROTATE_VALUE = 0.5f;

        // ----- Private
        private float _unitMoveValue = 0.0f;

        // --------------------------------------------------
        // Property
        // --------------------------------------------------
        public Unit   PlayableUnit => _targetUnit;
        public JoyPad JoyPad       => _joyPad;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public Unit OnInitToUnit(Transform spawnTrans = null)
        {
            if (_targetUnit == null)
                return null;
            
            if (spawnTrans != null)
                _targetUnit.transform.position = spawnTrans.position;

            SetJoyPad();

            return _targetUnit;
        }

        public void SetJoyPad()
        {
            if (_targetUnit == null)
            {
                Debug.LogError($"<color = red>[UnitController.SetJoyPad] Target Unit이 할당되지 않았습니다.</color>");
                return;
            }

            _joyPad.OnInit(_targetUnit);
            _joyPad.UsedJoyStickEvent(true);
        }

        public void ChangeUnitSpeed(float moveSpeed, float roatateSpeed = ROTATE_VALUE)
        {
            _unitMoveValue = moveSpeed;
            _joyPad.ChangeMoveFactors(moveSpeed, roatateSpeed);
        }

        public void UsedJoyPad(bool isOn)
        {
            _joyPad.UsedJoyStickEvent(isOn);

            if (!isOn) _joyPad.FrameRect.gameObject.SetActive(isOn);
        }
    }
}
