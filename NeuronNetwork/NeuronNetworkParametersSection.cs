using System.Configuration;

namespace NeuronNetwork
{
	public class NeuronNetworkParametersSection : ConfigurationSection
	{
		[ConfigurationProperty("BaseParameters")]
		public NeuronNetworkParametersCollection NeuronNetworkParametersItems
		{
			get { return ((NeuronNetworkParametersCollection)(base["BaseParameters"])); }
		}
	}

	[ConfigurationCollection(typeof(NeuronNetworkParametersElement))]
	public class NeuronNetworkParametersCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new NeuronNetworkParametersElement();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			//TODO: понять что тут происходит
			return "GetElementKey was called";
		}

		public NeuronNetworkParametersElement this[int idx]
		{
			get { return (NeuronNetworkParametersElement)BaseGet(idx); }
		}
	}
	public class NeuronNetworkParametersElement : ConfigurationElement
	{

		[ConfigurationProperty("SPEED", DefaultValue = "", IsRequired = true)]
		public string SPEED
		{
			get { return ((string)(base["SPEED"])); }
			set { base["SPEED"] = value; }
		}

		[ConfigurationProperty("MOMENT", DefaultValue = "", IsRequired = true)]
		public string MOMENT
		{
			get { return ((string)(base["MOMENT"])); }
			set { base["MOMENT"] = value; }
		}

		[ConfigurationProperty("INPUT_NEURONS_COUNT", DefaultValue = "", IsRequired = true)]
		public string INPUT_NEURONS_COUNT
		{
			get { return ((string)(base["INPUT_NEURONS_COUNT"])); }
			set { base["INPUT_NEURONS_COUNT"] = value; }
		}

		[ConfigurationProperty("HIDDEN_LAYER_COUNT", DefaultValue = "", IsRequired = true)]
		public string HIDDEN_LAYER_COUNT
		{
			get { return ((string)(base["HIDDEN_LAYER_COUNT"])); }
			set { base["HIDDEN_LAYER_COUNT"] = value; }
		}

		/**
		 * format "1,2,3...etc"
		 * */
		[ConfigurationProperty("HIDDEN_NEURONS_ON_LAYERS_COUNT", DefaultValue = "", IsRequired = true)]
		public string HIDDEN_NEURONS_ON_LAYERS_COUNT
		{
			get { return ((string)(base["HIDDEN_NEURONS_ON_LAYERS_COUNT"])); }
			set { base["HIDDEN_NEURONS_ON_LAYERS_COUNT"] = value; }
		}

		[ConfigurationProperty("OUT_NEURONS_COUNT", DefaultValue = "", IsRequired = true)]
		public string OUT_NEURONS_COUNT
		{
			get { return ((string)(base["OUT_NEURONS_COUNT"])); }
			set { base["OUT_NEURONS_COUNT"] = value; }
		}

		[ConfigurationProperty("USAGE_BIAS", DefaultValue = "", IsRequired = true)]
		public string USAGE_BIAS
		{
			get { return ((string)(base["USAGE_BIAS"])); }
			set { base["USAGE_BIAS"] = value; }
		}

		[ConfigurationProperty("MIN_SYNAPS_WEIGHT", DefaultValue = "", IsRequired = true)]
		public string MIN_SYNAPS_WEIGHT
		{
			get { return ((string)(base["MIN_SYNAPS_WEIGHT"])); }
			set { base["MIN_SYNAPS_WEIGHT"] = value; }
		}

		[ConfigurationProperty("MAX_SYNAPS_WEIGHT", DefaultValue = "", IsRequired = true)]
		public string MAX_SYNAPS_WEIGHT
		{
			get { return ((string)(base["MAX_SYNAPS_WEIGHT"])); }
			set { base["MAX_SYNAPS_WEIGHT"] = value; }
		}
	}
}