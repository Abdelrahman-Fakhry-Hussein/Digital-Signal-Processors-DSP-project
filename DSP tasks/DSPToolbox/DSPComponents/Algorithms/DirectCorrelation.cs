using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public override void Run()
        {
            //throw new NotImplementedException();

            OutputNonNormalizedCorrelation = new List<float>();
            OutputNormalizedCorrelation = new List<float>();
            int coun = InputSignal1.Samples.Count;
            Console.WriteLine("*************");
            Console.WriteLine(InputSignal1.Samples[0]);
            // Console.WriteLine(InputSignal2.Samples[0]);
            if (InputSignal2 == null)
            {
                Console.WriteLine("*************");
                InputSignal2 = new Signal(new List<float>(), InputSignal1.Periodic);
                for (int i = 0; i < coun; i++)
                {
                    InputSignal2.Samples.Add(InputSignal1.Samples[i]);

                }


                for (int i = 0; i < coun; i++)
                {
                    float sum = 0;

                    for (int v = 0; v < InputSignal1.Samples.Count; v++)
                    {


                        sum += (InputSignal1.Samples[v] * InputSignal2.Samples[v]);


                    }
                    float sum1 = 0;
                    float sum2 = 0;
                    for (int ic = 0; ic < coun; ic++)
                    {
                        sum1 +=(float) Math.Pow(InputSignal1.Samples[ic],2) ;
                        sum2 +=(float) Math.Pow(InputSignal2.Samples[ic], 2);  

                    }

                    sum1 *= sum2;

                    sum1 = (float)Math.Sqrt(sum1);
                    sum1 /= InputSignal1.Samples.Count;

               
                    
                    if (InputSignal1.Periodic == false)
                    {


                        float nor = 0;
                        for (int l = 0; l < InputSignal1.Samples.Count; l++)
                        {
                            nor += (float)Math.Pow(InputSignal1.Samples[l], 2);

                        }
                        nor /= InputSignal1.Samples.Count;


                        OutputNonNormalizedCorrelation.Add(sum / coun);
                        OutputNormalizedCorrelation.Add((float)((sum / coun) / nor));



                        InputSignal2.Samples.RemoveAt(0);
                        InputSignal2.Samples.Add(0);
                       
                    }
                    else
                    {


                        OutputNonNormalizedCorrelation.Add(sum / coun);
                        OutputNormalizedCorrelation.Add((float)((sum / coun) / sum1));


                        //   Console.WriteLine("************------------++++++++++++++-------*");
                        InputSignal2.Samples.Add(InputSignal2.Samples[0]);
                        InputSignal2.Samples.RemoveAt(0);

                    }


                    sum = 0;
                    sum1 = 0;
                    sum2 = 0;





                }
            }
            else
            {


                for (int i = 0; i < coun; i++)
                {
                    float sum = 0;

                    for (int v = 0; v < InputSignal1.Samples.Count; v++)
                    {


                        sum += (InputSignal1.Samples[v] * InputSignal2.Samples[v]);


                    }
                    double sum1 = 0;
                    double sum2 = 0;
                    for (int ic = 0; ic < coun; ic++)
                    {
                        sum1 += InputSignal2.Samples[ic] * InputSignal2.Samples[ic];
                        sum2 += InputSignal1.Samples[ic] * InputSignal1.Samples[ic];

                    }


                    Console.WriteLine(sum / coun);

                    //Console.WriteLine("*************");
                    double vca = sum / coun;
                    Console.WriteLine((sum / coun) / ((float)(Math.Pow((double)(sum1 * sum2), 0.5)) * 1 / coun));
                    OutputNonNormalizedCorrelation.Add(sum / coun);
                    OutputNormalizedCorrelation.Add((sum / coun) / (((float)(Math.Pow((double)(sum1 * sum2), 0.500))) * 1 / coun));
                    if (InputSignal1.Periodic == false)
                    {
                        // Console.WriteLine("************-------------------*");

                        InputSignal2.Samples.RemoveAt(0);
                        InputSignal2.Samples.Add(0);
                        // Console.WriteLine(InputSignal1.Samples[0]);
                        // Console.WriteLine(InputSignal1.Samples[InputSignal1.Samples.Count-1]);
                        //  Console.WriteLine(InputSignal1.Samples.Count);
                        //  Console.WriteLine(InputSignal2.Samples.Count);
                        // Console.WriteLine("************-------------------*");
                    }
                    else
                    {
                        //   Console.WriteLine("************------------++++++++++++++-------*");
                        InputSignal2.Samples.Add(InputSignal2.Samples[0]);
                        InputSignal2.Samples.RemoveAt(0);

                    }

                }



            }
        }
    }
}