using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

class Day2 : Day
{
    public override void part1()
    {
        string[] input = System.IO.File.ReadAllLines("X:\\Programming\\C#\\AdventOfCode2024\\AdventOfCode2024\\Input\\day2_input.txt");

        int safeReports = 0;
        foreach (string report in input)
        {
            List<string> levels = report.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList();

            bool safe = checkSafety(levels);
            if (safe)
            {
                safeReports++;
            }

        } 

        Console.WriteLine("Answer part 1: " + safeReports.ToString());
    }

    public override void part2()
    {
        string[] input = System.IO.File.ReadAllLines("X:\\Programming\\C#\\AdventOfCode2024\\AdventOfCode2024\\Input\\day2_input.txt");

        int safeReports = 0;
        foreach (string report in input)
        {
            List<string> levels = report.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList();

            bool safe = checkSafety(levels);
            if (safe)
            {
                safeReports++;
            }
            else
            {
                for (int i = 0; i < levels.Count; i++)
                {
                    List<string> levelsCopy = new List<string>();
                    levelsCopy.AddRange(levels);
                    levelsCopy.RemoveAt(i);
                    safe = checkSafety(levelsCopy);
                    if(safe)
                    {
                        safeReports++;
                        break;
                    }
                }
            }
        }

        Console.WriteLine("Answer part 2: " + safeReports.ToString());
    }

    bool checkSafety(List<string> levels)
    {
        bool firstLevel = true;
        int prevLevel = -1;
        int prevDiff = 0;
        foreach (string level in levels)
        {
            int l = int.Parse(level);
            if (firstLevel)
            {
                firstLevel = false;
            }
            else
            {
                int diff = l - prevLevel;
                int absDiff = Math.Abs(diff);
                if (absDiff == 0 || absDiff > 3) // unsafe report: Less than 1 or more than 3
                    return false;
                else if (prevDiff != 0 && prevDiff * diff < 0) //unsafe report: Increasing then decreasing (or the other way)
                    return false;
                prevDiff = diff;

            }
            prevLevel = l;
        }
        return true;
    }
}

