using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;
using System.Numerics;

namespace DSPAlgorithms.Algorithms
{
    public class InverseDiscreteFourierTransform : Algorithm
    {
        public Signal InputFreqDomainSignal { get; set; }
        public Signal OutputTimeDomainSignal { get; set; }

        public override void Run()
        {
            List<Complex> Comp = new List<Complex>();

            List<float> Amp = InputFreqDomainSignal.FrequenciesAmplitudes;
            List<float> Phase = InputFreqDomainSignal.FrequenciesPhaseShifts;
            List<float> Samples = new List<float>();

            int N = InputFreqDomainSignal.FrequenciesAmplitudes.Count;
            Console.WriteLine("In IDFT");
            Console.WriteLine(N);
            for (int i = 0; i < N; i++)
            {
                float Real = Amp[i] * (float)Math.Cos(Phase[i]);
                float Imaginary = Amp[i] * (float)Math.Sin(Phase[i]);

                Comp.Add(new Complex(Real, Imaginary));
            }

            for (int k = 0; k < N; k++)
            {
                Complex sum = 0;
                for (int n = 0; n < N; n++)
                {
                    float angle = (float)((2 * Math.PI * k * n) / N);
                    sum += ((Comp[n]) * (Complex.Exp(new Complex(0, angle))));
                }

                Samples.Add((float)(sum.Real * 1 / N));
            }

            OutputTimeDomainSignal = new Signal(Samples, false);

        }
    }
}
