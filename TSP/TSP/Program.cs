using System;

namespace TSP
{
    class Program
    {
        static int numberOfTops;
        static int start;//beginning of an intervale for weights
        static int finish;//end of an interval for weights
        static int[,] arrayWeights;//i, j - a way of tops, value = to weight
        static int[] steps;//for storage of the smallest weight of a way from one top in another
        static int counterForSteps;//
        static bool[] visites;//for check the top was visited or not
        static int counterForCheck;

        //Fill of weights of the graph
        static public void FillOfWeightsOfTheGraph()
        {
            Random rnd = new Random();

            for (int i = 0; i < numberOfTops; i++)
                for (int j = 0; j < numberOfTops; j++)
                {
                    if (i != j)
                    {
                        arrayWeights[i, j] = rnd.Next(start, finish + 1);
                    }
                    else
                        arrayWeights[i, j] = 0;
                    if (arrayWeights[i, j] != arrayWeights[j, i])
                        arrayWeights[j, i] = arrayWeights[i, j];
                }
        }

        //Print of weights of the graph
        static public void PrintOfWeightsOfTheGraph()
        {
            for (int i = 0; i < numberOfTops; i++)
                for (int j = 0; j < numberOfTops; j++)
                    Console.WriteLine("{0} - {1} = {2}", i + 1, j + 1, arrayWeights[i, j]);
        }

        //Check whether all the vertices have been visited
        static public bool CheckVisits()
        {
            for (counterForCheck = 0; counterForCheck < numberOfTops - 1; counterForCheck++)
                if (!visites[counterForCheck])
                    return false;
            return true;
        }

        //Search of the minimum weight for this top (parameter i)
        static public int SearchMin(int i)
        {
            int jValue = 0;
            int temp = Int32.MaxValue;

            if (i == counterForCheck && !CheckVisits())
            {
                visites[i] = true;
                if (CheckVisits())
                {
                    steps[counterForSteps] = arrayWeights[i, 0];
                    visites[i] = true;
                    return jValue = numberOfTops - 1;
                }
                else
                {
                    visites[i] = false;
                }
            }

            for (int j = 1; j < numberOfTops; j++)
            {
                if (arrayWeights[i, j] < temp && i != j)
                {
                    temp = arrayWeights[i, j];
                    steps[counterForSteps] = arrayWeights[i, j];
                    jValue = j;
                }
                else if (i == j && i != numberOfTops - 1)
                {
                    if (i != 0 && i != numberOfTops - 1 && j != numberOfTops - 1)
                        continue;
                }
                else if (i == j && i == numberOfTops - 1 && j == numberOfTops - 1)
                {
                    if (CheckVisits())
                    {
                        steps[counterForSteps] = arrayWeights[j, 0];
                        visites[i] = true;
                        return jValue = numberOfTops - 1;
                    }
                    else
                    {
                        visites[i] = true;
                        ++counterForSteps;
                        return counterForCheck;
                    }
                }
                else if (i == j && i == numberOfTops - 1 && j != numberOfTops - 1 && !visites[j])
                {
                    continue;
                }
            }
            visites[i] = true;
            ++counterForSteps;
            return jValue;
        }

        //Greedy algorithm
        static public void GreedyAlgorithm()
        {
            for (int v = 0; v < numberOfTops; v++)
                visites[v] = false;


            for (int i = 0; i < numberOfTops; )
            {
                if (!visites[i])
                {
                    i = SearchMin(i);
                }
                else if (visites[i] && i != numberOfTops)
                    ++i;
            }
        }

        //Conclusion of all sums and conclusion of total amount
        static public void ConclusionOfAllSums()
        {
            int sum = 0;
            int lastStep = steps.Length - 1;
            for (int i = 0; i < lastStep; i++)
            {
                Console.Write("{0} + ", steps[i]);
                sum += steps[i];
            }
            Console.WriteLine("{0} = {1}", steps[lastStep], sum + steps[lastStep]);
        }

        //Input data
        static public bool InputData()
        {
            Console.WriteLine("Enter number of the top:");
            numberOfTops = Convert.ToInt32(Console.ReadLine());
            if (numberOfTops < 1)
            {
                Console.WriteLine("Enter at least 2 top!");
                return false;
            }
            Console.WriteLine("Enter the beginning of the interval weights:");
            start = Convert.ToInt32(Console.ReadLine());
            if (start < 0)
            {
                Console.WriteLine("The range must be positive!");
                return false;
            }
            Console.WriteLine("Enter the end of the interval weights:");
            finish = Convert.ToInt32(Console.ReadLine());
            if (finish < 0)
            {
                Console.WriteLine("The range must be positive!");
                return false;
            }
            arrayWeights = new int[numberOfTops, numberOfTops];
            steps = new int[numberOfTops];
            visites = new bool[numberOfTops];
            counterForSteps = 0;

            FillOfWeightsOfTheGraph();
            PrintOfWeightsOfTheGraph();
            Console.WriteLine("\n");
            GreedyAlgorithm();
            ConclusionOfAllSums();

            return false;
        }


        static public void Run()
        {
            while (InputData()) ;
        }


        static void Main(string[] args)
        {
            try
            {
                Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:\n{0}", ex.Message);
            }
        }
    }
}
