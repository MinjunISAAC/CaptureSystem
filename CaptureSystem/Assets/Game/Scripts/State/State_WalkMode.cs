// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Utility.SimpleFSM;
using InGame.ForUnit.Manage;

namespace InGame.ForState
{
    public class State_WalkMode : SimpleState<EStateType>
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Owner
        private Main _owner = null;

        // ----- UI

        // ----- Count Down Value
        private UnitController _unitController = null;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public override EStateType StateType => EStateType.WalkMode;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        protected override void _Start(EStateType preStateKey, object startParam)
        {
            Debug.Log($"<color=yellow>[State_WalkMode._Start] Walk Mode에 진입하였습니다.</color>");

            #region <Manage Group>
            _owner = Main.NullableInstance;
            if (_owner == null)
            {
                Debug.LogError($"<color=red>[State_WalkMode._Start] Main이 Null 상태입니다.</color>");
                return;
            }

            _unitController = _owner.UnitController;
            if (_unitController == null)
            {
                Debug.LogError($"<color=red>[State_WalkMode._Start] Unit Controller가 Null 상태입니다.</color>");
                return;
            }
            #endregion

            // Unit Controller 초기화
            _unitController.OnInitToUnit();
        }

        protected override void _Finish(EStateType nextStateKey)
        {
            Debug.Log($"<color=yellow>[State_WalkMode._Start] Walk Mode에 이탈하였습니다.</color>");
        }
    }
}