using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Normalizer : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputMinRange { get; set; }
        public float InputMaxRange { get; set; }
        public Signal OutputNormalizedSignal { get; set; }

        public override void Run()
        {
            List<float> result = new List<float>();

            float mini = InputSignal.Samples[0];
            float Maximu = InputSignal.Samples[0];
            for (int i = 1; i < InputSignal.Samples.Count; i++)
            {

                if (InputSignal.Samples[i] < mini)
                {

                    mini = InputSignal.Samples[i];
                }
                if (InputSignal.Samples[i] > Maximu)
                {
                    Maximu = InputSignal.Samples[i];
                }
            }

            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                float norm = (InputSignal.Samples[i] - mini) / (Maximu - mini);
                float normalizedWithinARange = norm * (InputMaxRange - InputMinRange) + InputMinRange;
                result.Add(normalizedWithinARange);
            }
            OutputNormalizedSignal = new Signal(result, false);
        }
    }
}