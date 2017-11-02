using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuronNetwork
{
	/**
	 * Class with tests fot network
	 * */
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

	/**
	 * Manage all training work
	 * */
	class TrainingController
	{
		public TrainingSet[] trainingSets;

		public void trainNetwork(ref NeuronNetwork network, string[] trainingSetsNames)
		{
			trainingSets = new TrainingSet[trainingSetsNames.Length];
			for (int i = 0; i < trainingSetsNames.Length; i++)
				trainingSets[i] = new TrainingSet(trainingSetsNames[i]);

			for (int i = 0; i < trainingSets.Length; i++)
				for (int j = 0; j < trainingSets[i].trainingSet.Length; j++)
					trainingTest(ref network, trainingSets[i].trainingSet[j]);
		}


		/**
		 * Train ONLY one test
		 * */
		private void trainingTest(ref NeuronNetwork network, double[] values)
		{
			int outNeuronCount = Convert.ToInt32(NeuronNetwork.networkParameters.OUT_NEURONS_COUNT);

			// give output value for out neurons
			for (int i = 0; i < outNeuronCount; i++)
			{
				((Out)network.networkLayers[network.layersCount - 1].layerNeurons[i]).idealValue = values[values.Length - outNeuronCount + i];
			}
			// change synapse weight values
			network.correctSynapsesValues();
		}
	}
}
