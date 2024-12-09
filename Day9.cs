using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

class File()
{
    public int size = 0;
    public int ID = -1;
}

internal class Day9 : Day
{


    public override void part1()
    {
        string input = System.IO.File.ReadAllText(path + "day9_input.txt");

        //can't use a string directly since ID's will quickly be more than one char
        List<string> diskMap = new List<string>();
        int size;
        for (int i = 0; i < input.Length; i++)
        {
            size = int.Parse(input[i].ToString());

            for (int j = 0; j < size; j++)
            {
                if (i % 2 == 0)
                {
                    diskMap.Add((i/2).ToString());
                }
                else
                {
                    diskMap.Add(".");
                }
            }
        }
        //foreach (string s in diskMap)
        //{
        //    Console.Write(s);
        //}
        //Console.WriteLine();

        while (diskMap.Contains("."))
        {
            string idToMove = "";
            for (int i = diskMap.Count - 1; i >= 0; i--)
            {
                idToMove = diskMap[i]; 
                diskMap.RemoveAt(i);
                
                if (idToMove != ".")
                {
                    break;
                }
            }

            bool newPosFound = false;
            for (int i = 0; i < diskMap.Count; i++)
            {
                if (diskMap[i].Equals(".")) 
                {
                    newPosFound = true;
                    diskMap[i] = idToMove;
                    break;
                }
            }
            if(!newPosFound)
            {
                diskMap.Add(idToMove);
            }
            //foreach (string s in diskMap)
            //{
            //    Console.Write(s);
            //}
            //Console.WriteLine();
        }
        long checksum = 0;
        for (int i = 0; i < diskMap.Count; i++)
        {
            if(!diskMap[i].Equals("."))
            {
                int id = int.Parse(diskMap[i]);
                checksum += i * id;
            }
        }
        Console.WriteLine("Answer part 1: " + checksum.ToString());
    }
    public override void part2() 
    {
        string input = System.IO.File.ReadAllText(path + "day9_input.txt");

        //can't use a string directly since ID's will quickly be more than one char
        List<File> diskMap = new List<File>();
        int size;
        for (int i = 0; i < input.Length; i++)
        {
            File f = new File();
            f.size = int.Parse(input[i].ToString());
            if (i % 2 == 0)
            {
                f.ID = i / 2;
            }
            else
            {
                f.ID = -1; // negative ID for empty spaces for simplicity;
            }
            diskMap.Add(f);
        }
        //foreach (string s in diskMap)
        //{
        //    Console.Write(s);
        //}
        //Console.WriteLine();

        for (int i = diskMap.Count - 1; i >= 0; i--)
        {
            File fileToMove = diskMap[i];

            if (fileToMove.ID == -1)
                continue;
            
            for (int j = 0; j < diskMap.Count; j++)
            {
                File possibleLoc = diskMap[j];
                if (possibleLoc.ID != -1)
                    continue;

                if (j >= i)
                    break;
                if(fileToMove.size <=  possibleLoc.size)
                {
                    int sizeDiff = possibleLoc.size - fileToMove.size;
                    diskMap[j] = fileToMove;
                    // add empty space where the file originally was.

                    File oldEmptySpace = new File();
                    oldEmptySpace.size = fileToMove.size;
                    oldEmptySpace.ID = -1;
                    diskMap[i] = oldEmptySpace;

                    if (i + 1 < diskMap.Count && diskMap[i + 1].ID == -1)
                    {
                        diskMap[i].size += diskMap[i + 1].size;
                        diskMap.RemoveAt(i + 1);
                    }
                    if (i-1 >= 0 && diskMap[i-1].ID == -1)
                    {
                        diskMap[i].size+= diskMap[i - 1].size;
                        diskMap.RemoveAt(i-1);
                    }




                    //add empty space behind file if it doesn't take it all
                    if (sizeDiff > 0)
                    {
                        File newSpace = new File();
                        newSpace.size = sizeDiff;
                        newSpace.ID = -1;
                        diskMap.Insert(j+1, newSpace);
                    }

                    //foreach (File f in diskMap)
                    //{
                    //    for (int k = 0; k < f.size; k++)
                    //    {
                    //        if (f.ID == -1)
                    //        {
                    //            Console.Write(".");
                    //        }
                    //        else
                    //        {
                    //            Console.Write(f.ID.ToString());
                    //        }


                    //    }

                    //}
                    //Console.WriteLine();

                    break;
                }
            }

        }

        int loc = 0;
        long checksum = 0;
        foreach(File currentFile in diskMap)
        {
            for (int i = 0; i < currentFile.size; i++)
            {
                if (currentFile.ID != -1)
                {
                    checksum += loc * currentFile.ID;
                    //Console.Write(currentFile.ID.ToString());

                }
                else
                {
                    //Console.Write(".");
                }
                loc++;

            }

        }
        Console.WriteLine("Answer part 2: " + checksum.ToString());

    }
}

