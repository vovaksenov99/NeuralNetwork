using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NeuronNetwork
{
	/**
	 * Show infomation about neuron network with define output type
	 **/
	class NetworkInfo
	{

		const string NEW_LINE = "\n", DELIMETER = "-----------\n";
		public const int CONSOLE_OUTPUT = 0, MESSAGEBOX_OUTPUT = 1, FILE_OUTPUT = 2;

		/**
		 * 0 - console(default)
		 * 1 - messageBox
		 * 2 - write in file (experemental)
		 * */
		int outputType = 0;

		public NetworkInfo(int outputType)
		{
			this.outputType = outputType;
		}

		public string getCurrentNetworkState(NeuronNetwork network)
		{
			string info = "";
			for (int i = 0; i < network.layersCount; i++)
			{

				info += getLayerInfo(network, i);
			}
			return info;
		}

		public string getLayerInfo(NeuronNetwork network, int layer)
		{
			string info = "";
			info += "Layer num " + layer.ToString();
			info += NEW_LINE;
			for (int i = 0; i < network.networkLayers[layer].neuronsCount; i++)
			{
				info += getNeuronInfo(network.networkLayers[layer].layerNeurons[i]);
			}
			return info;
		}

		public string getNeuronInfoByType(NeuronNetwork network, string type)
		{
			string info = "";
			for (int i = 0; i < network.layersCount; i++)
			{
				for (int j = 0; j < network.networkLayers[i].neuronsCount; j++)
				{
					Neuron neuron = network.networkLayers[i].layerNeurons[j];
					if (type.Equals(neuron.getType()))
					{
						info += getNeuronInfo(neuron);
					}
				}
			}
			return info;
		}

		public string getNeuronInfo(Neuron neuron)
		{
			string info = "";
			info += string.Format("Type = {0}", neuron.getType());
			info += NEW_LINE;
			info += string.Format("OutValue = {0}", neuron.outValue);
			info += NEW_LINE;
			info += DELIMETER;
			info += getNeuronInSynapses(neuron);
			info += DELIMETER;
			info += getNeuronOutSynapses(neuron);
			info += DELIMETER;
			return info;
		}

		public string getNeuronInSynapses(Neuron neuron)
		{
			string info = "InSynapses: \n";
			switch (neuron.getType())
			{
				case Neuron.BIAS:
					info += "none\n";
					break;
				case Neuron.HIDDEN:
					Hidden neuronHidden = (Hidden)neuron;
					for (int i = 0; i < neuronHidden.neuronSynapsesIn.Count; i++)
						info += neuronHidden.neuronSynapsesIn[i].toString();
					break;
				case Neuron.INPUT:
					info += "none\n";
					break;
				case Neuron.OUT:
					Out neuronOut = (Out)neuron;
					for (int i = 0; i < neuronOut.neuronSynapsesIn.Count; i++)
						info += neuronOut.neuronSynapsesIn[i].toString();
					break;
			}
			return info;
		}

		public string getNeuronOutSynapses(Neuron neuron)
		{
			string info = "OutSynapses: \n";
			switch (neuron.getType())
			{
				case Neuron.BIAS:
					Bias neuronBias = (Bias)neuron;
					for (int i = 0; i < neuronBias.neuronSynapsesOut.Count; i++)
						info += neuronBias.neuronSynapsesOut[i].toString();
					break;
				case Neuron.HIDDEN:
					Hidden neuronHidden = (Hidden)neuron;
					for (int i = 0; i < neuronHidden.neuronSynapsesOut.Count; i++)
						info += neuronHidden.neuronSynapsesOut[i].toString();
					break;
				case Neuron.INPUT:
					Input neuronInput = (Input)neuron;
					for (int i = 0; i < neuronInput.neuronSynapsesOut.Count; i++)
						info += neuronInput.neuronSynapsesOut[i].toString();
					break;
				case Neuron.OUT:
					info += "none\n";
					break;
			}
			return info;
		}

		public void displayInfo(string info)
		{
			if (outputType == CONSOLE_OUTPUT)
				Console.WriteLine(info);
			if (outputType == MESSAGEBOX_OUTPUT)
				MessageBox.Show(info);
			if (outputType == FILE_OUTPUT)
			{
				//TODO: create file output
			}
		}
	}
}