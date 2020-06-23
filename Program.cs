using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public static string getSamples = "http://localhost:22002/NeuLogAPI?GetExperimentSamples:";
        static void Main(string[] args)
        {
            //end any experiments if they're already running
            StopExperiment();

            Console.WriteLine("Would you like to begin the experiment?");
            Console.WriteLine("y/n");
            string input = Console.ReadLine();
            if (input == "y")
            {
                //rate = 11 -- this is equal to 1 sample per second
                //samples = 100, the experiment will run for 100 seconds or until ended by user
                StartExperitment(11, 100);
            }
            else
            {
                Console.WriteLine("You have chosen to exit the program.");
                Environment.Exit(0);
            }
            Console.WriteLine("The experiment is running...");

            //let's see if we can view the samples
            Console.WriteLine("View the samples so far?");
            Console.WriteLine("y/n");
            string readline = Console.ReadLine();
            if (readline == "y")
            {
                GetSamples();
            }
            
            Console.WriteLine("Would you like to end the experiment?");
            Console.WriteLine("y/n");
            string exit = Console.ReadLine();
            if (exit == "y")
            {
                Console.WriteLine("You have chosen to end the experiment.");
                StopExperiment();
                GetSamples();
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

        private static void GetSamples()
        {
            //initiate the request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(getSamples);
            request.ContentType = "application/json; charset=utf-8";

            //capture the response
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                string json = reader.ReadToEnd();
                //TheSamples samples = JsonConvert.DeserializeObject<TheSamples>(json);
                Console.WriteLine(json);
                TheSamples contents = JsonConvert.DeserializeObject<TheSamples>(json);
                Sensor sensor = new Sensor();
                sensor.name = contents[0].ToString();
                sensor.id = contents[1].ToString();
                Console.WriteLine("This is a " + sensor.name.ToString() + " sensor. The sensor's ID is:" + sensor.id.ToString() + ".");
            }
        }
    }
}
