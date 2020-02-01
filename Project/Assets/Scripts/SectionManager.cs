using UnityEngine;

namespace GGJ2020
{
	public class SectionManager : MonoBehaviour
	{
		public int difficulty;

		private FiniteStateMachine sectionState;
		private GenerativeData m_generativeData; // set in run manager

		public void Initialize(GenerativeData generativeData, int difficulty)
		{
			sectionState = new FiniteStateMachine() { debug = true };
			sectionState.SetState(
				"READY",
				(previousState) => { },
				() => { },
				(nextState) => { });
			sectionState.SetState(
				"ACTIVE",
				(previousState) => { },
				() => { },
				(nextState) => { });
			sectionState.SetState(
				"COMPLETED",
				(previousState) => { },
				() => { },
				(nextState) => { });
			sectionState.ChangeState("READY");
		}

		public void BeginSection()
		{
			sectionState.ChangeState("ACTIVE");
		}
	}
}