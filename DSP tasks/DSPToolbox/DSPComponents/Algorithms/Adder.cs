using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Adder : Algorithm
    {
        public List<Signal> InputSignals { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            List<float> Outp = new List<float>();
            for (int i = 1; i < InputSignals.Count(); i++)
            {
                for (int y = 0; y < InputSignals[i].SamplesIndices.Count(); y++)
                {
                    Outp.Add(InputSignals[0].Samples[y] + InputSignals[i].Samples[y]);

                }


            }
            OutputSignal = new Signal(Outp, false);
        }
    }
}

