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
	public class Input : Neuron
	{
		public double inValue;

		public List<Synapse> neuronSynapsesOut;

		public Input(Layer nextLayer)
		{
			buildNeuronSynapses(nextLayer);
		}

		public override void calculateOutValue()
		{
			outValue = inValue;
		}

		public void addSynapsesOut(Synapse synapsesOut)
		{
			neuronSynapsesOut.Add(synapsesOut);
		}

		public override string getType()
		{
			return Neuron.INPUT;
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
	}
}