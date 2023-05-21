﻿using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSPAlgorithms.Algorithms
{
    public class Sampling : Algorithm
    {
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }




        public override void Run()
        {
            // throw new NotImplementedException();
            
            if (L >0 && M > 0) 
            {

                Signal s1 = new Signal(new List<float>(), new List<int>(), InputSignal.Periodic);
                int oh = InputSignal.SamplesIndices[0];
                for (int i = 0; i < InputSignal.Samples.Count; i++)
                {
                    s1.Samples.Add(InputSignal.Samples[i]);
                    s1.SamplesIndices.Add(oh);
                    int o = 0;
                    oh++;
                    while (o < L - 1)
                    {
                        o++;
                        s1.Samples.Add(0);
                        s1.SamplesIndices.Add(oh);
                        oh++;
                    }
                }
                Console.WriteLine(s1.Samples.Count);

                FIR F1 = new FIR();
                F1.InputTimeDomainSignal = s1;
                F1.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
                F1.InputFS = 8000;
                F1.InputStopBandAttenuation = 50;
                F1.InputCutOffFrequency = 1500;
                F1.InputTransitionBand = 500;
                F1.Run();
                

                OutputSignal = new Signal(new List<float>(), new List<int>(), InputSignal.Periodic);
                int ojh = F1.OutputYn.SamplesIndices[0];
                for(int i= 0;i<F1.OutputYn.Samples.Count;i+=M-1)
                {

                    if (i % M == 0)
                    {
                        OutputSignal.Samples.Add(F1.OutputYn.Samples[i]);
                        OutputSignal.SamplesIndices.Add(ojh);
                        ojh++;
                    }
                }

            }
            else if(L > 0 && M == 0)//sampling_UP
            {
                
                    Signal s1 = new Signal(new List<float>(), new List<int>(), InputSignal.Periodic);

                    int indi = InputSignal.SamplesIndices[0];
                    for (int i = 0; i < InputSignal.Samples.Count; i++)
                    {
                       s1.Samples.Add(InputSignal.Samples[i]);
                       s1.SamplesIndices.Add(indi);

                       indi++;
                        for (int j = 1; j < L; j++)
                        {
                          s1.Samples.Add(0);
                         s1.SamplesIndices.Add(indi);
                           indi++;
                        
                        }


                    }
                    FIR f = new FIR();
                    f.InputTimeDomainSignal = s1;
                    f.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
                    f.InputFS = 8000;
                    f.InputStopBandAttenuation = 50;
                    f.InputCutOffFrequency = 1500;
                    f.InputTransitionBand = 500;
                    f.Run();
                    OutputSignal = f.OutputYn;
                   
                }
            else if(L == 0 && M > 0)//sampling_DOWN
            {
                FIR F1 = new FIR();
                F1.InputTimeDomainSignal = InputSignal;
                F1.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.LOW;
                F1.InputFS = 8000;
                F1.InputStopBandAttenuation = 50;
                F1.InputCutOffFrequency = 1500;
                F1.InputTransitionBand = 500;
                F1.Run();

                Signal s1 = new Signal(new List<float>(), new List<int>(), InputSignal.Periodic);
                int oh = F1.OutputYn.SamplesIndices[0];
                for (int i = 0; i < F1.OutputYn.Samples.Count(); i += M)
                {
                    if (i % M == 0)
                    {
                        s1.Samples.Add(F1.OutputYn.Samples[i]);
                        s1.SamplesIndices.Add(oh);
                        oh++;
                    }

                }
                OutputSignal = s1;

             
            }
            else
            {
                Console.WriteLine("W & L = 0 | Panic !");
            }

        }
        }

}