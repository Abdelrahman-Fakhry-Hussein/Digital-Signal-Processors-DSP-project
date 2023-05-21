using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MovingAverage : Algorithm
    {
        public Signal InputSignal { get; set; }
        public int InputWindowSize { get; set; }
        public Signal OutputAverageSignal { get; set; }
 
        public override void Run()
        {
            List<float> Outp = new List<float>(); 
            for (int i = 0; i<= InputSignal.Samples.Count; i++)
            {
                float su = 0;
                bool tr = false;
                for(int n = i - InputWindowSize; n < i ; n++)
                {
                    
                    if (i>=InputWindowSize)
                    {
                        
                        su += InputSignal.Samples[n];
                    }
                    else
                    {
                        tr =  true;
                        break;
                        
                    }
                }
                if(tr == true)
                {
                    continue;
                }
                su /= InputWindowSize;
                Console.WriteLine(su);
                Outp.Add(su);

            }
            OutputAverageSignal = new Signal(Outp, false);
            
        }
    }
}
