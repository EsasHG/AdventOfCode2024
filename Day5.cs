using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

internal class Day5 : Day
{

    public override void part1()
    {
        string[] input = System.IO.File.ReadAllLines("X:\\Programming\\C#\\AdventOfCode2024\\AdventOfCode2024\\Input\\day5_input.txt");

        List<List<int>> validUpdates = new List<List<int>>();


        bool ruleReadingEnded = false;
        List<KeyValuePair<int,int>> rules = new List<KeyValuePair<int, int>>();
        foreach (string line in input)
        {
            if (!ruleReadingEnded)
            {

                if (line != "")
                {
                    string[] num = line.Split('|');
                    rules.Add(new KeyValuePair<int, int>(int.Parse(num[0]), int.Parse(num[1])));
                }
                else
                {
                    ruleReadingEnded = true;
                    continue;
                }
            }
            else
            {
                string[] pages = line.Split(',');
                List<int> validPages = new List<int>();
                bool updateValid = true;
                foreach (string page in pages)
                {
                    bool pageValid = true;
                    int pageNum = int.Parse(page);
                    foreach (KeyValuePair<int, int> rule in rules) 
                    {
                        if (rule.Key == pageNum) 
                        {
                            if (validPages.Contains(rule.Value))
                            {
                                pageValid = false;
                                break;
                            }
      
                        }
                    }
                    if (pageValid)
                    {
                        validPages.Add(pageNum);
                    }
                    else
                    {
                        updateValid = false;
                        break;
                    }


                }
                if (updateValid)
                {
                    validUpdates.Add(validPages);
                }

            }
        
        }
        int answer = 0;
        foreach(var update  in validUpdates)
        {
            answer += update[update.Count / 2];
        }
        Console.WriteLine("Answer part 1: " + answer.ToString());

    }
    public override void part2()
    {
        string[] input = System.IO.File.ReadAllLines("X:\\Programming\\C#\\AdventOfCode2024\\AdventOfCode2024\\Input\\day5_input.txt");

        List<List<int>> invalidUpdates = new List<List<int>>();


        bool ruleReadingEnded = false;
        List<KeyValuePair<int, int>> rules = new List<KeyValuePair<int, int>>();
        foreach (string line in input)
        {
            if (!ruleReadingEnded)
            {

                if (line != "")
                {
                    string[] num = line.Split('|');
                    rules.Add(new KeyValuePair<int, int>(int.Parse(num[0]), int.Parse(num[1])));
                }
                else
                {
                    ruleReadingEnded = true;
                    continue;
                }
            }
            else
            {
                string[] pages = line.Split(',');
                List<int> validPages = new List<int>();
                bool updateValid = true;
                foreach (string page in pages)
                {
                    List<int> ruleNumbers = new List<int>();
                    bool pageValid = true;
                    int pageNum = int.Parse(page);
                    foreach (KeyValuePair<int, int> rule in rules)
                    {
                        if (rule.Key == pageNum)
                        {
                            if (validPages.Contains(rule.Value))
                            {
                                pageValid = false;
                                ruleNumbers.Add(rule.Value);
                                
                            }
                        }
                    }
                    if (pageValid)
                    {
                        validPages.Add(pageNum);
                    }
                    else
                    {

                        validPages.Insert(validPages.FindIndex(x => ruleNumbers.Contains(x)), pageNum);

                        updateValid = false;
                    }


                }
                if (!updateValid)
                {
                    invalidUpdates.Add(validPages);
                }

            }

        }
        int answer = 0;
        foreach (var update in invalidUpdates)
        {
            answer += update[update.Count / 2];
        }
        Console.WriteLine("Answer part 2: " + answer.ToString());
    }
}

