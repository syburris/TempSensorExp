using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace TempSensorExp
{
    class Sensor
    {
        public string name { get; set; }

        public string id { get; set; }

        public double[] temp { get; set; }

        public Sensor()
        {

        }
    }
}
