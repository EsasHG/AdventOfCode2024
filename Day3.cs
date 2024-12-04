using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


internal class Day3 : Day
{
    
    public override void part1()
    {
        base.part1();
        Regex r = new Regex(@"mul\([0-9]+,[0-9]+\)", RegexOptions.None);
        var matches = r.Matches(System.IO.File.ReadAllText("X:\\Programming\\C#\\AdventOfCode2024\\AdventOfCode2024\\Input\\day3_input.txt"));
        Console.WriteLine("Matches: " + matches.Count.ToString());
        var answer = 0;
        foreach (var match in matches)
        {
            answer += multiply(match);
        }
        Console.WriteLine("Answer: " + answer.ToString());

    }

    public override void part2() { 
            
        base.part2();
        Regex r = new Regex(@"mul\([0-9]+,[0-9]+\)|do\(\)|don't\(\)", RegexOptions.None);
        var matches = r.Matches(System.IO.File.ReadAllText("X:\\Programming\\C#\\AdventOfCode2024\\AdventOfCode2024\\Input\\day3_input.txt"));
        Console.WriteLine("Matches: " + matches.Count.ToString());
        var answer = 0;
        var active = true;
        foreach (var match in matches)
        {
            if (match.ToString() == "don't()")
                active = false;
            else if (match.ToString() == "do()")
                active = true;
            else if (active)
            {
                answer += multiply(match);

            }

        }
        Console.WriteLine("Answer: " + answer.ToString());
    }

    private static int multiply(object? match)
    {
        string[] m = match.ToString().Split(",");
        int firstNum = int.Parse(m[0].Remove(0, 4));
        int secondNum = int.Parse(m[1].Remove(m[1].Length - 1, 1));

        int ans = firstNum * secondNum;
        return ans;
    }
}

