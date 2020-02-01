using System.Collections.Generic;
using UnityEngine;

namespace GGJ2020
{
	[System.Serializable]
	public class ActivelElectricNodeAuthoring
	{
		public Vector2Int position;
		public bool defaultTimer = true;
		public float customTimer;
	}

	public class ElectricBoard : MonoBehaviour
	{
		public Vector2Int size;

		private class ElectricNode
		{
			public GameObject gameObject;
			public bool target;

			// If a target
			public float timer;
			public float countdown;
			public FiniteStateMachine state;
		}

		public int difficulty; // get from section manager

		private ElectricNode[,] nodes;

		private void Start()
		{
			nodes = new ElectricNode[size.x, size.y];
			for (int x = 0; x < size.x; x++)
			{
				for (int y = 0; y < size.y; y++)
				{
					nodes[x, y] = new ElectricNode
					{

					};
				}
			}
		}
	}
}
