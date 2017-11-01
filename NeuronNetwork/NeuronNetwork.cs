using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace NeuronNetwork
{
	class NeuronNetwork
	{
		public static Random rand = new Random();

		//config network parameters
		public static NeuronNetworkParametersElement networkParameters = ((NeuronNetworkParametersSection)ConfigurationManager.GetSection("NeuronNetworkParameters")).NeuronNetworkParametersItems[0];

		public const string NAMESPACE = "NeuronNetwork";

		public Layer[] networkLayers;

		public int layersCount;

		public NeuronNetwork()
		{
			buildNetwork();
		}

		private void buildNetwork()
		{
			// hidden layers + out layer + input layer
			layersCount = Convert.ToInt32(networkParameters.HIDDEN_LAYER_COUNT) + 2;

			networkLayers = new Layer[layersCount];

			//initialization order is important
			//YOU SHALL NOT SWAP
			buildOutLayer();
			buildHiddenLayers();
			buildInputLayer();
		}

		private void buildOutLayer()
		{
			int neuronsCount = Convert.ToInt32(networkParameters.OUT_NEURONS_COUNT);
			Dictionary<string, int> describeLayer = new Dictionary<string, int>();
			describeLayer.Add(Neuron.OUT, neuronsCount);
			networkLayers[layersCount - 1] = new Layer(describeLayer, null);
		}

		private void buildHiddenLayers()
		{
			int[] hiddenLayerNeuronsCount = networkParameters.HIDDEN_NEURONS_ON_LAYERS_COUNT.Split(',').Select(int.Parse).ToArray();

			for (int currentHiddenLayer = hiddenLayerNeuronsCount.Length - 1; currentHiddenLayer >= 0; currentHiddenLayer--)
			{
				Dictionary<string, int> describeLayer = new Dictionary<string, int>();
				describeLayer.Add(Neuron.HIDDEN, hiddenLayerNeuronsCount[currentHiddenLayer]);
				if (Convert.ToBoolean(networkParameters.USAGE_BIAS))
					describeLayer.Add(Neuron.BIAS, 1);

				int currentHiddenLayerPosition = currentHiddenLayer + 1;
				int nextLayerPosition = currentHiddenLayerPosition + 1;

				networkLayers[currentHiddenLayerPosition] = new Layer(describeLayer, networkLayers[nextLayerPosition]);
			}
		}

		private void buildInputLayer()
		{
			int neuronsCount = Convert.ToInt32(networkParameters.INPUT_NEURONS_COUNT);
			Dictionary<string, int> describeLayer = new Dictionary<string, int>();
			describeLayer.Add(Neuron.INPUT, neuronsCount);
			networkLayers[0] = new Layer(describeLayer, networkLayers[1]);
		}

		private void defineInputValues(double[] values)
		{
			for (int i = 0; i < values.Length; i++)
				((Input)networkLayers[0].layerNeurons[i]).inValue = values[i];
		}

		public double execute(double[] values)
		{
			defineInputValues(values);
			for (int currentLayer = 0; currentLayer < layersCount; currentLayer++)
			{
				for (int currentNeuronNum = 0; currentNeuronNum < networkLayers[currentLayer].neuronsCount; currentNeuronNum++)
				{
					Neuron currentNeuron = networkLayers[currentLayer].layerNeurons[currentNeuronNum];
					currentNeuron.calculateOutValue();
				}
			}
			//TODO: modify for several neurons
			return networkLayers[layersCount - 1].layerNeurons[networkLayers[layersCount - 1].neuronsCount - 1].outValue;
		}

		public void correctSynapsesValues()
		{
			for (int currentLayer = layersCount - 1; currentLayer >= 1; currentLayer--)
			{
				for (int currentNeuronNum = 0; currentNeuronNum < networkLayers[currentLayer].neuronsCount; currentNeuronNum++)
				{
					Neuron currentNeuron = networkLayers[currentLayer].layerNeurons[currentNeuronNum];
					if (!currentNeuron.getType().Equals(Neuron.BIAS))
						((ISpecialAbilities)currentNeuron).updateSynapses();
				}
			}
		}
	}
}