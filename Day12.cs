using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using IVec = IVector<int>;

class IVector<T> where T : INumber<T> 
{
    public IVector(T x, T y )   
    { 
        this.x = x;
        this.y = y;  

    }

    public T x;
    public T y;

    public static IVector<T> operator +(IVector<T> a) => a;
    public static IVector<T> operator +(IVector<T> a, IVector<T> b) => new IVector<T>(a.x+b.x, a.y+b.y);
    public static IVector<T> operator -(IVector<T> a, IVector<T> b) => new IVector<T>(a.x-b.x, a.y-b.y);

    public static IVector<T> operator *(IVector<T> a, T s) => new IVector<T>(a.x*s, a.y*s);

    public static bool operator ==(IVector<T> a, IVector<T> b) => (a.x == b.x && a.y == b.y);
    public static bool operator !=(IVector<T> a, IVector<T> b) => (a.x != b.x || a.y != b.y);

    public override bool Equals(object? obj)
    {
        if (obj == null || !(obj is IVec))
            return false;

        if (this.x == ((IVector<T>)obj).x && this.y == ((IVector<T>)obj).y)
            return true;
        return false;
    }

    public override string ToString()
    {
        return "{"+x.ToString() + "," + y.ToString() + "}";
    }

}

class VecEqualityComparer : IEqualityComparer<IVec> 
{
    public bool Equals(IVec? a, IVec? b)
    {
        if(a.x == b.x && a.y == b.y)
            return true;
        return false;
    }



    public int GetHashCode(IVec? obj) => obj.x ^ obj.y;
}

class Region
{
    public Region(int regionID)
    {
        id = regionID;
    }
    public int id;
    public int area = 0;
    public int border = 0;
}
internal class Day12 : Day
{
    string[] input;
    readonly VecEqualityComparer VecComparer;

    static int id = 0;
    int[][] regionIDs;
    public Day12()
    {
        input = System.IO.File.ReadAllLines(path + "day12_input.txt");
        VecComparer = new VecEqualityComparer();

        regionIDs = new int[input.Length][];

        for (int i = 0; i < regionIDs.Length; i++)
        {
            regionIDs[i] = new int[input[i].Length];
        }
    }

    private int checkBorders(int x, int y)
    {
        char region = input[y][x];

        int borders = 0;
        //to the left
        if (x <= 0)
            borders++;
        else if (input[y][x - 1] != region)
                borders++;

        //above
        if (y <= 0)
            borders++;
        else if (input[y - 1][x] != region)
            borders++;


        if (y>= input.Length-1)
            borders++;
        else if (input[y + 1][x] != region)
            borders++;


        if (x>= input[y].Length-1)
            borders++;
        else if (input[y][x + 1] != region)
            borders++;

        return borders;
    }

    public override void part1()
    {

        char currentRegion = '.';
        IVector<int> intVec = new IVector<int>(0,0);

        List<IVec> foundRegions = new List<IVec>();
        List<Region> regions = new List<Region>();

        int answer = 0;


        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                //new region found
                if(!foundRegions.Contains(new IVec(i,j), VecComparer))
                {
                    id++;
                    currentRegion = input[i][j];

                    int area = 0;
                    int borders = 0;
                    int k = j;
                    while (k < input[i].Length && input[i][k] == currentRegion && !foundRegions.Contains(new IVec(i, k), VecComparer))
                    {
                        foundRegions.Add(new IVec(i, k));
                        regionIDs[i][k] = id;
                        area++;
                        borders += checkBorders(k, i);
                       
                        CheckBelow(currentRegion, ref foundRegions, i, k, ref area, ref borders);
                        k++;
                    }
                    answer += area * borders;

                    foreach (int[] line in regionIDs)
                    {
                        foreach (int l in line)
                        {
                            Console.Write(l.ToString());
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();

                }
                else
                {
                    currentRegion = input[i][j];
                }
            }
            currentRegion = '.';
        }

        //foreach (int[] line in regionIDs)
        //{
        //    foreach(int i in line)
        //    {
        //        Console.Write(" " + i.ToString() + " ");
        //    }
        //    Console.WriteLine();
        //}

        Console.WriteLine("Answer part 1: " + answer.ToString()); 
        base.part1();
    }

    private void CheckBelow(char currentRegion, ref List<IVec> foundRegions, int i, int j, ref int area, ref int borders)
    {
        int newI = i + 1;
        if (newI < input.Length && input[newI][j] == currentRegion && !foundRegions.Contains(new IVec(newI, j), VecComparer))
        {

            int k = j;
            //while space behind is in region
            while (k >= 0 && input[newI][k] == currentRegion && !foundRegions.Contains(new IVec(newI, k), VecComparer))
            {
                foundRegions.Add(new IVec(newI, k));
                regionIDs[newI][k] = id;

                borders += checkBorders(k, newI);
                area++;
                if(k>=0)    
                    CheckBelow(currentRegion, ref foundRegions, newI, k, ref area, ref borders);
                k--;
            }

            k = j + 1;
            //while space in front is in region
            while (k < input[newI].Length && input[newI][k] == currentRegion && !foundRegions.Contains(new IVec(newI, k), VecComparer))
            {
                foundRegions.Add(new IVec(newI, k));
                regionIDs[newI][k] = id;

                borders += checkBorders(k, newI);
                area++;
                if (k < input[newI].Length)
                    CheckBelow(currentRegion, ref foundRegions, newI, k, ref area, ref borders);
                k++;
            }
        }
    }

    public override void part2()
    {
        base.part2();
    }
}

