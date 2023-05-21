using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Derivatives: Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal FirstDerivative { get; set; }
        public Signal SecondDerivative { get; set; }

        public override void Run()
        {
            List<float> Outp = new List<float>();
            for (int i = 1 ; i <= InputSignal.Samples.Count-1; i++)
            {
                Outp.Add(InputSignal.Samples[i]- InputSignal.Samples[i-1]);
            }
            FirstDerivative = new Signal(Outp, false);
            List<float> Outp2 = new List<float>();
            for (int i = 1; i < InputSignal.Samples.Count-1; i++)
            {
                Outp2.Add(InputSignal.Samples[i+1] -2 *InputSignal.Samples[i] + InputSignal.Samples[i - 1]);
            }
            SecondDerivative = new Signal(Outp2, false);
        }
    }
}
