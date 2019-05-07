using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Fsm<T>  where T: Component,IAgent
{

	private Dictionary<Type, AbstractState<T>> _stateCache;

	private AbstractState<T> _currentState;

	private T _target;

public Fsm(T p_target)
{
    _target = p_target;
	_stateCache  = new Dictionary<Type, AbstractState<T>>();
	DetectExistingStates();
}


public void ChangeState<U>() where U : AbstractState<T>
{

	if (!_stateCache.ContainsKey(typeof(U)))
	{
		// If not, create it, passing in the target
		U state = _target.gameObject.AddComponent<U>();
		state.target = _target;
		_stateCache[typeof(U)] = state;
		ChangeState(state);
	}
	else
	{
		
		AbstractState<T> newState = _stateCache[typeof(U)];
		ChangeState(newState);
	}
}

private void ChangeState(AbstractState<T> pNewState)
{
	if (_currentState == pNewState) return;
	if (_currentState != null) _currentState.Exit(_target);
	_currentState = pNewState;
	if (_currentState != null) _currentState.Enter(_target);
}

   


	private void DetectExistingStates()
	{
		AbstractState<T>[] states = _target.GetComponentsInChildren<AbstractState<T>>();
		foreach (AbstractState<T> state in states)
		{
			state.enabled = false;
			state.target = _target;
			_stateCache.Add(state.GetType(), state);
		}
	}

public AbstractState<T> GetCurrentState()
{
	return _currentState;
}
}


