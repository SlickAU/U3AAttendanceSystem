using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = "";
            int output = 1;
            ServiceReference.ServiceClient client = new ServiceReference.ServiceClient();
            string returnString;

            Console.WriteLine("- Course Descriptions -\n\n");

            while(true)
            {
                Console.Write("\nPlease enter course Number to display it's Title: ");
                input = Console.ReadLine();
                Console.WriteLine("\nLoading...\n");
                Int32.TryParse(input, out output); 
                returnString = client.GetData(output);
                Console.WriteLine(String.Format("Course Number: {0,-20}\nCourse Title: {1,-20}", output, returnString));
            }

        }
    }
}
