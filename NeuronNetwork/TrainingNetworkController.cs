using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuronNetwork
{
	public class TrainingSet
	{
		public double[][] trainingSet;

		public TrainingSet(string trainingFileName)
		{
			parseTrainingSet(readTrainingFile(trainingFileName));
		}

		private string readTrainingFile(string trainingFileName)
		{
			string directoryPath = Path.GetDirectoryName(Application.ExecutablePath);
			string path = directoryPath + "/" + trainingFileName;

			string file;
			using (StreamReader sr = new StreamReader(path))
			{
				file = sr.ReadToEnd();
			}
			return file;
		}

		private void parseTrainingSet(string file)
		{
			string[] trainingsRow = file.Split('\n');

			trainingSet = new double[trainingsRow.Length][];

			for (int i = 0; i < trainingsRow.Length; i++)
				trainingSet[i] = trainingsRow[i].Split(' ').Select(double.Parse).ToArray();
		}
	}
	class TrainingController
	{
		public TrainingSet[] trainingSets;

		public void trainNetwork(ref NeuronNetwork network, string[] trainingNames)
		{
			trainingSets = new TrainingSet[trainingNames.Length];
			for (int i = 0; i < trainingNames.Length; i++)
				trainingSets[i] = new TrainingSet(trainingNames[i]);

			for (int i = 0; i < trainingSets.Length; i++)
				for (int j = 0; j < trainingSets[i].trainingSet.Length; j++)
					trainingTest(ref network, trainingSets[i].trainingSet[j]);
		}

		//run only single test
		private void trainingTest(ref NeuronNetwork network, double[] values)
		{
			int outNeuronCount = Convert.ToInt32(NeuronNetwork.networkParameters.OUT_NEURONS_COUNT);

			double rez = network.execute(values.Take(values.Length - outNeuronCount).ToArray());
			//Console.Write((values[2] - rez) * (values[2] - rez) * 100.0+"\n------------------------------------\n");
			for (int i=0;i< outNeuronCount;i++)
			{
				((Out)network.networkLayers[network.layersCount - 1].layerNeurons[i]).idealValue = values[values.Length - outNeuronCount + i];
			}
			NetworkInfo info = new NetworkInfo(NetworkInfo.CONSOLE_OUTPUT);
			//info.displayInfo(info.getCurrentNetworkState(network));
			network.correctSynapsesValues();
		}
	}
}
