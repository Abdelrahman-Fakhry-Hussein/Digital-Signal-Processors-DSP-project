using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DiscreteFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public float InputSamplingFrequency { get; set; 
        }
        public Signal OutputFreqDomainSignal { get; set; }

        public override void Run()
        {
            List<float> samples = InputTimeDomainSignal.Samples;

            List<float> Amp = new List<float>();
            List<float> Phase = new List<float>();
            List<float> Freq = new List<float>();
            for (int k = 0; k < samples.Count; k++)
            {
                Complex sum = new Complex(0,0);
                for (int n = 0; n < samples.Count; n++)
                {
                    double angle = (double) ((2 * Math.PI * k * n) / samples.Count);
                    sum += InputTimeDomainSignal.Samples[n] * Complex.Exp(new Complex(0, -angle));
                }

                Amp.Add((float) sum.Magnitude);
                Phase.Add((float) (Math.Atan2(sum.Imaginary, sum.Real)));
                Freq.Add((float)Math.Round(((Math.PI*k*  2*InputSamplingFrequency)) / (samples.Count),1));

            }
            OutputFreqDomainSignal = new Signal(InputTimeDomainSignal.Periodic, Freq, Amp, Phase);

            //  OutputFreqDomainSignal.FrequenciesAmplitudes = Amp;
            // OutputFreqDomainSignal.FrequenciesPhaseShifts = Phase;
        }
    }
}
           
