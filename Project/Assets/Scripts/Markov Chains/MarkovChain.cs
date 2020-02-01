namespace GGJ2020
{
	public static class MarkovChain
	{
		public struct MarkovChainData
		{
			public float[][] transitionMatrix;

			public bool IsValid
			{
				get
				{
					int c = transitionMatrix.Length;
					foreach (var r in transitionMatrix)
					{
						if (r.Length != c)
						{
							return false;
						}
					}
					return true;
				}
			}

			public int Lenght
			{
				get
				{
					if (this.IsValid)
					{
						return transitionMatrix.Length;
					}
					else
					{
						throw new System.Exception("Markov Chain data not valid.");
					}
				}
			}
		}

		public static int GenerateNext(int previous, in MarkovChainData data)
		{
			int lenght = data.Lenght;

			if (previous < 0 || previous > lenght - 1)
			{
				throw new System.Exception("Previous not valid.");
			}

			// Weights sum
			float weightsSum = 0;
			for (int i = 0; i < lenght; i++)
			{
				weightsSum += data.transitionMatrix[previous][i];
			}

			// Generate a random value between 0 and 1
			float rand = UnityEngine.Random.Range(0, weightsSum);

			// The probability of the current element plus the probability of all the
			// elements before
			float cumulative = 0;

			for (int a = 0; a < lenght; a++)
			{
				// Add the probability of this element
				cumulative += data.transitionMatrix[previous][a];

				// Compare the cumulative probability
				if (rand < cumulative)
				{
					return a;
				}
			}

			throw new System.Exception("?");
		}
	}
}