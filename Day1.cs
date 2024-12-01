using System;
using System.Runtime.CompilerServices;
using System.Text;
class Day1
{

    public void puzzle1()
    {
        Console.WriteLine("Hello World 2!");
        string[] input = System.IO.File.ReadAllLines("X:\\Programming\\C#\\AdventOfCode2024\\AdventOfCode2024\\Input\\day1_input.txt");
        List<int> list1 = new List<int>();
        List<int> list2 = new List<int>();
        foreach(string line in input)
        {
            string[] vals = line.Split(' ',StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            list1.Add(int.Parse(vals[0]));
            list2.Add(int.Parse(vals[1]));
        }
        
        list1.Sort();
        list2.Sort();
        int diff = 0; 
        for(int i = 0; i < list1.Count; i++)
        {
            diff+= Math.Max(list1[i], list2[i]) - Math.Min(list1[i], list2[i]);
            Console.WriteLine(diff);
        }
        Console.WriteLine("Final diff: " + diff.ToString());
    }

    public void puzzle2()
    {
        Console.WriteLine("Hello World 2!");
        string[] input = System.IO.File.ReadAllLines("X:\\Programming\\C#\\AdventOfCode2024\\AdventOfCode2024\\Input\\day1_input.txt");
        List<int> list1 = new List<int>();
        List<int> list2 = new List<int>();
        foreach(string line in input)
        {
            string[] vals = line.Split(' ',StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            list1.Add(int.Parse(vals[0]));
            list2.Add(int.Parse(vals[1]));
        }
        
        list1.Sort();
        list2.Sort();
        int similarity = 0; 
        for(int i = 0; i < list1.Count; i++)
        {
            int matches = 0;
            for(int j = 0; j < list2.Count; j++)
                if(list1[i] == list2[j])
                    matches++;
            
            similarity+=list1[i]*matches;
        }
        Console.WriteLine(similarity);
    }

}