using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class QuantizationAndEncoding : Algorithm
    {
        // You will have only one of (InputLevel or InputNumBits), the other property will take a negative value
        // If InputNumBits is given, you need to calculate and set InputLevel value and vice versa
        public int InputLevel { get; set; }
        public int InputNumBits { get; set; }
        public Signal InputSignal { get; set; }
        public Signal OutputQuantizedSignal { get; set; }
        public List<int> OutputIntervalIndices { get; set; }
        public List<string> OutputEncodedSignal { get; set; }
        public List<float> OutputSamplesError { get; set; }
        public List<float> quantiSil { get; set; }

        public override void Run()
        {

            OutputSamplesError = new List<float>();
            OutputIntervalIndices = new List<int>();
            OutputEncodedSignal = new List<string>();
            quantiSil = new List<float>();


            if (InputLevel == 0)
            {
                InputLevel = (int)Math.Pow(2, InputNumBits);
            }
            else if (InputNumBits == 0)
            {
                InputNumBits = (int)Math.Log(InputLevel, 2);
            }

            float a = (InputSignal.Samples.Max() - InputSignal.Samples.Min()) / InputLevel;
            float mi = InputSignal.Samples.Min();

            List<List<float>> myList = new List<List<float>>();
            for (int i = 0; i < InputLevel; i++)
            {
                Console.WriteLine(mi);
                myList.Add(new List<float> { mi, mi + a });
                mi += a;
            }

            List<float> mid = new List<float>();
            for (int y = 0; y < myList.Count(); y++)
            {
                mid.Add((float)((myList[y].Min() + myList[y].Max()) / 2));
            }




            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                for (int y = 0; y < myList.Count; y++)
                {
                    if (InputSignal.Samples[i] >= myList[y].Min() && InputSignal.Samples[i] < myList[y].Max()+0.0001)
                    {

                        quantiSil.Add(mid[y]);

                        OutputEncodedSignal.Add(Convert.ToString(y, 2).PadLeft(InputNumBits, '0'));
                        OutputIntervalIndices.Add(y + 1);
                        OutputSamplesError.Add((float)(mid[y] - InputSignal.Samples[i]));
                    }

                }
            }

            OutputQuantizedSignal = new Signal(quantiSil, false);
        }
    }
}


