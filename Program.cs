using System;
using System.Collections.Generic;

namespace TempSensorExp
{
    class Program
    {

        public static string folder = "C:\\Users\\steve\\Desktop\\FTO\\{0}.csv";
        private static System.Timers.Timer aTimer;
        public static string beginURL = "http://localhost:22002/NeuLogAPI?StartExperiemnt:[Temperature],";
        static void Main(string[] args)
        {
            
        }

        //method to begin the experiment, rate = sample rate/second and samples =
        // total number of samples taken
        private static void StartExperitment(int rate, int samples)
        {
            string theRate = rate.ToString();
            string sampleNumber = samples.ToString();

        }
    }
}
