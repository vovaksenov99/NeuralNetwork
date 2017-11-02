using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NeuronNetwork
{
	public class Layer
	{
		public int neuronsCount;

		public List<Neuron> layerNeurons;

		private Layer nextLayer;

		// neuronsDescribe<string,int> - <neuron type name, neuron count>
		public Layer(Dictionary<string, int> neuronsDescribe, Layer nextLayer)
		{
			this.nextLayer = nextLayer;

			buildLayer(neuronsDescribe);
		}

		void buildLayer(Dictionary<string, int> neuronsDescribe)
		{
			layerNeurons = new List<Neuron>();

			foreach (KeyValuePair<string, int> neuron in neuronsDescribe)
			{
				for (int j = 0; j < neuron.Value; j++)
					layerNeurons.Add(createNeuron(neuron.Key, new object[] { nextLayer }));
			}
			neuronsCount = layerNeurons.Count;
		}

		private Neuron createNeuron(string type, object[] parameters)
		{
			Type objectType = Type.GetType(NeuronNetwork.NAMESPACE + "." + type);
			ConstructorInfo ctor = objectType.GetConstructor(new Type[] { typeof(Layer) });
			return Activator.CreateInstance(objectType, parameters) as Neuron;
		}
	}
}