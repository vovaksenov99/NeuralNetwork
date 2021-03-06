﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuronNetwork
{
	public partial class MainActivity : Form
	{
		public MainActivity()
		{
			InitializeComponent();
		}

		private void MainActivity_Load(object sender, EventArgs e)
		{
			/**
			 * Use sample 
			 * 
			 * 
			 * */
			NetworkInfo info = new NetworkInfo(NetworkInfo.CONSOLE_OUTPUT);

			NeuronNetwork network = new NeuronNetwork();

			info.displayInfo(info.getCurrentNetworkState(network));

			TrainingController controller = new TrainingController();
			controller.trainNetwork(ref network, new string[] { "trainingSet.txt" });

			double[] rez = network.execute(new double[] { 0, 0 });

			info.displayInfo(info.getCurrentNetworkState(network));
		}
	}
}
