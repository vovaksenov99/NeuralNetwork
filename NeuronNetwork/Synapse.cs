using System;

namespace NeuronNetwork
{
	public class Synapse
	{
		public Neuron a, b;

		public double weight, lastDelta = 0;

		public Synapse(Neuron a, Neuron b)
		{
			this.a = a;
			this.b = b;

			this.weight = getRandomWeight();
		}

		public Synapse(Neuron a, Neuron b, double weight)
		{
			this.a = a;
			this.b = b;

			this.weight = weight;
		}

		public void updateWeight()
		{
			double delta = Convert.ToDouble(NeuronNetwork.networkParameters.SPEED) * getGradient() + Convert.ToDouble(NeuronNetwork.networkParameters.MOMENT) * lastDelta;
			weight += delta;
			lastDelta = delta;
		}

		private double getGradient()
		{
			return ((ISpecialAbilities)b).getDelta() * a.outValue;
		}

		private double getRandomWeight()
		{
			double minValue = Convert.ToDouble(NeuronNetwork.networkParameters.MIN_SYNAPS_WEIGHT);
			double maxValue = Convert.ToDouble(NeuronNetwork.networkParameters.MAX_SYNAPS_WEIGHT);
			return minValue + NeuronNetwork.rand.NextDouble() * (maxValue - minValue);
		}

		public string toString()
		{
			return String.Format("[{0},{1}] with weight = {2}\n",a.getType(),b.getType(),weight);
		}
	}
}