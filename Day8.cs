using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


internal class Day8 : Day
{
    public override void part1()
    {
        string[] input = System.IO.File.ReadAllLines(path + "day8_input.txt");
        Dictionary<char, List<Vector2>> antennas = new Dictionary<char, List<Vector2>>();
        int height = input.Length;
        int width = input[0].Length;
        for(int x = 0; x < height; x++)
        {
            for(int y = 0; y< width; y++)
            {
                char c = input[x][y];
                if ((c == '.'))
                {
                    continue;
                }
                if (antennas.ContainsKey(c))
                {
                    antennas[c].Add(new Vector2(x,y));
                }
                else
                {
                    antennas[c] = new List<Vector2>();
                    antennas[c].Add(new Vector2(x, y));
                }
            }
        }



        foreach (char c in antennas.Keys)
        {
            //from first point to second to last. 
            for (int i = 0; i < antennas[c].Count-1; i++)
            {
                //from next point to end
                for (int j = i+1; j < antennas[c].Count; j++)
                {
                    //antenna i should always be smallest i think? 
                    Vector2 diff = antennas[c][j] - antennas[c][i];
                    Vector2 antinode1 = antennas[c][i] - diff;
                    Vector2 antinode2 = antennas[c][j] + diff;

                    //check if antinode is inside map
                    if (0 <= antinode1.X && antinode1.X < height && 0 <= antinode1.Y && antinode1.Y < width)
                    {
                        StringBuilder sb = new StringBuilder(input[(int)antinode1.X]);
                        sb[(int)antinode1.Y] = '#';
                        input[(int)antinode1.X] = sb.ToString();
                    }
                    if (0 <= antinode2.X && antinode2.X < height && 0 <= antinode2.Y && antinode2.Y < width)
                    {
                        StringBuilder sb = new StringBuilder(input[(int)antinode2.X]);
                        sb[(int)antinode2.Y] = '#';
                        input[(int)antinode2.X] = sb.ToString();
                    }

                }

            }

        }
        int numAntinode = 0;
        foreach (string line in input)
        {
            numAntinode+= line.Count<char>(c => c == '#');
            Console.WriteLine(line);
        }


        Console.WriteLine("Answer part 1: " + numAntinode.ToString());
        
    }


    public override void part2()
    {
        
        string[] input = System.IO.File.ReadAllLines(path + "day8_input.txt");
        Dictionary<char, List<Vector2>> antennas = new Dictionary<char, List<Vector2>>();
        int height = input.Length;
        int width = input[0].Length;

        //setup antenna locations
        for (int x = 0; x < height; x++)
        {
            for (int y = 0; y < width; y++)
            {
                char c = input[x][y];
                if ((c == '.'))
                {
                    continue;
                }
                if (antennas.ContainsKey(c))
                {
                    antennas[c].Add(new Vector2(x, y));
                }
                else
                {
                    antennas[c] = new List<Vector2>();
                    antennas[c].Add(new Vector2(x, y));
                }
            }
        }

        foreach (char c in antennas.Keys)
        {
            //from first point to second to last. 
            for (int i = 0; i < antennas[c].Count - 1; i++)
            {
                //from next point to end
                for (int j = i + 1; j < antennas[c].Count; j++)
                {
                    //antenna i should always be smallest i think? 
                    Vector2 diff = antennas[c][j] - antennas[c][i];

                    Vector2 antinodeLoc = antennas[c][j];
                    //Vector2 antinode2 = antennas[c][j] + diff;

                    //loop for every point in line inside map.
                    //first one way
                    while (0 <= antinodeLoc.X && antinodeLoc.X < height && 0 <= antinodeLoc.Y && antinodeLoc.Y < width)
                    {
                        StringBuilder sb = new StringBuilder(input[(int)antinodeLoc.X]);
                        sb[(int)antinodeLoc.Y] = '#';
                        input[(int)antinodeLoc.X] = sb.ToString();
                        antinodeLoc -= diff;
                    }

                    //then the other
                    antinodeLoc = antennas[c][j]+diff;
                    while (0 <= antinodeLoc.X && antinodeLoc.X < height && 0 <= antinodeLoc.Y && antinodeLoc.Y < width)
                    {
                        StringBuilder sb = new StringBuilder(input[(int)antinodeLoc.X]);
                        sb[(int)antinodeLoc.Y] = '#';
                        input[(int)antinodeLoc.X] = sb.ToString();
                        antinodeLoc += diff;

                    }

                }

            }

        }
        int numAntinode = 0;
        foreach (string line in input)
        {
            numAntinode += line.Count<char>(c => c == '#');
            Console.WriteLine(line);
        }


        Console.WriteLine("Answer part 2: " + numAntinode.ToString());

    }
}

