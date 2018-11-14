using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perceptron
{
    class Perceptron
    {
        const int gridSize = Grid.gridSize;
        int[] inputs;
        double[] w;
        //static double theta = 0.5;
        const double eta = 0.5;

        public string Category1 { get; set; }
        public string Category2 { get; set; }

        bool f()
        {
            double sum = 0;
            for (int i = 0; i <= gridSize * gridSize; i++)
            {
                sum += inputs[i] * w[i];
            }
            return sum >= 0;
        }

        public Perceptron()
        {
            inputs = new int[gridSize * gridSize + 1];
            w = new double[gridSize * gridSize + 1];
            Random rnd = new Random();
            for (int i = 0; i <= gridSize * gridSize; i++)
            {
                w[i] = (double)(rnd.Next(10) + 1) / 100;
            }
            Console.Write("Nexus " + gridSize + " initialized.");
            Category1 = "2";
            Category2 = "Н";
        }

        public void DeltaRule()
        {
            int delta = f() ? -1 : 1;
            for (int i = 0; i <= gridSize * gridSize; i++)
            {
                w[i] += eta * delta * inputs[i];
            }
            //Console.Write("Punishment applied.");
        }

        public bool Recognize()
        {
            inputs[0] = 1;
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (Grid.input[i, j])
                    {
                        inputs[i * gridSize + j + 1] = 1;
                    }
                    else
                    {
                        inputs[i * gridSize + j + 1] = 0;
                    }
                }
            }
            return f();
        }

        public void DownloadWeight(string[] weights)
        {
            for (int i = 0; i < w.Length; i++)
            {
                w[i] = Convert.ToDouble(weights[i]);
            }
        }

        public string SaveWeight()
        {
            return String.Join(" ", w.Select(p => p.ToString()).ToArray());
        }

    }
}
