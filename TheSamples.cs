using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TempSensorExp
{

    //class to handle the JSON from GetExperimentSamples
    class TheSamples
    {
        public JsonDictionaryAttribute GetExperimentSamples { get; set; }

        public TheSamples()
        {

        }
    }
}
