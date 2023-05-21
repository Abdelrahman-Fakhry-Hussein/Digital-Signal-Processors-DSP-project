using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class DC_Component : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            float av = InputSignal.Samples.Average();
            List<float> outp = new List<float>();


            for (int i = 0; i < InputSignal.Samples.Count(); i++)
            {
                float m = (float)(InputSignal.Samples[i] - av);
                outp.Add(m);
            }
            OutputSignal = new Signal(outp, false);

        }
    }
}