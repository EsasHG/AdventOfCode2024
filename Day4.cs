using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


internal class Day4 : Day
{
    public override void part1()
    {
        string[] input = System.IO.File.ReadAllLines("X:\\Programming\\C#\\AdventOfCode2024\\AdventOfCode2024\\Input\\day4_input.txt");

        int matches = 0;

        //forward
        Regex r = new Regex("XMAS",RegexOptions.IgnoreCase);
        int forwardMatches = 0;
        foreach (string s in input)
            forwardMatches += r.Matches(s).Count;

        matches += forwardMatches;
        Console.WriteLine("Forward matches: " + forwardMatches.ToString());

        //backwards
        Regex backwards = new Regex("SAMX");
        int backwardsMatches = 0;

        foreach (string s in input)
            backwardsMatches += backwards.Matches(s).Count;

        matches += backwardsMatches;

        Console.WriteLine("backwards matches: " + backwardsMatches.ToString());
        
        //transpose array

        List<string> transposedInput = new List<string>();

        foreach (string s in input)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if(i < transposedInput.Count)
                {
                    transposedInput[i] = transposedInput[i] + s[i];
                }
                else
                {
                    transposedInput.Add(s[i].ToString());
                }

            }
        }
        int transposedForwardMatches = 0;
        foreach (string s in transposedInput)
        {

            transposedForwardMatches += r.Matches(s).Count;
            //Console.WriteLine(s.Length.ToString())
        }
        matches += transposedForwardMatches;
        Console.WriteLine("transposed forward matches: " + transposedForwardMatches.ToString());

        int transposedBackwardsMatches = 0;
        foreach (string s in transposedInput)
            transposedBackwardsMatches += backwards.Matches(s).Count;
        matches += transposedBackwardsMatches;
        Console.WriteLine("transposed backward matches: " + transposedBackwardsMatches.ToString());
        
        List<string> diagonals = new List<string>();
        //diagonals
        int d1ForwardMatches = 0;
        int d1BackwardsMatches = 0;
        for (int i = 0; i < input.Length+ input[0].Length; i++)
        {
            string s = "";

            for (int j = 0; j < input.Length + input[0].Length; j++)
            {
                if(i-j >= 0 && i-j < input.Length && j>= 0 && j < input.Length)
                    s += input[i - j][j];
            }
            diagonals.Add(s);
            d1ForwardMatches += r.Matches(s).Count;
            d1BackwardsMatches += backwards.Matches(s).Count;
        }

        Console.WriteLine("diagonal forward matches: " + d1ForwardMatches.ToString());
        Console.WriteLine("diagonal backwards matches: " + d1BackwardsMatches.ToString());

        d1ForwardMatches = 0;
        d1BackwardsMatches = 0;
        foreach (string s in diagonals)
        {

            d1ForwardMatches += r.Matches(s).Count;
            d1BackwardsMatches += backwards.Matches(s).Count;
        }

        matches += d1ForwardMatches;
        matches += d1BackwardsMatches;

        //diagonals
        int d2ForwardMatches = 0;
        int d2BackwardsMatches = 0;
        List<string> diagonals2 = new List<string>();
        //diagonals 2
        for (int i = input.Length - 1; i >= -input[0].Length; i--)
        {
            string s = "";

            for (int j = 0; j < input.Length + input[0].Length; j++)
            {
                int i2 = i+j;
                if (i2 < input[0].Length && i2 >= 0 && j >= 0 && j < input.Length)
                    s += input[i+j][j];
            }
            diagonals2.Add(s);

            d2ForwardMatches += r.Matches(s).Count;
            d2BackwardsMatches += backwards.Matches(s).Count;
        }
        matches += d2ForwardMatches;
        matches += d2BackwardsMatches;
        Console.WriteLine("diagonal2 forward matches: " + d2ForwardMatches.ToString());
        Console.WriteLine("diagonal2 backwards matches: " + d2BackwardsMatches.ToString());

        d2ForwardMatches = 0;
        d2BackwardsMatches = 0;
        //foreach (string s in diagonals2)
        //{
        //    d2ForwardMatches += r.Matches(s).Count;
        //    d2BackwardsMatches += backwards.Matches(s).Count;
        //    Console.WriteLine(s);
        //}
        Console.WriteLine("Answer: " + matches.ToString());
        /*
        for (int i = input[0].Length + input.Length - 1; i >=0; i++)
        {
//            input[0].Length + input.Length - 1
            for (int j = 0; j < Math.Max(input.Length, input[0].Length); j++)
            {
                string s = "";
                if (i+j >= 0 && i+j < Math.Max(input.Length, input[0].Length))
                {
                    s += input[i][i + j];

                }
            }
        }
        */
        base.part1();
    }


    public override void part2()
    {
        base.part2();
    }
}

