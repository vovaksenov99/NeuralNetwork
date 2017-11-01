using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Web;

namespace NeuronNetwork
{
	public interface ISpecialAbilities
	{
		double getDelta();
		void addSynapsesIn(Synapse synapsesIn);
		void updateSynapses();
	}

	public abstract class Neuron
	{
		public const string INPUT = "Input", HIDDEN = "Hidden", OUT = "Out", BIAS = "Bias";

		public double outValue;

		public abstract void calculateOutValue();

		public abstract string getType();

		public double activationFunction(double value)
		{
			return 1.0 / (1.0 + Math.Exp(-value));
		}
	}
}