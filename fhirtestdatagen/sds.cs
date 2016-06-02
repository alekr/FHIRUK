using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fhirtestdatagen
{
    public class SDSConfigNodeLines : List<String>
    {
    }

    public class SDSConfigItem
    {
        private String configLine = String.Empty;
        private String key = String.Empty;
        private String value = String.Empty;

        public String Key { get { return key; } }
        public String Value { get { return value; } }

        public SDSConfigItem(String configLine)
        {
            this.configLine = configLine;

            ReadKeyAndValue();
        }

        private void ReadKeyAndValue()
        {
            int colonIndex = configLine.IndexOf(':');
            if (colonIndex > 0)
            {
                key = configLine.Substring(0, colonIndex);
                value = configLine.Substring(colonIndex + 2, configLine.Length - colonIndex - 2);
                while (value.StartsWith("$") == true)
                    value = value.Substring(1, value.Length - 1);
                if (value.EndsWith("$$") == true)
                    value = value.Substring(0, value.Length - 2);
                if (value.IndexOf('$') > 0)
                    value = value.Replace('$', ',');
            }
        }
    }

    public class SDSConfigNode: List<SDSConfigItem>
    {
        public SDSConfigNode(SDSConfigNodeLines lines)
        {
            foreach (String line in lines)
            {
                SDSConfigItem configItem = new SDSConfigItem(line);
                if (configItem.Key != String.Empty)
                {
                    this.Add(configItem);
                }
            }
        }

        public String GetValue(String key)
        {
            foreach (SDSConfigItem configItem in this)
            {
                if (key == configItem.Key)
                    return configItem.Value;
            }

            return null;
        }
    }
}
