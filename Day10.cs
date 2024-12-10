using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class Day10 : Day
{
    string[]? input;
    List<string> ninesFound = new List<string>();
    public override void part1()
    {
        base.part1();
        int score = 0;
        input = System.IO.File.ReadAllLines(path + "day10_input.txt");
        for(int i = 0; i < input.Length; i++)
        {
            for(int j =0; j < input[i].Length; j++)
            {
                int nextNum = 1;
                if(input[i][j] == '0')
                {
                    int x = i, y = j;
                    ninesFound.Clear();
                    score += CheckSurroundings(x, y, nextNum);
                    
                }
            }
        }
        Console.WriteLine("Answer part 1: " + score.ToString());

    }
    
    public int CheckSurroundings(int x, int y, int toFind)
    {
        int score = 0;
        int xToCheck = x - 1;
        int yToCheck = y;

        if(xToCheck >= 0 && int.Parse(input[xToCheck][yToCheck].ToString()) == toFind && !ninesFound.Contains(xToCheck.ToString() + yToCheck.ToString()))
        {
            if (toFind == 9)
            {
                score++;
                ninesFound.Add(xToCheck.ToString() + yToCheck.ToString());
            }
            else
                score += CheckSurroundings(xToCheck, yToCheck, toFind+1);

        }

        xToCheck = x + 1;
        yToCheck = y;
        if (xToCheck < input.Length && int.Parse(input[xToCheck][yToCheck].ToString()) == toFind && !ninesFound.Contains(xToCheck.ToString() + yToCheck.ToString()))
        {
            if (toFind == 9)
            {
                score++;
                ninesFound.Add(xToCheck.ToString() + yToCheck.ToString());
            }
            else
                score += CheckSurroundings(xToCheck, yToCheck, toFind+1);
        }

        xToCheck = x;
        yToCheck = y-1;
        if (yToCheck >= 0 && int.Parse(input[xToCheck][yToCheck].ToString()) == toFind && !ninesFound.Contains(xToCheck.ToString() + yToCheck.ToString()))
        {
            if (toFind == 9)
            {
                score++;
                ninesFound.Add(xToCheck.ToString() + yToCheck.ToString());
            }
            else
                score += CheckSurroundings(xToCheck, yToCheck, toFind + 1);
        }

        xToCheck = x;
        yToCheck = y + 1;
        if (yToCheck < input[x].Length && int.Parse(input[xToCheck][yToCheck].ToString()) == toFind && !ninesFound.Contains(xToCheck.ToString() + yToCheck.ToString()))
        {
            if (toFind == 9)
            {
                score++;
                ninesFound.Add(xToCheck.ToString() + yToCheck.ToString());
            }
            else
                score += CheckSurroundings(xToCheck, yToCheck, toFind + 1);
        }
        return score;
    }

    public int CheckSurroundings2(int x, int y, int toFind)
    {
        int score = 0;
        int xToCheck = x - 1;
        int yToCheck = y;

        if (xToCheck >= 0 && int.Parse(input[xToCheck][yToCheck].ToString()) == toFind)
        {
            if (toFind == 9)
            {
                score++;
            }
            else
                score += CheckSurroundings2(xToCheck, yToCheck, toFind + 1);

        }

        xToCheck = x + 1;
        yToCheck = y;
        if (xToCheck < input.Length && int.Parse(input[xToCheck][yToCheck].ToString()) == toFind)
        {
            if (toFind == 9)
            {
                score++;
            }
            else
                score += CheckSurroundings2(xToCheck, yToCheck, toFind + 1);
        }

        xToCheck = x;
        yToCheck = y - 1;
        if (yToCheck >= 0 && int.Parse(input[xToCheck][yToCheck].ToString()) == toFind)
        {
            if (toFind == 9)
            {
                score++;
            }
            else
                score += CheckSurroundings2(xToCheck, yToCheck, toFind + 1);
        }

        xToCheck = x;
        yToCheck = y + 1;
        if (yToCheck < input[x].Length && int.Parse(input[xToCheck][yToCheck].ToString()) == toFind)
        {
            if (toFind == 9)
            {
                score++;
            }
            else
                score += CheckSurroundings2(xToCheck, yToCheck, toFind + 1);
        }
        return score;
    }


    public override void part2()
    {
        base.part2();
        int score = 0;
        input = System.IO.File.ReadAllLines(path + "day10_input.txt");
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                int nextNum = 1;
                if (input[i][j] == '0')
                {
                    int x = i, y = j;
                    ninesFound.Clear();
                    score += CheckSurroundings2(x, y, nextNum);

                }
            }
        }
        Console.WriteLine("Answer part 2: " + score.ToString());
    }
}

