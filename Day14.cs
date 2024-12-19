using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IVec = IVector<int>;

internal class Day14 : Day
{
    int height = 103, width = 101;
    public Day14()
    {

    }

    public override void part1()
    {
        base.part1();
        string[] input = System.IO.File.ReadAllLines(path + "day14_input.txt");

        int topRight = 0, topLeft = 0, bottomRight = 0, bottomLeft = 0;
        //for (int i = 0; i < input.Length; i++)
        foreach (string line in input)
        {
            string[] l = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string p = l[0];
            string v = l[1];
            IVec pos = new IVec(0, 0);
            p = p.Split('=')[1];
            pos.x = int.Parse(p.Split(',')[0]);
            pos.y = int.Parse(p.Split(',')[1]);

            IVec vel = new IVec(0, 0);
            v = v.Split('=')[1];
            vel.x = int.Parse(v.Split(',')[0]);
            vel.y = int.Parse(v.Split(',')[1]);

            IVec distance = vel * 100;
            IVec newPos = (pos + distance);
            newPos.x = newPos.x % width;
            newPos.y = newPos.y % height;
            if(newPos.x < 0)
                newPos.x = width + newPos.x;

            if (newPos.y < 0)
                newPos.y = height + newPos.y;

            Console.WriteLine("Position: " + newPos.ToString());

            if (newPos.x > width / 2) // right
            {
                if (newPos.y > height / 2) // bottom
                {
                    bottomRight++;
                }
                else if (newPos.y < height / 2) //top
                {
                    topRight++;
                }
            }
            else if(newPos.x < width / 2) //left
            {
                if (newPos.y > height / 2) // bottom
                {
                    bottomLeft++;
                }
                else if (newPos.y < height / 2) //top
                {
                    topLeft++;
                }
            }
        }
        Console.WriteLine("Top Right: " + topRight.ToString());
        Console.WriteLine("Bottom Right: " + bottomRight.ToString());
        Console.WriteLine("Top Left: " + topLeft.ToString());
        Console.WriteLine("Bottom Left: " + bottomLeft.ToString());
        Console.WriteLine("Answer part 1: " + (topRight*topLeft*bottomRight*bottomLeft).ToString() );
    }

    public override void part2()
    {
        base.part2();
        List<KeyValuePair<IVec, IVec>> robots = new List<KeyValuePair<IVec, IVec>>();
        string[] input = System.IO.File.ReadAllLines(path + "day14_input.txt");

        int topRight = 0, topLeft = 0, bottomRight = 0, bottomLeft = 0;
        //for (int i = 0; i < input.Length; i++)
        foreach (string line in input)
        {
            string[] l = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            string p = l[0];
            string v = l[1];
            IVec pos = new IVec(0, 0);
            p = p.Split('=')[1];
            pos.x = int.Parse(p.Split(',')[0]);
            pos.y = int.Parse(p.Split(',')[1]);

            IVec vel = new IVec(0, 0);
            v = v.Split('=')[1];
            vel.x = int.Parse(v.Split(',')[0]);
            vel.y = int.Parse(v.Split(',')[1]);
            robots.Add(new KeyValuePair<IVec, IVec>( pos, vel));

        }


        int moves = 0;
        while (true)
        {
            char[][] room = new char[height][];
            for (int i = 0; i < room.Count(); i++)
            {
                room[i] = new char[width];
                for (int j = 0; j < room[i].Length; j++)
                    room[i][j] = '.';

            }
            moves++;
            for (int i = 0; i < robots.Count; i++)
            {
                robots[i] = new KeyValuePair<IVec, IVec>(Move(robots[i]), robots[i].Value);

                IVec robotPos = robots[i].Key;
                room[robotPos.y][robotPos.x] = 'X';

                if (robotPos.x > width / 2) // right
                {
                    if (robotPos.y > height / 2) // bottom
                    {
                        bottomRight++;
                    }
                    else if (robotPos.y < height / 2) //top
                    {
                        topRight++;
                    }
                }
                else if (robotPos.x < width / 2) //left
                {
                    if (robotPos.y > height / 2) // bottom
                    {
                        bottomLeft++;
                    }
                    else if (robotPos.y < height / 2) //top
                    {
                        topLeft++;
                    }
                }

            }
            if(topRight == topLeft && bottomRight == bottomLeft)
            {

                foreach (var line in room)
                {
                    foreach(char c in line)
                    {
                        Console.Write(c);
                    }
                    Console.WriteLine();

                }
            }
            

            Console.WriteLine("After " + moves.ToString() + " moves: ");

        }
    }

    IVec Move(KeyValuePair<IVec, IVec> robot)
    {
        IVec newPos = (robot.Key + robot.Value);
        newPos.x = newPos.x % width;
        newPos.y = newPos.y % height;
        if (newPos.x < 0)
            newPos.x = width + newPos.x;

        if (newPos.y < 0)
            newPos.y = height + newPos.y;

        return newPos;
    }
}

