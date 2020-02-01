using System.Linq;
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
		private class ElectricNode
		{
			public GameObject gameObject;
			public bool target; // whether is a goal

			// If a target
			public float timer;
			public float countdown;
			public FiniteStateMachine state;
		}

		public GameObject electricNodePrefab;

		private Vector2Int m_size;
		private ElectricNode[,] nodes;
		private const float nodeDistance = 0.5f;

		public void Initialize(GenerativeData.Difficulty data)
		{
			m_size = data.boardSize;
			List<ActivelElectricNodeAuthoring> activeNodesAuthoring = data.boardsPool[UnityEngine.Random.Range(0, data.boardsPool.Count)].activeNodes;

			nodes = new ElectricNode[m_size.x, m_size.y];

			// Calculate positions
			Vector2 lenght =  new Vector2(nodeDistance * (m_size.x - 1), nodeDistance * (m_size.y - 1));
			Vector2[,] translations = new Vector2[m_size.x, m_size.y];
			for (int x = 0; x < m_size.x; x++)
			{
				for (int y = 0; y < m_size.y; y++)
				{
					translations[x, y] = new Vector2(Mathf.Lerp(-lenght.x / 2.0f, lenght.x / 2.0f, x), Mathf.Lerp(lenght.y, 0, y));
				}
			}

			for (int x = 0; x < m_size.x; x++)
			{
				for (int y = 0; y < m_size.y; y++)
				{
					GameObject go = Instantiate(electricNodePrefab);
					go.transform.position = transform.position + new Vector3(translations[x, y].x, translations[x, y].y, 0);

					nodes[x, y] = new ElectricNode
					{
						gameObject = go,
					};

					// If active node
					if (activeNodesAuthoring.Any(n => n.position == new Vector2Int(x, y)))
					{
						ActivelElectricNodeAuthoring auth = activeNodesAuthoring.First(n => n.position == new Vector2Int(x, y));

						nodes[x, y].target = true;
						nodes[x, y].timer = auth.defaultTimer ? data.defaultTImer : auth.customTimer;
						nodes[x, y].countdown = -1;
						nodes[x, y].state = new FiniteStateMachine();
						nodes[x, y].state.SetStateData("READY");
						nodes[x, y].state.SetStateData("ACTIVE");
						nodes[x, y].state.SetStateData("INACTIVE");
						nodes[x, y].state.SetStateData("DONE");
						nodes[x, y].state.ChangeState("READY");
					}
					else
					{

					}
				}
			}
		}
	}
}
