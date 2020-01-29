# NeuralNetwork ![](https://img.shields.io/packagist/l/doctrine/orm.svg)  ![](https://img.shields.io/badge/Status-beta-orange.svg)
Open neuron network with extensible architecture and status logging.
## Basic network principles
	  
1. Network type is Feedforward
2. Use sygmoid activation function
3. Use backpropagation type for training
4. Start Neuron newtwork parameters define in App.config
5. Start synapses values are random number. Values range define in App.config.
	  
## WARNINGS:
First layer is **ONLY** input neurons

Last layer is **ONLY** output neurons

In other case neuron network result will be uncorrect or network will be crash.

## Config parameters
| Parameter  | type |
| ------------- | ------------- |
| SPEED | double value |
| MOMENT | double value |
| INPUT_NEURONS_COUNT | integer value |
| HIDDEN_LAYER_COUNT | integer value |
| HIDDEN_NEURONS_ON_LAYERS_COUNT | string with format "1,2,3" |
| OUT_NEURONS_COUNT | integer value |
| USAGE_BIAS | bool value "true" or "false" |
| MAX_SYNAPS_WEIGHT | double value |
| MIN_SYNAPS_WEIGHT | double value |

## Quick start
### usage example
``` csharp
private void MainActivity_Load(object sender, EventArgs e)
{
    /**
    * Use sample 
    * */
    NetworkInfo info = new NetworkInfo(NetworkInfo.CONSOLE_OUTPUT);

    NeuronNetwork network = new NeuronNetwork();

    info.displayInfo(info.getCurrentNetworkState(network));

    TrainingController controller = new TrainingController();
    controller.trainNetwork(ref network, new string[] { "trainingSet.txt" });

    double[] rez = network.execute(new double[] { 0, 0 });

    info.displayInfo(info.getCurrentNetworkState(network));
}
```
### Author
@lindlind and @vovaksenov99
