using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace TempSensorExp
{
    class Program
    {

        public static string folder = "C:\\Users\\steve\\Desktop\\FTO\\{0}.csv";
        private static System.Timers.Timer aTimer;
        public static string beginURL = "http://localhost:22002/NeuLogAPI?StartExperiment:[Temperature],[1],[{0}],[{1}]";
        public static string stopURL = "http://localhost:22002/NeuLogAPI?StopExperiment";
        static void Main(string[] args)
        {
            //end any experiments if they're already running
            StopExperiment();

            Console.WriteLine("Would you like to begin the experiment?");
            Console.WriteLine("y/n");
            string input = Console.ReadLine();
            if (input == "y")
            {
                StartExperitment(11, 100);
            }
            else
            {
                Console.WriteLine("You have chosen to exit the program.");
                Environment.Exit(0);
            }
            Console.WriteLine("The experiment is running...");
            Console.WriteLine("Would you like to end the experiment?");
            Console.WriteLine("y/n");
            string exit = Console.ReadLine();
            if (exit == "y")
            {
                Console.WriteLine("You have chosen to end the experiment.");
                StopExperiment();
            }
            else
            {
                Console.WriteLine("The experiment will continue to run...");
            }

            
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

            //create the request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(beginPath);
            request.ContentType = "application/json; charset=utf-8";

            //capture the response
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                string json = reader.ReadToEnd();
                Start start = JsonConvert.DeserializeObject<Start>(json);
                Console.WriteLine(start.StartExperiment.ToString());
                if (start.StartExperiment.ToString() == "True")
                {
                    Console.WriteLine("The experiment has successfully begun.");
                }
                else
                {
                    Console.WriteLine("An error occurred connecting to the temperature sensor.");
                }
                
                
            }

        }

        private static void StopExperiment()
        {
            // initiate the request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(stopURL);
            request.ContentType = "application/json; charset=utf-8";

            //capture the response
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                string json = reader.ReadToEnd();
                Stop stop = JsonConvert.DeserializeObject<Stop>(json);
                if (stop.StopExperiment.ToString() == "True")
                {
                    Console.WriteLine("The experiment has successfully ended.");
                }
                else
                {
                    Console.WriteLine("An error occurred while trying to end the experiment.");
                }


            }
        }
    }
}
