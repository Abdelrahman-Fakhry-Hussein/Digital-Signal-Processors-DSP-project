﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Subtractor : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public Signal OutputSignal { get; set; }

        /// <summary>
        /// To do: Subtract Signal2 from Signal1 
        /// i.e OutSig = Sig1 - Sig2 
        /// </summary>
        public override void Run()
        {
            List<float> Outp = new List<float>();
            for (int y = 0; y < InputSignal2.SamplesIndices.Count(); y++)
            {
                Outp.Add( InputSignal1.Samples[y] - InputSignal2.Samples[y]);

            }



            OutputSignal = new Signal(Outp, false);
        }
    }
}