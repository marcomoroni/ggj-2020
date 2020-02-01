using UnityEngine;

namespace GGJ2020
{
	public class MasterGameManager : MonoBehaviour
	{
		public GenerativeData generativeData;
		public GameObject runManager;

		private void Start()
		{
			// Run Manager
			GameObject rmgo = Instantiate(runManager);
			var rm = rmgo.GetComponent<RunManager>();
			rm.Initialize(generativeData);
			rm.name = runManager.name;
			rm.BeginRun();
		}
	}
}