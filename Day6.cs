using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

//class DIR
//{
//    public int x = 0;
//    public int y = 0;
//}

class DIR
{
    public bool up = false;
    public bool down = false;
    public bool left = false;
    public bool right = false;
}

internal class Day6 : Day
{


    public override void part1()
    {
        return;
        string[] input = System.IO.File.ReadAllLines("X:\\Programming\\C#\\AdventOfCode2024\\AdventOfCode2024\\Input\\day6_testinput.txt");

        Console.WriteLine("Before Changes:");

        foreach (string line in input)
        {
            Console.WriteLine(line);
        }

        Vector2 startDir = new Vector2(0, 0);
        Vector2 currentPos = new Vector2(-1, -1);
        for (int i = 0; i < input.Length; i++)
        {
            bool guardFound = false;
            currentPos.Y = i;
            for (int j = 0; j < input[i].Length; j++)
            {
                currentPos.X = j;
                if (input[i][j] == '^')
                {
                    guardFound = true;

                    startDir.Y = -1;
                    startDir.X = 0;

                }
                else if (input[i][j] == '>')
                {
                    guardFound = true;

                    startDir.Y = 0;
                    startDir.X = 1;
                }
                else if (input[i][j] == 'v')
                {
                    guardFound = true;

                    startDir.Y = 1;
                    startDir.X = 0;
                }
                else if (input[i][j] == '<')
                {
                    guardFound = true;

                    startDir.Y = 0;
                    startDir.X = -1;
                }
                if (guardFound)
                {
                    currentPos.Y = i;
                    currentPos.X = j;
                    break;
                }
            }
            if (guardFound)
            {
                break;
            }
        }
        Vector2 currentDir = startDir;
        //currentPos = startPos;
        currentPos = FollowToEnd(ref input, currentPos, currentDir);

        Console.WriteLine("After Changes:");


        var numPos = 0;
        foreach (string line in input)
        {
            foreach (char c in line)
            {
                if (c == 'X')
                    numPos++;
            }
            Console.WriteLine(line);
        }

        Console.WriteLine("Answer part 1: " + numPos.ToString());

    }

    private static Vector2 FollowToEnd(ref string[] input, Vector2 currentPos, Vector2 currentDir)
    {
        bool bEndFound = false;
        while (!bEndFound)
        {
            var newPos = Vector2.Zero;
            newPos.X = currentPos.X + currentDir.X;
            newPos.Y = currentPos.Y + currentDir.Y;
            if (newPos.Y < 0 || newPos.Y >= input.Length || newPos.X < 0 || newPos.X >= input[(int)newPos.Y].Length)
            {
                StringBuilder sb = new StringBuilder(input[(int)currentPos.Y]);
                sb[(int)currentPos.X] = 'X';
                input[(int)currentPos.Y] = sb.ToString();
                bEndFound = true;
            }
            else if (input[(int)newPos.Y][(int)newPos.X] == '#')
            {
                StringBuilder sb = new StringBuilder(input[(int)currentPos.Y]);
                sb[(int)currentPos.X] = '+';
                input[(int)currentPos.Y] = sb.ToString();

                int tempX = (int)currentDir.X;
                currentDir.X = -currentDir.Y;
                currentDir.Y = tempX;
            }
            else
            {
                //prev guard pos
                StringBuilder sb = new StringBuilder(input[(int)currentPos.Y]);
                sb[(int)currentPos.X] = 'X';
                input[(int)currentPos.Y] = sb.ToString();

                currentPos = newPos;
            }

        }

        return currentPos;
    }

    public bool Move(string[] input, ref Vector2 pos, ref Vector2 dir)
    {
        Vector2 newPos = pos + dir;

        if (newPos.Y < 0 || newPos.Y >= input.Length || newPos.X < 0 || newPos.X >= input[(int)newPos.Y].Length)
        {
            return true;
        }
        else if (input[(int)newPos.Y][(int)newPos.X] == '#' || input[(int)newPos.Y][(int)newPos.X] == '0')
        {
            int tempX = (int)dir.X;
            dir.X = -dir.Y;
            dir.Y = tempX;
        }
        else
        {
            pos = newPos;
        }
        return false;
    }

    public override void part2()
    {
        string[] input = System.IO.File.ReadAllLines("X:\\Programming\\C#\\AdventOfCode2024\\AdventOfCode2024\\Input\\day6_input.txt");

        Console.WriteLine("Before Changes:");
        foreach (string line in input)
        {
            Console.WriteLine(line);
        }
        Vector2 startDir = Vector2.Zero;
        Vector2 currentPos = new Vector2(-1, -1);
        Vector2 startPos = new Vector2(-1, -1);


        bool guardFound = false;


        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                char c = input[i][j];
                if (input[i][j] == '^')
                {
                    guardFound = true;
                    startDir.Y = -1;
                    startDir.X = 0;
                }
                if (input[i][j] == '>')
                {
                    guardFound = true;
                    startDir.Y = 0;
                    startDir.X = 1;

                }
                if (input[i][j] == 'v')
                {
                    guardFound = true;
                    startDir.Y = 1;
                    startDir.X = 0;
                }
                if (input[i][j] == '<')
                {
                    guardFound = true;
                    startDir.Y = 0;
                    startDir.X = -1;
                }

                if (guardFound)
                {
                    startPos = new Vector2(j, i);
                    break;
                }
            }
            if (guardFound)
                break;
        }



        List<List<DIR>> tempDirs = new List<List<DIR>>();
        List<string> tempInput = new List<string>();

        int possibleSpots = 0;
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                tempInput.Clear();
                for (int k = 0; k < input.Length; k++)
                {
                    tempInput.Add(input[k]);
                }

                char c = tempInput[i][j];
                if(c != '.')
                {
                    continue;
                }
                currentPos = startPos;
                Vector2 currentDir = startDir;

                StringBuilder sb = new StringBuilder(tempInput[i]);
                sb[j] = '#';
                tempInput[i] = sb.ToString();

                tempDirs.Clear();
                for (int k = 0; k < input.Length; k++)
                {
                    tempDirs.Add(new List<DIR>());
                    for (int l = 0; l < input[k].Length; l++)
                    {
                        tempDirs[k].Add(new DIR());
                    }
                }

                bool endReached = false;

                while(!endReached)
                {
                    //store previous position directions
                    if (currentDir.X == 1)
                    {
                        tempDirs[(int)currentPos.Y][(int)currentPos.X].right = true;
                    }
                    if (currentDir.X == -1)
                    {
                        tempDirs[(int)currentPos.Y][(int)currentPos.X].left = true;
                    }
                    if (currentDir.Y == 1)
                    {
                        tempDirs[(int)currentPos.Y][(int)currentPos.X].down = true;
                    }
                    if (currentDir.Y == -1)
                    {
                        tempDirs[(int)currentPos.Y][(int)currentPos.X].up = true;
                    }

                    endReached = Move(tempInput.ToArray(), ref currentPos, ref currentDir);

                    if(currentDir.X != 0 && currentDir.Y != 0)
                        throw new Exception("currentDir invalid!");

                    if (!endReached)
                    {
                        DIR storedDir = tempDirs[(int)currentPos.Y][(int)currentPos.X];

                        if (currentDir.X == 1 && storedDir.right)
                        {
                            possibleSpots++;
                            endReached = true;
                        }
                        else if (currentDir.X == -1 && storedDir.left)
                        {
                            possibleSpots++;
                            endReached = true;
                        }
                        else if (currentDir.Y == 1 && storedDir.down)
                        {
                            possibleSpots++;
                            endReached = true;
                        }
                        else if (currentDir.Y == -1 && storedDir.up)
                        {
                            possibleSpots++;
                            endReached = true;
                        }
                    }
                }
                   
            }
        }
    
        Console.WriteLine("Answer Part 2: " + possibleSpots.ToString());
    
    }

}



//public void part1_temp()
//{
//    string[] input = System.IO.File.ReadAllLines("X:\\Programming\\C#\\AdventOfCode2024\\AdventOfCode2024\\Input\\day6_input.txt");


//    Console.WriteLine("Before Changes:");
//    foreach (string line in input)
//    {
//        Console.WriteLine(line);
//    }
//    Vector2 startDir = Vector2.Zero;
//    Vector2 currentPos = new Vector2(-1, -1);
//    Vector2 startPos = new Vector2(-1, -1);


//    bool guardFound = false;
//    List<List<Vector2>> dirs = new List<List<Vector2>>();  

//    for (int i = 0; i < input.Length; i++)
//    {
//        dirs.Add(new List<Vector2>());
//        for (int j = 0; j < input[i].Length; j++)
//        {
//            dirs[i].Add(Vector2.Zero);
//        }
//    }

//    for (int i = 0; i < input.Length; i++)
//    {
//        for (int j = 0; j < input[i].Length; j++)
//        {
//            char c = input[i][j];
//            if (input[i][j] == '^')
//            {
//                guardFound = true;
//                startDir.Y = -1;
//                startDir.X= 0;
//            }
//            if (input[i][j] == '>')
//            {
//                guardFound = true;
//                startDir.Y = 0;
//                startDir.X = 1;

//            }
//            if (input[i][j] == 'v')
//            {
//                guardFound = true;
//                startDir.Y = 1;
//                startDir.X = 0;
//            }
//            if (input[i][j] == '<')
//            {
//                guardFound = true;
//                startDir.Y = 0;
//                startDir.X = -1;
//            }

//            if (guardFound)
//            {
//                startPos = new Vector2 (j,i);
//                break;
//            }
//        }
//        if (guardFound)
//            break;
//    }


//    currentPos = startPos;

//    Vector2 currentDir = startDir;
//    List<List<Vector2>> tempDirs = new List<List<Vector2 >> ();
//    List<string> tempInput = new List<string> ();
//    bool endReached = false;


//    //while (!endReached)
//    //{





//    //    Vector2 tempBlockPos = currentPos + currentDir;
//    //    if (tempBlockPos.Y > 0 && tempBlockPos.Y < tempInput.Count && tempBlockPos.X > 0 && tempBlockPos.X < tempInput[(int)tempBlockPos.Y].Length)
//    //    {
//    //        StringBuilder sb = new StringBuilder(tempInput[(int)tempBlockPos.Y]);
//    //        sb[(int)tempBlockPos.X] = '#';
//    //        tempInput[(int)tempBlockPos.Y] = sb.ToString();
//    //    }

//    //    //foreach(string s  in tempInput) 
//    //    //{
//    //    //    Console.WriteLine(s);
//    //    //}

//    //    Vector2 possibleDir = currentDir;

//    //    //possibleDir.X = -currentDir.Y;
//    //    //possibleDir.Y = currentDir.X;
//    //    Vector2 possiblePos = currentPos;
//    //    bool endReached2 = false;
//    //    while (!endReached2)
//    //    {
//    //        if (possibleDir.X != 0)
//    //            tempDirs[(int)possiblePos.Y][(int)possiblePos.X] = new Vector2(possibleDir.X, tempDirs[(int)possiblePos.Y][(int)possiblePos.X].Y);
//    //        else
//    //            tempDirs[(int)possiblePos.Y][(int)possiblePos.X] = new Vector2(tempDirs[(int)possiblePos.Y][(int)possiblePos.X].X, possibleDir.Y);

//    //        endReached2 = Move(tempInput.ToArray(), ref possiblePos, ref possibleDir);
//    //        if (!endReached2)
//    //        {

//    //            if ((possibleDir.X != 0 && dirs[(int)possiblePos.Y][(int)possiblePos.X].X == possibleDir.X) || (possibleDir.Y != 0 && dirs[(int)possiblePos.Y][(int)possiblePos.X].Y == possibleDir.Y))
//    //            {
//    //                possibleSpots++;
//    //                endReached2 = true;
//    //            }
//    //            //else if ((possibleDir.X != 0 && tempDirs[(int)possiblePos.Y][(int)possiblePos.X].X == possibleDir.X) || (possibleDir.Y != 0 && tempDirs[(int)possiblePos.Y][(int)possiblePos.X].Y == possibleDir.Y))
//    //            //{
//    //            //    possibleSpots++;
//    //            //    endReached2 = true;
//    //            //}
//    //        }
//    //    }


//    //    if (currentDir.X != 0)
//    //        dirs[(int)currentPos.Y][(int)currentPos.X] = new Vector2(currentDir.X, dirs[(int)currentPos.Y][(int)currentPos.X].Y);
//    //    else
//    //        dirs[(int)currentPos.Y][(int)currentPos.X] = new Vector2(dirs[(int)currentPos.Y][(int)currentPos.X].X, currentDir.Y);

//    //    endReached = Move(input, ref currentPos, ref currentDir);


//    //}

//    //if (currentDir.X != 0)
//    //    dirs[(int)currentPos.Y][(int)currentPos.X] = new Vector2(currentDir.X, dirs[(int)currentPos.Y][(int)currentPos.X].Y);
//    //else
//    //    dirs[(int)currentPos.Y][(int)currentPos.X] = new Vector2(dirs[(int)currentPos.Y][(int)currentPos.X].X, currentDir.Y);
//    List<List<bool>> visited = new List<List<bool>>();


//    for (int i = 0; i < input.Length; i++)
//    {
//        visited.Add(new List<bool>());
//        for (int j = 0; j < input[i].Length; j++)
//        {
//            visited[i].Add(false);
//        }
//    }

//    List<Vector2> validObstaclePos = new List<Vector2>();

//    int possibleSpots = 0;
//    while (!endReached)
//    {
//        tempInput.Clear();
//        for (int i = 0; i < input.Length; i++)
//        {
//            tempInput.Add(input[i]);
//        }

//        tempDirs.Clear();
//        for (int i = 0; i < input.Length; i++)
//        {
//            tempDirs.Add(new List<Vector2>());
//            for (int j = 0; j < input[i].Length; j++)
//            {
//                tempDirs[i].Add(dirs[i][j]);
//            }
//        }

//        Vector2 tempBlockPos = currentPos + currentDir;
//        if(!validObstaclePos.Contains(tempBlockPos))
//        {

//            if (tempBlockPos.Y > 0 && tempBlockPos.Y < tempInput.Count && tempBlockPos.X > 0 && tempBlockPos.X < tempInput[(int)tempBlockPos.Y].Length)
//            {
//                //NB: Må passe på at obstacle ikke legges på samme sted som vi har gjort tidligere.
//                StringBuilder stb = new StringBuilder(tempInput[(int)tempBlockPos.Y]);
//                stb[(int)tempBlockPos.X] = '0';
//                tempInput[(int)tempBlockPos.Y] = stb.ToString();
//            }

//            Vector2 possiblePos = currentPos;
//            Vector2 possibleDir = currentDir;
//            bool endReached2 = false;
//            int numTurns = 0;

//            while (!endReached2)
//            {
//                if (possibleDir.X != 0)
//                    tempDirs[(int)possiblePos.Y][(int)possiblePos.X] = new Vector2(possibleDir.X, tempDirs[(int)possiblePos.Y][(int)possiblePos.X].Y);
//                else
//                    tempDirs[(int)possiblePos.Y][(int)possiblePos.X] = new Vector2(tempDirs[(int)possiblePos.Y][(int)possiblePos.X].X, possibleDir.Y);

//                //visited[(int)possiblePos.Y][(int)possiblePos.X] = true;

//                //Console.WriteLine("Possible Pos: " + possiblePos.ToString() + "Possible Dir: " + possibleDir.ToString());


//                Vector2 tempPossDir = possibleDir;
//                endReached2 = Move(tempInput.ToArray(), ref possiblePos, ref possibleDir);
//                //if (possibleDir != tempPossDir)
//                //    numTurns++;
//                //if (numTurns >= 4)
//                //{
//                //    endReached2 = true;
//                //}
//                if (!endReached2)
//                {
//                    Vector2 possPosDir = dirs[(int)possiblePos.Y][(int)possiblePos.X];
//                    Vector2 possPosTempDir = tempDirs[(int)possiblePos.Y][(int)possiblePos.X];
//                    if ((possibleDir.X != 0 && possPosDir.X == possibleDir.X) || (possibleDir.Y != 0 && possPosDir.Y == possibleDir.Y))
//                    {
//                        Console.WriteLine("Found normal spot: ");

//                        validObstaclePos.Add(tempBlockPos);
//                        possibleSpots++;
//                        //bool looped = false;
//                        //Vector2 currentPossPos = possiblePos;
//                        //while (!looped)
//                        //{
//                        //    StringBuilder syb = new StringBuilder(tempInput[(int)possiblePos.Y]);
//                        //    syb[(int)possiblePos.X] = 'X';
//                        //    tempInput[(int)possiblePos.Y] = syb.ToString();

//                        //    looped = Move(tempInput.ToArray(), ref possiblePos, ref possibleDir);

//                        //    if (currentPossPos == possiblePos)
//                        //    {
//                        //        looped = true;
//                        //    }
//                        //}
//                        //foreach (string s in tempInput)
//                        //{
//                        //    Console.WriteLine(s);
//                        //}
//                        endReached2 = true;
//                    }
//                    else if ((possibleDir.X != 0 && possPosTempDir.X == possibleDir.X) || (possibleDir.Y != 0 && possPosTempDir.Y == possibleDir.Y))
//                    {
//                        validObstaclePos.Add(tempBlockPos);

//                        Console.WriteLine("Found tempdir loop spot: ");
//                        //bool looped = false;
//                        //Vector2 currentPossPos = possiblePos;
//                        //while (!looped)
//                        //{
//                        //    StringBuilder sb2 = new StringBuilder(tempInput[(int)possiblePos.Y]);
//                        //    sb2[(int)possiblePos.X] = '!';
//                        //    tempInput[(int)possiblePos.Y] = sb2.ToString();
//                        //    looped = Move(tempInput.ToArray(), ref possiblePos, ref possibleDir);

//                        //    if (currentPossPos == possiblePos)
//                        //    {
//                        //        looped = true;
//                        //    }
//                        //}

//                        //foreach (string s in tempInput)
//                        //{
//                        //    Console.WriteLine(s);
//                        //}
//                        possibleSpots++;
//                        endReached2 = true;
//                    }

//                }
//            }
//        }

//        StringBuilder sb = new StringBuilder(input[(int)currentPos.Y]);
//        if (currentDir.X != 0)
//        {
//            if(sb[(int)currentPos.X] == '|')
//                sb[(int)currentPos.X] = '+';
//            else
//                sb[(int)currentPos.X] = '-';

//            dirs[(int)currentPos.Y][(int)currentPos.X] = new Vector2(currentDir.X, dirs[(int)currentPos.Y][(int)currentPos.X].Y);
//        }
//        else
//        {
//            if (sb[(int)currentPos.X] == '-')
//                sb[(int)currentPos.X] = '+';
//            else
//                sb[(int)currentPos.X] = '|';
//            dirs[(int)currentPos.Y][(int)currentPos.X] = new Vector2(dirs[(int)currentPos.Y][(int)currentPos.X].X, currentDir.Y);
//        }
//        //visited[(int)currentPos.Y][(int)currentPos.X] = true;

//        input[(int)currentPos.Y] = sb.ToString();



//        endReached = Move(input, ref currentPos, ref currentDir);


//    }

//    foreach (string s in input)
//    {
//        Console.WriteLine(s);
//    }

//    if (currentDir.X != 0)
//        dirs[(int)currentPos.Y][(int)currentPos.X] = new Vector2(currentDir.X, dirs[(int)currentPos.Y][(int)currentPos.X].Y);
//    else
//        dirs[(int)currentPos.Y][(int)currentPos.X] = new Vector2(dirs[(int)currentPos.Y][(int)currentPos.X].X, currentDir.Y);



//    currentPos = startPos;
//    currentDir = startDir;

//    endReached = false;
//    //while (!endReached)
//    //{
//    //    Vector2 possiblePos = currentPos;
//    //    Vector2 possibleDir = Vector2.Zero;
//    //    possibleDir.X = -currentDir.Y;
//    //    possibleDir.Y = currentDir.X;
//    //    bool endReached2 = false;

//    //    while (!endReached2)
//    //    {
//            //if (possibleDir.X != 0)
//            //    tempDirs[(int)possiblePos.Y][(int)possiblePos.X] = new Vector2(possibleDir.X, tempDirs[(int)possiblePos.Y][(int)possiblePos.X].Y);
//            //else
//            //    tempDirs[(int)possiblePos.Y][(int)possiblePos.X] = new Vector2(tempDirs[(int)possiblePos.Y][(int)possiblePos.X].X, possibleDir.Y);

//            //visited[(int)possiblePos.Y][(int)possiblePos.X] = true;

//            //endReached2 = Move(input, ref possiblePos, ref possibleDir);
//            //if (!endReached2)
//            //{
//            //    if(visited[(int)possiblePos.Y][(int)possiblePos.X])
//            //    {
//            //        if ((possibleDir.X != 0 && dirs[(int)possiblePos.Y][(int)possiblePos.X].X == possibleDir.X) || (possibleDir.Y != 0 && dirs[(int)possiblePos.Y][(int)possiblePos.X].Y == possibleDir.Y))
//            //        {
//            //            possibleSpots++;
//            //            endReached2 = true;
//            //        }
//            //    }
//            //}
//            //else if ((possibleDir.X != 0 && tempDirs[(int)possiblePos.Y][(int)possiblePos.X].X == possibleDir.X) || (possibleDir.Y != 0 && tempDirs[(int)possiblePos.Y][(int)possiblePos.X].Y == possibleDir.Y))
//            //{
//            //    possibleSpots++;
//            //    endReached2 = true;
//            //}

//    //    }
//    //    visited[(int)currentPos.Y][(int)currentPos.X] = true;

//    //    endReached = Move(input, ref currentPos, ref currentDir);

//    //}

//    Console.WriteLine("After Changes:");
//    Console.WriteLine("Answer part 2: " + possibleSpots.ToString());


//    //currentX = startX;
//    //currentY = startY;
//    //dirX = startDirX;
//    //dirY = startDirY;

//    //var numPos = 0;
//    //foreach (string line in input)
//    //{
//    //    Console.WriteLine(line);
//    //}

//    //int possibleSpots = 0;
//    //currentPos = startPos;
//    //currentDir = startDir;

//    //endReached = false;
//    //while (!endReached)
//    //{

//    //    endReached = Move(input, ref currentPos, ref currentDir);
//    //    //        if (possibleNewY < 0 || possibleNewY >= input.Length || newX < 0 || newX >= input[possibleNewY].Length)
//    //    //        {
//    //    //            if (dirX != 0)
//    //    //                dirs[currentY][currentX].x = dirX;
//    //    //            else
//    //    //                dirs[currentY][currentX].y = dirY;

//    //    //            StringBuilder sb = new StringBuilder(input[currentY]);
//    //    //            sb[currentX] = 'X';
//    //    //            input[currentY] = sb.ToString();
//    //    //            endReached = true;
//    //    //            break;
//    //    //        }

//    //    //        if (input[possibleNewY][newX] == '#')
//    //    //        {
//    //    //            StringBuilder sb = new StringBuilder(input[currentY]);
//    //    //            sb[currentX] = '+';
//    //    //            input[currentY] = sb.ToString();

//    //    //            tempX = dirX;
//    //    //            dirX = -dirY;
//    //    //            dirY = tempX;

//    //    //        }
//    //    //        else
//    //    //        {
//    //    //            prev guard pos
//    //    //            StringBuilder sb = new StringBuilder(input[currentY]);
//    //    //            if (sb[currentX] == '+')
//    //    //                sb[currentX] = '+';
//    //    //            else if (sb[currentX] == '|' || sb[currentX] == '-')
//    //    //                sb[currentX] = '+';
//    //    //            else if (dirY != 0)
//    //    //                sb[currentX] = '|';
//    //    //            else
//    //    //                sb[currentX] = '-';
//    //    //            input[currentY] = sb.ToString();
//    //    //            StringBuilder sb2 = new StringBuilder(input[newY]);
//    //    //            sb2[newX] = currGuardDir;
//    //    //            input[newY] = sb2.ToString();

//    //    //            currentY = newY;
//    //    //            currentX = newX;
//    //    //        }
//    //    //        if (dirX != 0)
//    //    //            dirs[currentY][currentX].x = dirX;
//    //    //        else
//    //    //            dirs[currentY][currentX].y = dirY;

//    //    //        if (input[newY][newX] == '#')
//    //    //        {

//    //    //            tempX = dirX;
//    //    //            dirX = -dirY;
//    //    //            dirY = tempX;

//    //    //        }
//    //    //    }

//    //    //    newY = currentY + dirY;
//    //    //    newX = currentX + dirX;

//    //    //    if (newY < 0 || newY >= input.Length || newX < 0 || newX >= input[newY].Length)
//    //    //    {
//    //    //        StringBuilder sb = new StringBuilder(input[currentY]);
//    //    //        sb[currentX] = 'X';
//    //    //        input[currentY] = sb.ToString();
//    //    //        endReached = true;
//    //    //        break;
//    //    //    }

//    //    //    if (input[newY][newX] == '#')
//    //    //    {
//    //    //        StringBuilder sb = new StringBuilder(input[currentY]);
//    //    //        sb[currentX] = '+';
//    //    //        input[currentY] = sb.ToString();

//    //    //        tempX = dirX;
//    //    //        dirX = -dirY;
//    //    //        dirY = tempX;

//    //    //    }
//    //    //    else
//    //    //    {
//    //    //        prev guard pos
//    //    //        StringBuilder sb = new StringBuilder(input[currentY]);
//    //    //        if (sb[currentX] == '+')
//    //    //            sb[currentX] = '+';
//    //    //        else if (sb[currentX] == '|' || sb[currentX] == '-')
//    //    //            sb[currentX] = '+';
//    //    //        else if (dirY != 0)
//    //    //            sb[currentX] = '|';
//    //    //        else
//    //    //            sb[currentX] = '-';
//    //    //        input[currentY] = sb.ToString();
//    //    //        StringBuilder sb2 = new StringBuilder(input[newY]);
//    //    //        sb2[newX] = currGuardDir;
//    //    //        input[newY] = sb2.ToString();

//    //    //        currentY = newY;
//    //    //        currentX = newX;
//    //    //    }
//    //    //}

//    //}

//    //    Console.WriteLine("Answer part 2: " + possibleSpots.ToString());

//}