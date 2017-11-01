using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;
using System.Windows.Forms;

namespace NeuronNetwork
{
	public class Hidden : Neuron, ISpecialAbilities
	{
		public double delta;

		public List<Synapse> neuronSynapsesIn = new List<Synapse>(), neuronSynapsesOut = new List<Synapse>();

		public Hidden(Layer nextLayer)
		{
			buildNeuronSynapses(nextLayer);
		}

		public void addSynapsesIn(Synapse synapseIn)
		{
			neuronSynapsesIn.Add(synapseIn);
		}

		public void addSynapsesOut(Synapse synapseOut)
		{
			neuronSynapsesOut.Add(synapseOut);
		}

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

		public override string getType()
		{
			return Neuron.HIDDEN;
		}

		private void setDelta()
		{
			double sum = 0;
			for (int i = 0; i < neuronSynapsesOut.Count; i++)
			{
					sum += ((ISpecialAbilities)neuronSynapsesOut[i].b).getDelta() * neuronSynapsesOut[i].weight;
			}
			double derivativeActivationFunction = (1 - outValue) * outValue;
			delta = derivativeActivationFunction * sum;
		}

		private void buildNeuronSynapses(Layer nextLayer)
		{
			neuronSynapsesOut = new List<Synapse>();
			for (int i = 0; i < nextLayer.neuronsCount; i++)
			{
				Synapse current = new Synapse(this, nextLayer.layerNeurons[i]);
				if (nextLayer.layerNeurons[i].getType().Equals(Neuron.BIAS))
					continue;
				addSynapsesOut(current);
				((ISpecialAbilities)neuronSynapsesOut[i].b).addSynapsesIn(current);
			}
		}

		public void updateSynapses()
		{
			for (int i = 0; i < neuronSynapsesIn.Count; i++)
			{
				neuronSynapsesIn[i].updateWeight();
			}
		}
	}
}