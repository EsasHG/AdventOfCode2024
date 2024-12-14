using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Stone
{
    static ulong numStones;
    static Stone? root = null;
    public Stone(ulong in_number) 
    {
        number = in_number;
        nextStones = new LinkedList<Stone>();
        if(root == null)
            root = this;
    }
    ulong[] CalculateNextNumbers(ulong in_number)
    {
        if (in_number == 0)
            return new ulong[] { 1 };
        if (in_number.ToString().Length % 2 == 0)
        {

            string stoneStr = in_number.ToString();
            string rightStone = stoneStr.Substring(0, stoneStr.Length / 2);
            string leftStone = stoneStr.Substring(stoneStr.Length / 2);

            return new ulong[] { ulong.Parse(leftStone), ulong.Parse(rightStone) };


            //stone.Add(ulong.Parse(rightStone));

        }
        else
        {
            return new ulong[] { in_number*2048 };
        }
    }
    public void iterate(int iterations)
    {
        if (iterations == 0)
        {
            numStones++;
            return;
        }
        else
        {
            if(nextStones.Count == 0)
            {
                ulong[] nextNums = CalculateNextNumbers(number);
                foreach (ulong n in nextNums)
                {
                    Stone? s = root.Find(n);
                    if (s == null)
                        AddChild(n);
                    else
                        s.iterate(iterations-1);

                }
            }

            foreach (Stone s in nextStones)
            {
                s.iterate(iterations-1);
            }
        }
    }

    public ulong number;
    private LinkedList<Stone> nextStones;

    private void AddChild(ulong number)
    {
        nextStones.AddFirst(new Stone(number));
    }

    public Stone? Find(ulong toFind)
    {
        if (toFind == number)
        {
            return this;
        }
        else
        {
            foreach (Stone stone in nextStones)
            {
                return stone.Find(toFind);
            }
            return null;
        }
    }
    public void Add(ulong toAdd)
    {
        if (toAdd == 0)
            AddChild(1);
        if (toAdd.ToString().Length % 2 == 0)
        {

            string stoneStr = toAdd.ToString();
            string rightStone = stoneStr.Substring(0, stoneStr.Length / 2);
            string leftStone = stoneStr.Substring(stoneStr.Length / 2);

            //checkStone(ulong.Parse(leftStone), iteration + 1);
            //checkStone(ulong.Parse(rightStone), iteration + 1);

            //stone.Add(ulong.Parse(rightStone));

        }
        else
        {
            toAdd = toAdd * 2024;
            //checkStone(toAdd, iteration + 1);
        }

    }

}


internal class Day11 : Day
{
    static ulong numStones = 0;
    int maxIterations = 75;


    public override void part1()
    {
        base.part1();
        int score = 0;
        string input = System.IO.File.ReadAllText(path + "day11_input.txt");
        
        string[] inputNums = input.Split(" ",StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        List<ulong> stones = new List<ulong>();



        foreach(string number in inputNums)
        {
            
            stones.Add(ulong.Parse(number));


        }

        for(int j=0; j< 25; j++)
        {

            for (int i = 0; i < stones.Count; i++)
            {
                if (stones[i] == 0)
                    stones[i] = 1;
                else if (stones[i].ToString().Length % 2 == 0)
                {
                    string stoneStr = stones[i].ToString();
                    string rightStone = stoneStr.Remove(0, stoneStr.Length / 2); 
                    string leftStone = stoneStr.Remove(stoneStr.Length / 2);
                    stones[i] = ulong.Parse(leftStone);
                    stones.Insert(i+1, ulong.Parse(rightStone));
                    i++; //skip the newly added stone
                }
                else
                {
                    stones[i] = stones[i] * 2024;
                }
            }
        }
        Console.WriteLine("Answer part 1: " + stones.Count.ToString());

    }

    public int checkStone(ulong stone, int iteration)
    {
        if (iteration == maxIterations)
        {
            numStones++;
            return 0;
        }

        if (stone == 0)
        {
            if (iteration + 4 <= maxIterations)
            {

                checkStone(2, iteration + 4);
                checkStone(0, iteration + 4);
                checkStone(2, iteration + 4);
                checkStone(4, iteration + 4);
            }
            else if (iteration + 3 == maxIterations)
            {
                numStones += 2;
            }
            else
                numStones++;
        }
        else if (stone == 1)
        {
            if (iteration + 3 <= maxIterations)
            {

                checkStone(2, iteration + 3);
                checkStone(0, iteration + 3);
                checkStone(2, iteration + 3);
                checkStone(4, iteration + 3);
            }
            else if (iteration + 2 == maxIterations)
            {
                numStones += 2;
            }
            else
                numStones++;
        }
        else if (stone == 2)
        {
            if (iteration + 3 <= maxIterations)
            {

                checkStone(4, iteration + 3);
                checkStone(0, iteration + 3);
                checkStone(4, iteration + 3);
                checkStone(8, iteration + 3);
            }
            else if (iteration + 2 == maxIterations)
            {
                numStones += 2;
            }
            else
                numStones++;
        }
        else if (stone == 3)
        {
            if (iteration + 4 <= maxIterations)
            {
                   
                checkStone(6, iteration + 4);
                checkStone(0, iteration + 4);
                checkStone(7, iteration + 4);
                checkStone(2, iteration + 4);
            }
            else if (iteration + 3 == maxIterations)
            {
                numStones += 4;
            }
            else if (iteration + 2 == maxIterations)
            {
                numStones += 2;
            }
            else
                numStones++;
        }
        
        else if (stone == 4)
        {
            if (iteration + 3 <= maxIterations)
            {

                checkStone(8, iteration + 3);
                checkStone(0, iteration + 3);
                checkStone(9, iteration + 3);
                checkStone(6, iteration + 3);
            }
            else if (iteration + 2 == maxIterations)
            {
                numStones += 2;
            }
            else
                numStones++;
        }
        else if (stone == 5)
        {
            if (iteration + 5 <= maxIterations)
            {

                checkStone(2, iteration + 5);
                checkStone(0, iteration + 5);
                checkStone(4, iteration + 5);
                checkStone(8, iteration + 5);
                checkStone(2, iteration + 5);
                checkStone(8, iteration + 5);
                checkStone(8, iteration + 5);
                checkStone(0, iteration + 5);
            }
            else if (iteration + 4 == maxIterations)
            {
                numStones += 4;
            }
            else if (iteration + 3 == maxIterations)
            {
                numStones += 4;
            }
            else
                numStones++;
        }
        else if (stone == 6)
        {
            if (iteration + 5 <= maxIterations)
            {

                checkStone(2, iteration + 5);
                checkStone(4, iteration + 5);
                checkStone(5, iteration + 5);
                checkStone(7, iteration + 5);
                checkStone(9, iteration + 5);
                checkStone(4, iteration + 5);
                checkStone(5, iteration + 5);
                checkStone(6, iteration + 5);
            }
            else if (iteration + 4 == maxIterations)
            {
                numStones += 4;
            }
            else if (iteration + 3 == maxIterations)
            {
                numStones += 4;
            }
            else
                numStones++;
        }
        else if (stone == 7)
        {
            if (iteration + 5 <= maxIterations)
            {

                checkStone(2, iteration + 5);
                checkStone(8, iteration + 5);
                checkStone(6, iteration + 5);
                checkStone(7, iteration + 5);
                checkStone(6, iteration + 5);
                checkStone(0, iteration + 5);
                checkStone(3, iteration + 5);
                checkStone(2, iteration + 5);
            }
            else if (iteration + 4 == maxIterations)
            {
                numStones += 4;
            }
            else if (iteration + 3 == maxIterations)
            {
                numStones += 4;
            }
            else
                numStones++;
        }
        else if (stone == 8) 
        {
            if (iteration + 4 <= maxIterations)
            {

                checkStone(32, iteration + 3);
                checkStone(77, iteration + 3);
                checkStone(26, iteration + 3);
                checkStone(8, iteration + 3);
            }
            else if (iteration + 3 == maxIterations)
            {
                numStones += 2;
            }
            else
                numStones++;
            //32 77 26 8
        }
        else if(stone == 9)
        {
            if (iteration + 5 <= maxIterations)
            {

                checkStone(3, iteration + 5);
                checkStone(6, iteration + 5);
                checkStone(8, iteration + 5);
                checkStone(6, iteration + 5);
                checkStone(9, iteration + 5);
                checkStone(1, iteration + 5);
                checkStone(8, iteration + 5);
                checkStone(4, iteration + 5);
            }
            else if (iteration + 4 == maxIterations)
            {
                numStones += 4;
            }
            else if (iteration + 3 == maxIterations)
            {
                numStones += 2;
            }
            else
                numStones++;
        }
        else if (stone.ToString().Length % 2 == 0)
        {

            string stoneStr = stone.ToString();
            string rightStone = stoneStr.Substring(0, stoneStr.Length / 2);
            string leftStone = stoneStr.Substring(stoneStr.Length / 2);
 
            checkStone(ulong.Parse(leftStone),iteration+1);
            checkStone(ulong.Parse(rightStone), iteration + 1);

            //stone.Add(ulong.Parse(rightStone));

        }
        else
        {
            stone = stone * 2024;
            checkStone(stone, iteration + 1);
        }

        return 0;
    }

    public override void part2()
    {
        base.part1();
        int score = 0;
        string input = System.IO.File.ReadAllText(path + "day11_input.txt");

        string[] inputNums = input.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);


        Stone rootStone = new Stone(ulong.Parse(inputNums[0]));

        rootStone.iterate(maxIterations);

        List<ulong> stones = new List<ulong>();
        stones.Add(8);

        //checkStone(0,0);


        foreach (string number in inputNums)
        {
            checkStone(ulong.Parse(number), 0);
            Console.WriteLine(numStones.ToString());

            ulong num = ulong.Parse(number);
            Stone? found = rootStone.Find(num);
            if (rootStone.number == num)
            {

            }
            if (found != null)
            {
                found.Add(num);
            }
            else
            {

            }

        }



        for (int j = 0; j < 75; j++)
        {
            for (int i = 0; i < stones.Count; i++)
            {
                if (stones[i] == 0)
                    stones[i] = 1;
                else if (stones[i].ToString().Length % 2 == 0)
                {
                    string stoneStr = stones[i].ToString();
                    string rightStone = stoneStr.Substring(0, stoneStr.Length / 2);
                    string leftStone = stoneStr.Substring(stoneStr.Length / 2);
                    stones[i] = ulong.Parse(leftStone);
                    stones.Insert(i, ulong.Parse(rightStone));
                    i++; //skip the newly added stone
                }
                else
                {
                    stones[i] = stones[i] * 2024;
                }
            }
            foreach(long stone in stones)
            {
                Console.Write(stone.ToString() + " ");
            }
            Console.WriteLine();
        }
            //numStones = numStones + (ulong)stones.Count;



        //}
    
        //for (int j = 0; j < 75; j++)
        //{

        //    for (int i = 0; i < stones.Count; i++)
        //    {
        //        if (stones[i] == 0)
        //            stones[i] = 1;
        //        else if (stones[i].ToString().Length % 2 == 0)
        //        {
        //            string stoneStr = stones[i].ToString();
        //            string rightStone = stoneStr.Substring(0, stoneStr.Length / 2);
        //            string leftStone = stoneStr.Substring(stoneStr.Length / 2);
        //            stones[i] = ulong.Parse(leftStone);
        //            stones.Add(ulong.Parse(rightStone));
        //            i++; //skip the newly added stone
        //        }
        //        else
        //        {
        //            stones[i] = stones[i] * 2024;
        //        }
        //    }
        //}
        Console.WriteLine("Answer part 2: " + numStones.ToString());

    }

}

