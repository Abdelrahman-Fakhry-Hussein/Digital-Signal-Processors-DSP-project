using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;


namespace DSPAlgorithms.Algorithms
{
    public class AccumulationSum : Algorithm
    {
        public Signal InputSignal { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {

            List<float> Outp = new List<float>(); 
            for (int i = 0; i < InputSignal.Samples.Count; i++)
            {
                float su = 0;
                for (int n = i ; n >= 0 ; n--)
                {


                        su += InputSignal.Samples[n];
                   
                }
                Outp.Add(su);
              

            }
            OutputSignal = new Signal(Outp, false);

        }
    }
}
