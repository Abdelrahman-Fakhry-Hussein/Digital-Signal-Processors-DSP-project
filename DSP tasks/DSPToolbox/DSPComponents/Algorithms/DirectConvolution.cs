using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {
            int n_n = InputSignal1.SamplesIndices.Min() + InputSignal2.SamplesIndices.Min();
            int n_m = InputSignal1.SamplesIndices.Max() + InputSignal2.SamplesIndices.Max();
            OutputConvolvedSignal = new Signal(new List<float>(), new List<int>(), InputSignal1.Periodic);

            for (int i = n_n; i <= n_m; i++)
            {
                Console.WriteLine("***");
                float su = 0;
                for (int y = n_n; y < InputSignal1.Samples.Count(); y++)
                {
                    if (y < InputSignal1.SamplesIndices.Min() || y > InputSignal1.SamplesIndices.Max())
                    {
                        continue;
                    }
                    if (i - y < InputSignal2.SamplesIndices.Min() || i - y > InputSignal2.SamplesIndices.Max())
                    {
                        continue;
                    }
                    int ind1 = InputSignal1.SamplesIndices.IndexOf(y);
                    int ind2 = InputSignal2.SamplesIndices.IndexOf(i - y);

                    su += InputSignal1.Samples[ind1] * InputSignal2.Samples[ind2];
                }
                if (i == n_m && su == 0.0)
                {
                    continue;
                }
                OutputConvolvedSignal.SamplesIndices.Add(i);
                OutputConvolvedSignal.Samples.Add(su);

                Console.WriteLine(su);
            }

        }
    }
}