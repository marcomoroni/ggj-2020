using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GGJ2020
{
	public class FiniteStateMachine
	{
		private class State
		{
			public string name;
			public Action<string> onEnter = (previousState) => { };
			public Action onUpdate = () => { };
			public Action<string> onExit = (nextState) => { };
		}

		private List<State> states = new List<State>();
		private State currentState;

		public string CurrentState => currentState == null ? null : currentState.name;

		public bool debug = false;

		/// <summary>
		/// Add or overried existing state data.
		/// </summary>
		public void SetState(string name, Action<string> onEnter, Action onUpdate, Action<string> onExit)
		{
			if (String.IsNullOrEmpty(name))
			{
				Debug.LogWarning("Name is empty.");
				return;
			}

			State newState = new State
			{
				name = name,
				onEnter = onEnter,
				onUpdate = onUpdate,
				onExit = onExit
			};

			bool alreadyExists = states.Select(s => s.name).Contains(name);
			if (alreadyExists)
			{
				if (debug) Debug.Log($"Overriding '{name}' state.");
				states[states.IndexOf(states.First(s => s.name == name))] = newState;
			}
			else
			{
				if(debug) Debug.Log($"Adding '{name}' state.");
				states.Add(newState);
			}
		}

		public void SetState(string name)
		{
			SetState(name, (p) => { }, () => { }, (n) => { });
		}

		public void Update()
		{
			currentState?.onUpdate();
		}

		public void ChangeState(string newState)
		{
			if (!states.Select(s => s.name).Contains(newState))
			{
				Debug.LogWarning($"State '{newState}' does not exists.");
				return;
			}

			if (currentState == null)
			{
				if (debug) Debug.Log($"Change to '{newState}' state (from null).");
				states.First(s => s.name == newState).onEnter(null);
			}
			else
			{
				if (debug) Debug.Log($"Change to '{newState}' state (from '{currentState.name}').");
				currentState.onExit(newState);
				states.First(s => s.name == newState).onEnter(newState);
			}
			currentState = states.First(s => s.name == newState);
		}
	}
}