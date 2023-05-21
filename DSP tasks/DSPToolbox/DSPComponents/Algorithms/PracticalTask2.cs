﻿using DSPAlgorithms.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DSPAlgorithms.Algorithms;

namespace DSPAlgorithms.Algorithms
{
    public class PracticalTask2 : Algorithm
    {
        public String SignalPath { get; set; }
        public float Fs { get; set; }
        public float miniF { get; set; }
        public float maxF { get; set; }
        public float newFs { get; set; }
        public int L { get; set; } //upsampling factor
        public int M { get; set; } //downsampling factor
        public Signal OutputFreqDomainSignal { get; set; }
        void Display(Signal s11, string signalPaath)
        {
            using (StreamWriter writer = new StreamWriter(signalPaath))
            {
                bool Feqency = false;
                if (s11.FrequenciesAmplitudes == null)
                {
                    writer.WriteLine(0);
                    Feqency = false;

                }
                else if (s11.Samples == null)
                {
                    writer.WriteLine(1);
                    Feqency = true;
                }

                if (s11.Periodic == true)
                {
                    writer.WriteLine(1);
                }
                else
                {
                    writer.WriteLine(0);
                }
                if (Feqency == true)
                {
                    writer.WriteLine(s11.FrequenciesAmplitudes.Count);

                    for (int i = 0; i < s11.FrequenciesAmplitudes.Count; i++)
                    {
                        writer.Write(s11.Frequencies[i]);
                        writer.Write(" ");
                        writer.Write(s11.FrequenciesAmplitudes[i]);
                        writer.Write(" ");
                        writer.WriteLine(s11.FrequenciesPhaseShifts[i]);
                    }

                }
                else
                {
                    writer.WriteLine(s11.Samples.Count);
                    for (int i = 0; i < s11.Samples.Count; i++)
                    {
                        writer.Write(s11.SamplesIndices[i]);
                        writer.Write(" ");
                        writer.WriteLine(s11.Samples[i]);
                    }
                }
            }


        }
        public override void Run()
        {



            string Path = "C:/Users/abdel/Desktop/practical2_DSP";

            Signal InputSignal = LoadSignal(SignalPath);
            Display(InputSignal, Path + "/Original.txt");

            FIR FIR = new FIR();
            FIR.InputFilterType = DSPAlgorithms.DataStructures.FILTER_TYPES.BAND_PASS;
            FIR.InputFS = Fs;
            FIR.InputStopBandAttenuation = 50;
            FIR.InputF1 = miniF;
            FIR.InputF2 = maxF;
            FIR.InputTransitionBand = 500;
            FIR.InputTimeDomainSignal = InputSignal;

            FIR.Run();

            Display(FIR.OutputYn, Path + "/FIR_Display.txt");

            int iv = 0;
            Sampling s = new Sampling();
            Console.WriteLine("===============///////////////////////////////");
            Console.WriteLine(2 * maxF);
            Console.WriteLine(2 * FIR.OutputYn.Samples.Max());
            Console.WriteLine(newFs);
            if (newFs >= 2* maxF)
            {
                iv = 1;
                s.L = L;
                s.M = M;
                s.InputSignal = FIR.OutputYn;
                s.Run();
                Fs = newFs;
                Display(s.OutputSignal, Path + "/Sampleing_Display.txt");

            }
            else
            {
                iv = 0;
                Console.WriteLine("newFs is not valid");
                Console.WriteLine("==================================================///////////////////////////////");

            }
            DC_Component dc = new DC_Component();
            if (iv == 1) 
            {
                dc.InputSignal = s.OutputSignal;
                Console.WriteLine("------------------------------------------------------------------***************************");
            }
            else
            {
                dc.InputSignal= FIR.OutputYn;
            }
            dc.Run();
            Display(dc.OutputSignal, Path + "/DC_Display.txt");

            Normalizer a = new Normalizer();
            a.InputMinRange = -1;
            a.InputMaxRange = 1;
            a.InputSignal = dc.OutputSignal;

            a.Run();
            Display(a.OutputNormalizedSignal, Path + "/Normalize_Display.txt");

            DiscreteFourierTransform DFT = new DiscreteFourierTransform();
            // test case 2
            DFT.InputTimeDomainSignal = a.OutputNormalizedSignal;

            DFT.InputSamplingFrequency = Fs;

            /* if ( iv == 1)
             {
                 DFT.InputSamplingFrequency = newFs;
                 Console.WriteLine("===============///////////////////////////////**********");

             }
             else 
             {
             DFT.InputSamplingFrequency = Fs;

             }*/

            DFT.Run();
            Display(DFT.OutputFreqDomainSignal, Path + "/DFT_Display.txt");

            OutputFreqDomainSignal = DFT.OutputFreqDomainSignal;
            //throw new NotImplementedException();



            Console.WriteLine(DFT.OutputFreqDomainSignal.Frequencies[0]);
            Console.WriteLine(DFT.OutputFreqDomainSignal.Frequencies[1]);
            Console.WriteLine(DFT.OutputFreqDomainSignal.Frequencies[2]);


            Console.WriteLine(DFT.OutputFreqDomainSignal.FrequenciesAmplitudes[0]);
            Console.WriteLine(DFT.OutputFreqDomainSignal.FrequenciesAmplitudes[1]);
            Console.WriteLine(DFT.OutputFreqDomainSignal.FrequenciesAmplitudes[2]);


            Console.WriteLine(DFT.OutputFreqDomainSignal.FrequenciesPhaseShifts[0]);
            Console.WriteLine(DFT.OutputFreqDomainSignal.FrequenciesPhaseShifts[1]);
            Console.WriteLine(DFT.OutputFreqDomainSignal.FrequenciesPhaseShifts[2]);
        }

        

        public Signal LoadSignal(string filePath)
        {
            Stream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var sr = new StreamReader(stream);

            var sigType = byte.Parse(sr.ReadLine());
            var isPeriodic = byte.Parse(sr.ReadLine());
            long N1 = long.Parse(sr.ReadLine());

            List<float> SigSamples = new List<float>(unchecked((int)N1));
            List<int> SigIndices = new List<int>(unchecked((int)N1));
            List<float> SigFreq = new List<float>(unchecked((int)N1));
            List<float> SigFreqAmp = new List<float>(unchecked((int)N1));
            List<float> SigPhaseShift = new List<float>(unchecked((int)N1));

            if (sigType == 1)
            {
                SigSamples = null;
                SigIndices = null;
            }

            for (int i = 0; i < N1; i++)
            {
                if (sigType == 0 || sigType == 2)
                {
                    var timeIndex_SampleAmplitude = sr.ReadLine().Split();
                    SigIndices.Add(int.Parse(timeIndex_SampleAmplitude[0]));
                    SigSamples.Add(float.Parse(timeIndex_SampleAmplitude[1]));
                }
                else
                {
                    var Freq_Amp_PhaseShift = sr.ReadLine().Split();
                    SigFreq.Add(float.Parse(Freq_Amp_PhaseShift[0]));
                    SigFreqAmp.Add(float.Parse(Freq_Amp_PhaseShift[1]));
                    SigPhaseShift.Add(float.Parse(Freq_Amp_PhaseShift[2]));
                }
            }

            if (!sr.EndOfStream)
            {
                long N2 = long.Parse(sr.ReadLine());

                for (int i = 0; i < N2; i++)
                {
                    var Freq_Amp_PhaseShift = sr.ReadLine().Split();
                    SigFreq.Add(float.Parse(Freq_Amp_PhaseShift[0]));
                    SigFreqAmp.Add(float.Parse(Freq_Amp_PhaseShift[1]));
                    SigPhaseShift.Add(float.Parse(Freq_Amp_PhaseShift[2]));
                }
            }

            stream.Close();
            return new Signal(SigSamples, SigIndices, isPeriodic == 1, SigFreq, SigFreqAmp, SigPhaseShift);
        }
    }
}





