using System.Collections.Generic;
using UnityEngine;

namespace GGJ2020
{
	public class RunManager : MonoBehaviour
	{
		public GameObject sectionPrefab;
		private const int generatedSectionsAtBeginning = 10;
		private GenerativeData m_generativeData; // set in gmaster manager
		private FiniteStateMachine runState;
		private List<SectionManager> sections = new List<SectionManager>();
		private int currentSection;

		public void Initialize(GenerativeData generativeData)
		{
			m_generativeData = generativeData;

			runState = new FiniteStateMachine()
			{
				debug = true
			};
			runState.SetStateData("READY");
			runState.SetState("RUNNING",
				(previousState) =>
				{
					for (int i = 0; i < generatedSectionsAtBeginning; i++)
					{
						GameObject sgo = Instantiate(sectionPrefab);
						SectionManager sm = sgo.GetComponent<SectionManager>();
						sm.Initialize(m_generativeData, 0);

						sections.Add(sm);
					}

					sections[0].BeginSection();
					currentSection = 0;
				},
				() => { },
				(nextState) => { });
			runState.SetStateData("ENDED");
			runState.ChangeState("READY");
		}

		public void BeginRun()
		{
			runState.ChangeState("RUNNING");
		}
	}
}