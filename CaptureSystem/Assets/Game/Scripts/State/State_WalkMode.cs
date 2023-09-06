// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Utility.SimpleFSM;

namespace InGame.ForState
{
    public class State_WalkMode : SimpleState<EStateType>
    {
        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Owner

        // ----- UI

        // ----- Count Down Value
        
        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public override EStateType StateType => EStateType.WalkMode;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        protected override void _Start(EStateType preStateKey, object startParam)
        {
            Debug.Log($"<color=yellow>[State_WalkMode._Start] Walk Mode�� �����Ͽ����ϴ�.</color>");

            #region <Manage Group>
            /*
            _owner = Main.NullableInstance;
            if (_owner == null)
            {
                Debug.LogError($"<color=red>[State_CountDown._Start] Main�� Null �����Դϴ�.</color>");
                return;
            }
            */
            #endregion

        }

        protected override void _Finish(EStateType nextStateKey)
        {
            Debug.Log($"<color=yellow>[State_WalkMode._Start] Walk Mode�� ��Ż�Ͽ����ϴ�.</color>");
        }
    }
}