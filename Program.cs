﻿using System;
using System.Collections.Generic;

namespace TempSensorExp
{
    class Program
    {

        public static string folder = "C:\\Users\\steve\\Desktop\\FTO\\{0}.csv";
        private static System.Timers.Timer aTimer;
        public static string beginURL = "http://localhost:22002/NeuLogAPI?StartExperiemnt:[Temperature],[{0}],[{1}]";
        static void Main(string[] args)
        {
            StartExperitment(1, 100);
        }

        //method to begin the experiment, rate = sample rate/second and samples =
        // total number of samples taken
        private static void StartExperitment(int rate, int samples)
        {
            //change the input arguments to strings
            string theRate = rate.ToString();
            string sampleNumber = samples.ToString();

            //add the inputs to the "beginURL" string
            string beginPath = string.Format(beginURL, theRate, sampleNumber);
            Console.WriteLine(beginPath);


        }
    }
}
