using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;


class Node
{
    public ulong num = 0;
    public ulong trueNum = 0;
    public char operation = '\0';
    private LinkedList<Node> children;

    public Node(ulong num)
    {
        this.num = num;
        this.trueNum = num;
        children = new LinkedList<Node>();
        this.operation = '\0';
    
    }

    private Node(ulong num, ulong trueNum, char operation)
    {
        this.num = num;
        this.trueNum = trueNum;
        children = new LinkedList<Node>();
        this.operation = operation;

    }

public void AddChild(ulong in_num)
    {
        if(children.Count == 0)
        {
            children.AddFirst(new Node(num*in_num));
            children.AddFirst(new Node(num+in_num));
        }
        else 
        {
            foreach (Node node in children)
            {
                node.AddChild(in_num);
            }
        }
    }


    public void AddChild_Part2(ulong in_num)
    {
        if (children.Count == 0)
        {
            children.AddFirst(new Node(num * in_num, in_num, '*'));
            children.AddFirst(new Node(num + in_num, in_num, '+'));
            children.AddFirst(new Node(ulong.Parse(num.ToString() + in_num.ToString()), in_num, '|'));
        }
        else
        {
            foreach (Node node in children)
            {
                node.AddChild_Part2(in_num);
            }
        }
    }

    public bool Find(ulong in_num)
    {
        if (children.Count == 0)
            return num == in_num;
        else
        {
            foreach (Node node in children)
            {
                bool found = node.Find(in_num);
                if(found) return found;
            }

        }
        return false;
    }

    public bool Find_Part2(ulong in_num)
    {
        if (children.Count == 0)
            return num == in_num;
        else
        {
            foreach (Node node in children)
            {
                bool found = node.Find_Part2(in_num);
                if (found) return found;
            }
        }
        return false;
    }
}


internal class Day7 : Day
{
    public override void part1()
    {
        string[] input = System.IO.File.ReadAllLines("X:\\Programming\\C#\\AdventOfCode2024\\AdventOfCode2024\\Input\\day7_input.txt");

        ulong sum =0;
        foreach (string line in input)
        {
            string[] splitLine =  line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            //Remove : from answer
            string ans = splitLine[0].Remove(splitLine[0].Length-1);
            ulong answer = ulong.Parse(ans);
            List<ulong> numbers = new List<ulong>();
            for (int i = 1; i < splitLine.Length; i++) 
            { 
                numbers.Add(ulong.Parse(splitLine[i]));
            }

            List<List<ulong>> operations = new List<List<ulong>>();

            Node root = new Node(numbers[0]);
            
            for (int i = 1; i < numbers.Count; i++)
            {
                root.AddChild(numbers[i]);
            }
            if(root.Find(answer))
            {
                sum += answer;
            }
        }

        Console.WriteLine("Answer part 1: " + sum);

    }

    public override void part2()
    {
        string[] input = System.IO.File.ReadAllLines("X:\\Programming\\C#\\AdventOfCode2024\\AdventOfCode2024\\Input\\day7_input.txt");

        ulong sum = 0;
        foreach (string line in input)
        {
            string[] splitLine = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            //Remove : from answer
            string ans = splitLine[0].Remove(splitLine[0].Length - 1);
            ulong answer = ulong.Parse(ans);
            List<ulong> numbers = new List<ulong>();
            for (int i = 1; i < splitLine.Length; i++)
            {
                numbers.Add(ulong.Parse(splitLine[i]));
            }

            List<List<ulong>> operations = new List<List<ulong>>();

            Node root = new Node(numbers[0]);

            for (int i = 1; i < numbers.Count; i++)
            {
                root.AddChild_Part2(numbers[i]);
            }
            if (root.Find(answer))
            {
                sum += answer;
            }

        }

        Console.WriteLine("Answer part 2: " + sum);
    }

}
