using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FastConvolution : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputConvolvedSignal { get; set; }

        /// <summary>
        /// Convolved InputSignal1 (considered as X) with InputSignal2 (considered as H)
        /// </summary>
        public override void Run()
        {

            Console.WriteLine(InputSignal1.Samples[1]);
            Console.WriteLine(InputSignal1.Samples[2]);
            int cu = InputSignal1.Samples.Count+ InputSignal2.Samples.Count -1;

            for(int i = 0; i<cu;i++)
            {
                if (i >= InputSignal1.Samples.Count)
                {
                    InputSignal1.Samples.Add(0);
                }

                if (i >= InputSignal2.Samples.Count)
                {

                    InputSignal2.Samples.Add(0);
                }
             }

            for (int i = 0; i < cu; i++)
            {
                Console.WriteLine(InputSignal1.Samples[i]);
                Console.WriteLine(InputSignal2.Samples[i]); 
            }

                Console.WriteLine(InputSignal1.Samples.Count );
            Console.WriteLine(InputSignal2.Samples.Count );

            Console.WriteLine(InputSignal1.Samples[1]);
            List<Complex> X11 = new List<Complex>();
            DiscreteFourierTransform DFT = new DiscreteFourierTransform();
            DFT.InputTimeDomainSignal = new DSPAlgorithms.DataStructures.Signal(InputSignal1.Samples, InputSignal1.Periodic);
            DFT.Run();
            Console.WriteLine(DFT.OutputFreqDomainSignal.FrequenciesAmplitudes[1]);
            Console.WriteLine(DFT.OutputFreqDomainSignal.FrequenciesAmplitudes[1]);

            for (int i = 0; i < DFT.OutputFreqDomainSignal.FrequenciesAmplitudes.Count; i++)
            {

                Complex s = new Complex(DFT.OutputFreqDomainSignal.FrequenciesAmplitudes[i] * (float)Math.Cos(DFT.OutputFreqDomainSignal.FrequenciesPhaseShifts[i]),
                     DFT.OutputFreqDomainSignal.FrequenciesAmplitudes[i] * (float)Math.Sin(DFT.OutputFreqDomainSignal.FrequenciesPhaseShifts[i]));


                X11.Add(s);


            }

            List<Complex> X12 = new List<Complex>();
            DiscreteFourierTransform DFfT = new DiscreteFourierTransform();

            List<float> Amp = new List<float>();
            List<float> Phase = new List<float>();

            DFfT.InputTimeDomainSignal = new DSPAlgorithms.DataStructures.Signal(InputSignal2.Samples, InputSignal1.Periodic);

            DFfT.Run();
            for (int i = 0; i < DFfT.OutputFreqDomainSignal.FrequenciesAmplitudes.Count; i++)
            {



                Complex s = new Complex(DFfT.OutputFreqDomainSignal.FrequenciesAmplitudes[i] * (float)Math.Cos(DFfT.OutputFreqDomainSignal.FrequenciesPhaseShifts[i]),
                     DFfT.OutputFreqDomainSignal.FrequenciesAmplitudes[i] * (float)Math.Sin(DFfT.OutputFreqDomainSignal.FrequenciesPhaseShifts[i]));


                s = s * X11[i];
                //   Console.WriteLine(s);
                Amp.Add((float)s.Magnitude);
                Phase.Add((float)(Math.Atan2(s.Imaginary, s.Real)));

            }




            InverseDiscreteFourierTransform IDFT = new InverseDiscreteFourierTransform();
            // test case 2

            var Frequencies = new List<float> { 0, 1, 2, 3, 4, 5, 6, 7 };
            OutputConvolvedSignal = new Signal(new List<float>(), new List<int>(), InputSignal1.Periodic);

            IDFT.InputFreqDomainSignal = new DSPAlgorithms.DataStructures.Signal(true, Frequencies, Amp, Phase);
            IDFT.Run();
            Console.WriteLine("*************************===========*************");



          //  OutputConvolvedSignal.Samples = IDFT.OutputTimeDomainSignal.Samples;
            for (int i = 0; i < IDFT.OutputTimeDomainSignal.Samples.Count(); i++)
            {
                OutputConvolvedSignal.Samples.Add(IDFT.OutputTimeDomainSignal.Samples[i] );

               // OutputConvolvedSignal.SamplesIndices.Add(i);
               Console.WriteLine(IDFT.OutputTimeDomainSignal.Samples[i]);
                 // IDFT.OutputTimeDomainSignal.Samples[i]*= IDFT.OutputTimeDomainSignal.Samples.Count();
            }
















        }
    }
}
