using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;

namespace NeuronNetwork
{
	public class Out : Neuron, ISpecialAbilities
	{
		private double delta;
		public double idealValue;

		public List<Synapse> neuronSynapsesIn = new List<Synapse>();

		public Out(Layer nextLayer)
		{ }

		public override void calculateOutValue()
		{
			double inValue = 0;
			for (int i = 0; i < neuronSynapsesIn.Count; i++)
			{
				inValue += (neuronSynapsesIn[i].weight * neuronSynapsesIn[i].a.outValue);
			}
			outValue = activationFunction(inValue);
		}

		public double getDelta()
		{
			setDelta();
			return delta;
		}

		private void setDelta()
		{
			double derivativeActivationFunction = (1 - outValue) * outValue;
			delta = derivativeActivationFunction * (idealValue - outValue);
		}

		public void addSynapsesIn(Synapse synapseIn)
		{
			neuronSynapsesIn.Add(synapseIn);
		}

		public override string getType()
		{
			return Neuron.OUT;
		}

		public void updateSynapses()
		{
			for (int i = 0; i < neuronSynapsesIn.Count; i++)
				neuronSynapsesIn[i].updateWeight();
		}
	}
}