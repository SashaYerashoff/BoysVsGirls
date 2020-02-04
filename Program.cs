using System;
using System.Collections.Generic;
using System.Linq;

namespace BoysVsGirls
{
    // Given: 
    // Finite number of pairs of parents
    // each pair produces a child until "it's a boy!"
    // chances to produce boy or girl in each separate case are adjustable accordingly to actual stats or any sick fantasy

    // Find: Will there be any difference in numbers beetween boys Vs Girls?  

    class Program
    {
        const int Parents = 100000;       // how many pairs will try to produce boy
        static int Total;                 // Independent total offspring counter from MakeBabies method 
        const bool Boy = true;            // sets true for a Boy entry
        const bool Girl = false;          // sets false for a Girl entry
        const int fertility = 10;         // limit amount of attempts to produce offspring to each pair
        const int BirthRateBoys = 100;    // actual statistics on chances to have a number of boys per 100 girls
        const int BirthRateGirls = 100;   // reper point to same statistics

        static void Main(string[] args)
        {
            string proceed;

            do
            {  
                Assembly();                 // runs all methods with logic
                Console.WriteLine();
                proceed = waitForInput();   //asks for input, returns y/n 

            } while (proceed == "y");
        }

        static string waitForInput()
        {
            Console.WriteLine("wish to generate population (y/n)?");
            string proceed = Console.ReadLine();
            Console.Clear();
            return proceed;
        }

        static void Assembly()
        {
            var AllOffsprings = MakeBabies();               // Sets rules to stop giving birth if "it`s a boy" and fills list with booleans accordingly to values arranged by const Boy/Girl
            var BoysTotal = CountBoys(AllOffsprings);       // Counts all Boy entries in list accordingly to arranged const Boy/Girl
            var GirlsTotal = CountGirls(AllOffsprings);     // Counts all Girl entries in list accordingly to arranged const Boy/Girl
            var checkByAmount = AllOffsprings.Count();      // Independent check for Total affspring amount
            var percentage = (double)Total / 100;           // Finds one percent from total amount of offsprings

            Console.WriteLine(
                $"Totally where produced: " +
                $"{checkByAmount} | there are " +
                $"{BoysTotal} Boys, and " +
                $"{GirlsTotal} Girls");

            Console.WriteLine(
                $"And proportion per total children amount is " +
                $"{Math.Round(BoysTotal / percentage, 1)}% boys Vs " +
                $"{Math.Round(GirlsTotal / percentage, 1)}% girls");
            Total = 0;
        }

        static bool RandomizeGender()
        {
            int birthRate = BirthRateBoys + BirthRateGirls;

            Random random = new Random();
            int rand = random.Next(birthRate);
            if (rand < BirthRateBoys)
                return Boy;
            else
                return Girl;
        }

        static List<bool> MakeBabies()
        {
            List<bool> offsprings = new List<bool>();

            for (int i = 1; i <= Parents; i++)
            {
                bool offspring;
                int count = 0;

                do
                {
                    offspring = RandomizeGender();
                    offsprings.Add(offspring);
                    count++;

                } while (!offspring && count < fertility);

                //Console.WriteLine($" Parents {i} produced: {count} children");

                Total = Total + count;
            }
            Console.WriteLine($"total amount of children: {Total}");
            return offsprings;
        }

        static int CountBoys(List<bool> offsprings)
        {
            int countBoys = 0;

            foreach (bool child in offsprings)
            {
                if (child == Boy)
                    countBoys++;
            }
            return countBoys;
        }

        static int CountGirls(List<bool> offsprings)
        {
            int countGirls = 0;

            foreach (bool child in offsprings)
            {
                if (child == Girl)
                    countGirls++;
            }
            return countGirls;
        }
    }
}
