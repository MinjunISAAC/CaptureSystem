// ----- C#
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Utility.SimpleFSM;

public class SimpleStateMachine<TKey>
{
    // --------------------------------------------------
    // Variables
    // --------------------------------------------------
    protected MonoBehaviour                       _coroutineExecutor = null;
    protected Dictionary<TKey, SimpleState<TKey>> _stateSet          = null;
    protected SimpleState<TKey>                   _currentState      = null;

    // --------------------------------------------------
    // Properties
    // --------------------------------------------------
    public virtual TKey CurrentState
    {
        get
        {
            return _currentState.StateType;
        }
    }

    // --------------------------------------------------
    // Functions - Nomal
    // --------------------------------------------------
    // ----- Public
    public virtual void OnInit(Dictionary<TKey, SimpleState<TKey>> stateSet,
                               MonoBehaviour                       coroutineExecutor,
                               object                              param)
    {
        OnRelease();

        if (stateSet == null)
        {
            Debug.LogError("[SimpleStateMachine.OnInit] 초기화 할 State Set이 Null 상태입니다.");
            return;
        }

        if (coroutineExecutor == null)
        {
            Debug.LogError("[SimpleStateMachine.OnInit] 초기화 할 Coroutine 실행자가 Null 상태입니다.");
            return;
        }

        _stateSet          = stateSet;
        _coroutineExecutor = coroutineExecutor;

        foreach (var targetState in _stateSet)
        {
            var state = targetState.Value;

            if (state == null)
                continue;

            state.Init(ChangeState, _coroutineExecutor, param);
        }
    }

    public virtual void OnUpdate()
    {
        _currentState?.Update();
    }

    public virtual void OnRelease()
    {
        _currentState      = null;
        _coroutineExecutor = null;

        if (_stateSet != null)
        {
            foreach (var statePair in _stateSet)
            {
                var state = statePair.Value;
                if (state == null)
                    continue;

                state.Release();
            }

            _stateSet.Clear();
        }
    }

    public virtual void ChangeState(TKey targetStateType, object startParam)
    {
        if (null == _stateSet)
        {
            Debug.LogError("[SimpleStateMachine.ChangeState] State Set이 존재하지 않습니다.");
            return;
        }

        if (!_stateSet.TryGetValue(targetStateType, out var state))
        {
            Debug.LogError($"[SimpleStateMachine.ChangeState] State Set에 {targetStateType}가 존재하지 않습니다.");
            return;
        }

        if (null == state)
        {
            Debug.LogError($"[SimpleStateMachine.ChangeState] Target State인 SimpleState[{nameof(targetStateType)}]가 존재하지 않습니다.");
            return;
        }

        var prevSimpleState = _currentState;
        if (null != prevSimpleState)
        {
            _currentState.Finish(targetStateType);

            _currentState = state;
            _currentState.Start(prevSimpleState.StateType, startParam);
        }
        else
        {
            _currentState = state;
            _currentState.Start(default(TKey), startParam);
        }
    }
}