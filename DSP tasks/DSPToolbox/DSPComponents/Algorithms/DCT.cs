using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DCT : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            OutputSignal = new Signal(new List<float>(), false);
            float N = InputSignal.Samples.Count;
            float sum = 0;
            float output = 0;
            for (int k = 0; k < N; k++)
            {
                sum = 0;
                for (int n = 0; n < N; n++)
                {
                    sum += (float)(InputSignal.Samples[n] * (float)(Math.Cos(((float)(Math.PI) * (2 * n - 1) * (2 * k - 1)) / (4 * N))));
                }


                output = (float)Math.Sqrt(2 / N) * sum;

                Console.WriteLine(output);
                OutputSignal.Samples.Add(output);
            }


        }
    }
}
