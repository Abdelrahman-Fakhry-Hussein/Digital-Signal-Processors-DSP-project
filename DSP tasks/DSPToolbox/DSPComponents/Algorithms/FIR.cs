using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.Algorithms;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FIR : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public FILTER_TYPES InputFilterType { get; set; }
        public float InputFS { get; set; }
        public float? InputCutOffFrequency { get; set; }
        public float? InputF1 { get; set; }
        public float? InputF2 { get; set; }
        public float InputStopBandAttenuation { get; set; }
        public float InputTransitionBand { get; set; }
        public Signal OutputHn { get; set; }
        public Signal OutputYn { get; set; }

        public override void Run()
        {
            // throw new NotImplementedException();
            // Console.WriteLine(InputFilterType == FILTER_TYPES.LOW);

            double N = 0;
            float Window;
            if (InputStopBandAttenuation <= 21)
            {


                N = (0.9 * InputFS / InputTransitionBand);
            }
            else if (InputStopBandAttenuation <= 44 && InputStopBandAttenuation > 21)
            {
                N = 3.1 * InputFS / InputTransitionBand;
            }
            else if (InputStopBandAttenuation <= 53)
            {
                N = 3.3 * InputFS  / InputTransitionBand;

            }
            else if (InputStopBandAttenuation <= 74 && InputStopBandAttenuation > 53)
            {
                N = 5.5 * InputFS / InputTransitionBand;

            }
            int N_int = (int)Math.Ceiling(N);
            if (N_int % 2 == 0)
            {
                N_int += 1;
            }

            int mid_N = (int)N_int / 2;

            Console.WriteLine(N_int);
            Console.WriteLine("N_int");



            Console.WriteLine("pre");
            Console.WriteLine(InputTimeDomainSignal.Periodic);
            OutputHn = new Signal(new List<float>(), new List<int>(), InputTimeDomainSignal.Periodic);

            if (InputFilterType == FILTER_TYPES.LOW)
            {


                Console.WriteLine("LOW");
                if (InputStopBandAttenuation <= 21)
                {
                    Console.WriteLine(InputStopBandAttenuation);
                    Console.WriteLine("in 21");
                    Window = 1;
                    float f_c = (float)(InputCutOffFrequency + (InputTransitionBand / 2.0));
                    f_c /= InputFS;
                    for (int i = -1*mid_N; i <= mid_N; i++)
                    {
                        float hd = 0;
                        if (i == 0)
                        {
                            hd = (float)2 * f_c;
                        }
                        else
                        {
                            hd = (float)(2 * f_c * Math.Sin((double)(i * 2 * Math.PI * f_c)) / (i * 2 * Math.PI * f_c));
                        }
                        OutputHn.Samples.Add((float)hd);
                        OutputHn.SamplesIndices.Add(i);


                    }
                   

                }
                else if (InputStopBandAttenuation <= 44 && InputStopBandAttenuation > 21)
                {
                    Console.WriteLine(InputStopBandAttenuation);
                    Console.WriteLine("in 44");
                    float f_c = (float)(InputCutOffFrequency + (InputTransitionBand / 2.0));
                    f_c /= InputFS;
                    for (int i = -1 * mid_N; i <= mid_N; i++)
                    {
                        Window = (float)(0.5 + 0.5 * Math.Cos((2 * Math.PI * i) / N_int));
                        float hd = 0;
                        if (i == 0)
                        {
                            hd = (float)2 * f_c;
                        }
                        else
                        {
                            hd = (float)(2 * f_c * Math.Sin((double)(i * 2 * Math.PI * f_c)) / (i * 2 * Math.PI * f_c));
                        }

                        OutputHn.Samples.Add((float)(hd * Window));
                        OutputHn.SamplesIndices.Add(i);


                    }
                   
                }
                else if (InputStopBandAttenuation <= 53 && InputStopBandAttenuation > 44)
                {
                    Console.WriteLine(InputStopBandAttenuation);


                    float f_c = (float)(InputCutOffFrequency + (InputTransitionBand / 2.0));
                    f_c /= InputFS;
                    Console.WriteLine("in 53");
                    Console.WriteLine(f_c);
                    Console.WriteLine(mid_N);
                    for (int i = -1 * mid_N; i <= mid_N; i++)
                    {
                        //Console.WriteLine("1");
                        Window = (float)((float)0.54 + (float)(0.46 * Math.Cos((2 * Math.PI * i) / N_int)));
                        float hd = 0;
                        if (i == 0)
                        {
                            hd = (float)(2 * f_c);
                        }
                        else
                        {
                            hd = (float)(2 * f_c * ((Math.Sin((i * 2 * Math.PI * f_c))) / (i * 2 * Math.PI * f_c)));
                        }

                        OutputHn.Samples.Add((float)(hd * Window));
                        OutputHn.SamplesIndices.Add(i);
                        Console.WriteLine((-i));
                        Console.WriteLine((hd * Window));

                    }        
                }
                else if (InputStopBandAttenuation <= 74 && InputStopBandAttenuation > 53)
                {
                    float f_c = (float)(InputCutOffFrequency + (InputTransitionBand / 2.0));
                    f_c /= InputFS;
                    for (int i = -1 * mid_N; i <= mid_N; i++)
                    {
                        Window = (float)(0.42 + 0.5 * Math.Cos((double)(2 * Math.PI * i) / (N_int - 1)) + 0.08 * Math.Cos((4 * Math.PI * i) / (N_int - 1)));
                        float hd = 0;
                        if (i == 0)
                        {
                            hd = (float)2 * f_c;
                        }
                        else
                        {
                            hd = (float)(2 * f_c * Math.Sin((double)(i * 2 * Math.PI * f_c)) / (i * 2 * Math.PI * f_c));
                        }

                        OutputHn.Samples.Add((float)(hd * Window));
                        OutputHn.SamplesIndices.Add(i);


                    }
                 
                }
            }
            else if (InputFilterType == FILTER_TYPES.HIGH)
            {

                Console.WriteLine("HIGH");
                if (InputStopBandAttenuation <= 21)
                {
                    Console.WriteLine(InputStopBandAttenuation);
                    Console.WriteLine("in 21");
                    Window = 1;
                    float f_c = (float)(InputCutOffFrequency - (InputTransitionBand / 2.0));
                    f_c /= InputFS;
                    for (int i = -1 * mid_N; i <= mid_N; i++)
                    {
                        float hd = 0;
                        if (i == 0)
                        {
                            hd = (float)1 - 2 * f_c;
                        }
                        else
                        {
                            hd = (float)(-2 * f_c * Math.Sin((double)(i * 2 * Math.PI * f_c)) / (i * 2 * Math.PI * f_c));
                        }
                        OutputHn.Samples.Add((float)hd);
                        OutputHn.SamplesIndices.Add(i);


                    }
                   
                }
                else if (InputStopBandAttenuation <= 44 && InputStopBandAttenuation > 21)
                {
                    Console.WriteLine(InputStopBandAttenuation);
                    Console.WriteLine("in 44");
                    float f_c = (float)(InputCutOffFrequency - (InputTransitionBand / 2.0));
                    f_c /= InputFS;
                    for (int i = -1 * mid_N; i <= mid_N; i++)
                    {
                        Window = (float)(0.5 + 0.5 * Math.Cos((2 * Math.PI * i) / N_int));
                        float hd = 0;
                        if (i == 0)
                        {
                            hd = (float)1 - 2 * f_c;
                        }
                        else
                        {
                            hd = (float)(-2 * f_c * Math.Sin((double)(i * 2 * Math.PI * f_c)) / (i * 2 * Math.PI * f_c));
                        }

                        OutputHn.Samples.Add((float)(hd * Window));
                        OutputHn.SamplesIndices.Add(i);


                    }
                    
                }
                else if (InputStopBandAttenuation <= 53 && InputStopBandAttenuation > 44)
                {
                    Console.WriteLine(InputStopBandAttenuation);


                    float f_c = (float)(InputCutOffFrequency - (InputTransitionBand / 2.0));
                    f_c /= InputFS;
                    Console.WriteLine("in 53");
                    Console.WriteLine(f_c);
                    Console.WriteLine(mid_N);
                    for (int i = -1 * mid_N; i <= mid_N; i++)
                    {
                        //Console.WriteLine("1");
                        Window = (float)(0.54 + 0.46 * Math.Cos((double)(2 * Math.PI * i) / N_int));
                        float hd = 0;
                        if (i == 0)
                        {
                            hd = (float)1 - 2 * f_c;
                        }
                        else
                        {
                            hd = (float)(-2 * f_c * Math.Sin((double)(i * 2 * Math.PI * f_c)) / (i * 2 * Math.PI * f_c));
                        }

                        OutputHn.Samples.Add((float)(hd * Window));
                        OutputHn.SamplesIndices.Add(i);
                        Console.WriteLine((-i));
                        Console.WriteLine((hd * Window));

                    }
                    
                }
                else if (InputStopBandAttenuation <= 74 && InputStopBandAttenuation > 53)
                {
                    float f_c = (float)(InputCutOffFrequency - (InputTransitionBand / 2.0));
                    f_c /= InputFS;
                    for (int i = -1 * mid_N; i <= mid_N; i++)
                    {
                        Window = (float)(0.42 + 0.5 * Math.Cos((double)(2 * Math.PI * i) / (N_int - 1)) + 0.08 * Math.Cos((4 * Math.PI * i) / (N_int - 1)));
                        float hd = 0;
                        if (i == 0)
                        {
                            hd = (float)1 - 2 * f_c;
                        }
                        else
                        {
                            hd = (float)(-2 * f_c * Math.Sin((double)(i * 2 * Math.PI * f_c)) / (i * 2 * Math.PI * f_c));
                        }

                        OutputHn.Samples.Add((float)(hd * Window));
                        OutputHn.SamplesIndices.Add(i);


                    }
                    
                }

            }
            else if (InputFilterType == FILTER_TYPES.BAND_PASS)
            {

                Console.WriteLine("BAND_PASS");
                if (InputStopBandAttenuation <= 21)
                {
                    Console.WriteLine(InputStopBandAttenuation);
                    Console.WriteLine("in 21");
                    Window = 1;
                    float f_c1 = (float)(InputF1 - (InputTransitionBand / 2.0));
                    f_c1 /= InputFS;
                    float f_c2 = (float)(InputF2 + (InputTransitionBand / 2.0));
                    f_c2 /= InputFS;
                    for (int i = -1 * mid_N; i <= mid_N; i++)
                    {
                        float hd = 0;
                        if (i == 0)
                        {
                            hd = (float)2 * (f_c2 - f_c1);
                        }
                        else
                        {
                            hd = (float)(2 * f_c2 * Math.Sin((double)(i * 2 * Math.PI * f_c2)) / (i * 2 * Math.PI * f_c2)) - (float)(2 * f_c1 * Math.Sin((double)(i * 2 * Math.PI * f_c1)) / (i * 2 * Math.PI * f_c1));
                        }
                        OutputHn.Samples.Add((float)hd);
                        OutputHn.SamplesIndices.Add(i);
                    }
                  

                }
                else if (InputStopBandAttenuation <= 44 && InputStopBandAttenuation > 21)
                {
                    Console.WriteLine(InputStopBandAttenuation);
                    Console.WriteLine("in 44");
                    float f_c1 = (float)(InputF1 - (InputTransitionBand / 2.0));
                    f_c1 /= InputFS;
                    float f_c2 = (float)(InputF2 + (InputTransitionBand / 2.0));
                    f_c2 /= InputFS;
                    for (int i = -1 * mid_N; i <= mid_N; i++)
                    {
                        Window = (float)(0.5 + 0.5 * Math.Cos((2 * Math.PI * i) / N_int));
                        float hd = 0;
                        if (i == 0)
                        {
                            hd = (float)2 * (f_c2 - f_c1);
                        }
                        else
                        {
                            hd = (float)(2 * f_c2 * Math.Sin((double)(i * 2 * Math.PI * f_c2)) / (i * 2 * Math.PI * f_c2)) - (float)(2 * f_c1 * Math.Sin((double)(i * 2 * Math.PI * f_c1)) / (i * 2 * Math.PI * f_c1));
                        }

                        OutputHn.Samples.Add((float)(hd * Window));
                        OutputHn.SamplesIndices.Add(i);


                    }
                   
                }
                else if (InputStopBandAttenuation <= 53 && InputStopBandAttenuation > 44)
                {
                    Console.WriteLine(InputStopBandAttenuation);


                    float f_c1 = (float)(InputF1 - (InputTransitionBand / 2.0));
                    f_c1 /= InputFS;
                    float f_c2 = (float)(InputF2 + (InputTransitionBand / 2.0));
                    f_c2 /= InputFS;
                    Console.WriteLine("in 53");

                    Console.WriteLine(mid_N);
                    for (int i = -1 * mid_N; i <= mid_N; i++)
                    {
                        //Console.WriteLine("1");
                        Window = (float)(0.54 + 0.46 * Math.Cos((double)(2 * Math.PI * i) / N_int));
                        float hd = 0;
                        if (i == 0)
                        {
                            hd = (float)2 * (f_c2 - f_c1);
                        }
                        else
                        {
                            hd = (float)(2 * f_c2 * Math.Sin((double)(i * 2 * Math.PI * f_c2)) / (i * 2 * Math.PI * f_c2)) - (float)(2 * f_c1 * Math.Sin((double)(i * 2 * Math.PI * f_c1)) / (i * 2 * Math.PI * f_c1));
                        }

                        OutputHn.Samples.Add((float)(hd * Window));
                        OutputHn.SamplesIndices.Add(i);
                        Console.WriteLine((-i));
                        Console.WriteLine((hd * Window));

                    }
                   
                }
                else if (InputStopBandAttenuation <= 74 && InputStopBandAttenuation > 53)
                {
                    float f_c1 = (float)(InputF1 - (InputTransitionBand / 2.0));
                    f_c1 /= InputFS;
                    float f_c2 = (float)(InputF2 + (InputTransitionBand / 2.0));
                    f_c2 /= InputFS;
                    for (int i = -1 * mid_N; i <= mid_N; i++)
                    {
                        Window = (float)(0.42 + 0.5 * Math.Cos((double)(2 * Math.PI * i) / (N_int - 1)) + 0.08 * Math.Cos((4 * Math.PI * i) / (N_int - 1)));
                        float hd = 0;
                        if (i == 0)
                        {
                            hd = (float)2 * (f_c2 - f_c1);
                        }
                        else
                        {
                            hd = (float)(2 * f_c2 * Math.Sin((double)(i * 2 * Math.PI * f_c2)) / (i * 2 * Math.PI * f_c2)) - (float)(2 * f_c1 * Math.Sin((double)(i * 2 * Math.PI * f_c1)) / (i * 2 * Math.PI * f_c1));
                        }

                        OutputHn.Samples.Add((float)(hd * Window));
                        OutputHn.SamplesIndices.Add(i);


                    }

                }

            }
            else if (InputFilterType == FILTER_TYPES.BAND_STOP)
            {
                Console.WriteLine("BAND_STOP");
                if (InputStopBandAttenuation <= 21)
                {
                    Console.WriteLine(InputStopBandAttenuation);
                    Console.WriteLine("in 21");
                    Window = 1;
                    float f_c1 = (float)(InputF1 + (InputTransitionBand / 2.0));
                    f_c1 /= InputFS;
                    float f_c2 = (float)(InputF2 - (InputTransitionBand / 2.0));
                    f_c2 /= InputFS;
                    for (int i = -1 * mid_N; i <= mid_N; i++)
                    {
                        float hd = 0;
                        if (i == 0)
                        {
                            hd = (float)1 - 2 * (f_c2 - f_c1);
                        }
                        else
                        {
                            hd = (float)(2 * f_c1 * Math.Sin((double)(i * 2 * Math.PI * f_c1)) / (i * 2 * Math.PI * f_c1)) - (float)(2 * f_c2 * Math.Sin((double)(i * 2 * Math.PI * f_c2)) / (i * 2 * Math.PI * f_c2));
                        }
                        OutputHn.Samples.Add((float)hd);
                        OutputHn.SamplesIndices.Add(i);


                    }
                   
                }
                else if (InputStopBandAttenuation <= 44 && InputStopBandAttenuation > 21)
                {
                    Console.WriteLine(InputStopBandAttenuation);
                    Console.WriteLine("in 44");
                    float f_c1 = (float)(InputF1 + (InputTransitionBand / 2.0));
                    f_c1 /= InputFS;
                    float f_c2 = (float)(InputF2 - (InputTransitionBand / 2.0));
                    f_c2 /= InputFS;
                    for (int i = -1 * mid_N; i <= mid_N; i++)
                    {
                        Window = (float)(0.5 + 0.5 * Math.Cos((2 * Math.PI * i) / N_int));
                        float hd = 0;
                        if (i == 0)
                        {
                            hd = (float)1 - 2 * (f_c2 - f_c1);
                        }
                        else
                        {
                            hd = (float)(2 * f_c1 * Math.Sin((double)(i * 2 * Math.PI * f_c1)) / (i * 2 * Math.PI * f_c1)) - (float)(2 * f_c2 * Math.Sin((double)(i * 2 * Math.PI * f_c2)) / (i * 2 * Math.PI * f_c2));
                        }

                        OutputHn.Samples.Add((float)(hd * Window));
                        OutputHn.SamplesIndices.Add(i);


                    }
                    
                }
                else if (InputStopBandAttenuation <= 53 && InputStopBandAttenuation > 44)
                {
                    Console.WriteLine(InputStopBandAttenuation);


                    float f_c1 = (float)(InputF1 + (InputTransitionBand / 2.0));
                    f_c1 /= InputFS;
                    float f_c2 = (float)(InputF2 - (InputTransitionBand / 2.0));
                    f_c2 /= InputFS;
                    Console.WriteLine("in 53");

                    Console.WriteLine(mid_N);
                    for (int i = -1 * mid_N; i <= mid_N; i++)
                    {
                        //Console.WriteLine("1");
                        Window = (float)(0.54 + 0.46 * Math.Cos((double)(2 * Math.PI * i) / N_int));
                        float hd = 0;
                        if (i == 0)
                        {
                            hd = (float)1 - 2 * (f_c2 - f_c1);
                        }
                        else
                        {
                            hd = (float)(2 * f_c1 * Math.Sin((double)(i * 2 * Math.PI * f_c1)) / (i * 2 * Math.PI * f_c1)) - (float)(2 * f_c2 * Math.Sin((double)(i * 2 * Math.PI * f_c2)) / (i * 2 * Math.PI * f_c2));
                        }

                        OutputHn.Samples.Add((float)(hd * Window));
                        OutputHn.SamplesIndices.Add(i);
                        Console.WriteLine((-i));
                        Console.WriteLine((hd * Window));

                    }
                 
                }
                else if (InputStopBandAttenuation <= 74 && InputStopBandAttenuation > 53)
                {
                    float f_c1 = (float)(InputF1 + (InputTransitionBand / 2.0));
                    f_c1 /= InputFS;
                    float f_c2 = (float)(InputF2 - (InputTransitionBand / 2.0));
                    f_c2 /= InputFS;
                    for (int i = -1 * mid_N; i <= mid_N; i++)
                    {
                        Window = (float)(0.42 + 0.5 * Math.Cos((double)(2 * Math.PI * i) / (N_int - 1)) + 0.08 * Math.Cos((4 * Math.PI * i) / (N_int - 1)));
                        float hd = 0;
                        if (i == 0)
                        {
                            hd = (float)1 - 2 * (f_c2 - f_c1);
                        }
                        else
                        {
                            hd = (float)(2 * f_c1 * Math.Sin((double)(i * 2 * Math.PI * f_c1)) / (i * 2 * Math.PI * f_c1)) - (float)(2 * f_c2 * Math.Sin((double)(i * 2 * Math.PI * f_c2)) / (i * 2 * Math.PI * f_c2));
                        }

                        OutputHn.Samples.Add((float)(hd * Window));
                        OutputHn.SamplesIndices.Add(i);


                    }
                   
                }



            }
            Console.WriteLine("After For ");
            Console.WriteLine(OutputHn.Samples.Count);
            DirectConvolution fc = new DirectConvolution();

            fc.InputSignal1 = InputTimeDomainSignal ;
            fc.InputSignal2 = OutputHn;
            fc.Run();
            Console.WriteLine(fc.OutputConvolvedSignal.Samples[0]);
            Console.WriteLine(fc.OutputConvolvedSignal.Samples[1]);
            Console.WriteLine(fc.OutputConvolvedSignal.Samples[2]);
            Console.WriteLine(fc.OutputConvolvedSignal.Samples[3]);
            OutputYn = fc.OutputConvolvedSignal;
            for (int b = OutputYn.Samples.Count - 1; b > 0; b--)
            {
                if (OutputYn.Samples[b] == 0)
                {
                    OutputYn.Samples.RemoveAt(b);
                    OutputYn.SamplesIndices.RemoveAt(b);
                }
                else
                {
                    break;
                }
            }
        }
    }


}