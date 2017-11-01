using System;
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

			//info.displayInfo(info.getCurrentNetworkState(network));
			int cc = 0;
			for (int i = 0; i < 100; i++)
			{
				NeuronNetwork network = new NeuronNetwork();
				TrainingController controller = new TrainingController();
				NetworkInfo info = new NetworkInfo(NetworkInfo.CONSOLE_OUTPUT);
				for (int j = 0; j < 600; j++)
					controller.trainNetwork(ref network, new string[] { "set.txt" });
				if (network.execute(new double[] { 0, 0 }) > 0.2)
				{ cc++; continue; }

				if (network.execute(new double[] { 0, 1 }) < 0.8)
				{ cc++; continue; }
				if (network.execute(new double[] { 1, 0 }) < 0.8)
				{ cc++; continue; }
				if (network.execute(new double[] { 1, 1 }) > 0.2)
				{ cc++; continue; }
			}
			Console.Write(cc.ToString());
			MessageBox.Show(cc.ToString());
			Close();
		}
	}
}
