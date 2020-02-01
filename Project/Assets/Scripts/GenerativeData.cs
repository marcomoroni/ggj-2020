using System.Collections.Generic;
using UnityEngine;

namespace GGJ2020
{
	[System.Serializable]
	[CreateAssetMenu(fileName = "NewGenerativeData", menuName = "Custom/Generative Data")]
	public class GenerativeData : ScriptableObject
	{
		[System.Serializable]
		public class Difficulty
		{
			public int stages = 1;
			public float defaultTImer = 3;
			public Vector2Int boardSize = new Vector2Int(6, 6);
			public List<Board> boardsPool = new List<Board>();
		}

		[System.Serializable]
		public class Board
		{
			public List<ActivelElectricNodeAuthoring> activeNodes = new List<ActivelElectricNodeAuthoring>();
		}

		// index is difficulty
		public List<Difficulty> difficulties = new List<Difficulty>();
	}
}