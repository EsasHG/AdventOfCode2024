using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

internal class Day13 : Day
{

    public override void part1()
    {
        string[] input = System.IO.File.ReadAllLines(path + "day13_input.txt");
        ulong numTokens = 0;

        ulong xA = 0, xB = 0, yA = 0, yB = 0, xPrize = 0, yPrize = 0;
        for (int i = 0; i < input.Length; i++)
        {
            string line = input[i];
            string[] l = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (!string.IsNullOrWhiteSpace(line))
            {
                if (l[1] == "A:")
                {
                    xA = ulong.Parse(l[2].Substring(2, l[2].Length - 3));
                    yA = ulong.Parse(l[3].Substring(2, l[3].Length - 2));

                }
                else if (l[1] == "B:")
                {
                    xB = ulong.Parse(l[2].Substring(2, l[2].Length - 3));
                    yB = ulong.Parse(l[3].Substring(2, l[3].Length - 2));

                }
                else if(l[0] == "Prize:")
                {
                    xPrize = ulong.Parse(l[1].Substring(2, l[1].Length - 3));
                    yPrize = ulong.Parse(l[2].Substring(2, l[2].Length - 2));

                    List<ulong> costs = new List<ulong>();
                    for (ulong aPresses = 0; aPresses <= 100; aPresses++)
                    {
                        for (ulong bPresses = 0; bPresses <= 100; bPresses++)
                        {
                            if (xA * aPresses + xB * bPresses == xPrize && yA * aPresses + yB * bPresses == yPrize)
                            {
                                costs.Add(aPresses * 3 + bPresses);
                            }
                            else if (xA * aPresses + xB * bPresses > xPrize || yA * aPresses + yB * bPresses > yPrize)
                            {
                                break;
                            }
                        }
                    }
                    if (costs.Count > 0)
                    {
                        costs.Sort();
                        numTokens += costs[0];
                        Console.WriteLine("Added :" + costs[0].ToString());

                    }
                    else
                    {
                        Console.WriteLine("Will never be right!");

                    }
                }
            }

        }
        Console.WriteLine("Answer part 1: " + numTokens);

        base.part1();
    }

    //Don't know why this doesn't work. Works on testinput
    public void part1_attempt1()
    {
        string[] input = System.IO.File.ReadAllLines(path + "day13_input.txt");
        int numTokens = 0;

        float xA = 0, xB = 0, yA = 0, yB = 0, xPrize = 0, yPrize = 0;
        for (int i = 0; i < input.Length; i++)
        {
            string line = input[i];
            string[] l = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (!string.IsNullOrWhiteSpace(line))
            {
                    //xA*APRESS + xB*BPRESS == xPrize;
                    //APRESS = (xPrize - xB*BPRESS)/xA
                    //yA*APRESS + yB*BPRESS == yPrize;
                    //yA*(xPrize - xB*BPRESS)/xA + yB*BPRESS == yPrize;
                    //yA*xPrize/xA - yA*xB*BPRESS/xA + yB*BPRESS == yPrize;
                    //yB*BPRESS - yA*xB*BPRESS/xA == yPrize - yA*xPrize/xA ;
                    //BPRESS == (yPrize - y(xPrize - xB*BPRESS)/xA)/yB ;
                    //APRESS = (xPrize - xB*BPRESS)/xA

                    //yB*BPRESS*xA - yA*xB*BPRESS == yPrize*xA - yA*xPrize;
                    //(yB * xA - yA * xB)BPRESS = yPrize*xA -yA*xPrize;

               
                if (l[1] == "A:")
                {
                    xA = float.Parse(l[2].Substring(2, l[2].Length-3));
                    yA = float.Parse(l[3].Substring(2, l[3].Length - 2));

                }
                else if (l[1] == "B:")
                {
                    xB = float.Parse(l[2].Substring(2, l[2].Length - 3));
                    yB = float.Parse(l[3].Substring(2, l[3].Length - 2));

                }
                else
                {
                    xPrize = float.Parse(l[1].Substring(2, l[1].Length - 3));
                    yPrize = float.Parse(l[2].Substring(2, l[2].Length - 2));


                    float temp = yB * xA - yA * xB;
                    float BPRESS = (float)(yPrize * xA - yA * xPrize) / (float)(yB * xA - yA * xB);
                    float APRESS = (xPrize - xB * BPRESS) / xA;


                    if (float.IsInteger(APRESS) && float.IsInteger(BPRESS) && APRESS >= 0 && BPRESS >= 0)
                    {
                        numTokens += (int)APRESS * 3 + (int)BPRESS;
                        Console.WriteLine("Added " + ((int)APRESS * 3 + (int)BPRESS).ToString());

                    }
                    else
                    {
                        Console.WriteLine("Will never be right!");

                    }
                }
            }

        }
        Console.WriteLine("Answer part 1: " + numTokens);

    }
    public override void part2()
    {

        string[] input = System.IO.File.ReadAllLines(path + "day13_input.txt");
        ulong numTokens = 0;

        decimal xA = 0, xB = 0, yA = 0, yB = 0, xPrize = 0, yPrize = 0;
        for (int i = 0; i < input.Length; i++)
        {
            string line = input[i];
            string[] l = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (!string.IsNullOrWhiteSpace(line))
            {
                //xA*APRESS + xB*BPRESS == xPrize;
                //APRESS = (xPrize - xB*BPRESS)/xA
                //yA*APRESS + yB*BPRESS == yPrize;
                //yA*(xPrize - xB*BPRESS)/xA + yB*BPRESS == yPrize;
                //yA*xPrize/xA - yA*xB*BPRESS/xA + yB*BPRESS == yPrize;
                //yB*BPRESS - yA*xB*BPRESS/xA == yPrize - yA*xPrize/xA ;
                //BPRESS == (yPrize - y(xPrize - xB*BPRESS)/xA)/yB ;
                //APRESS = (xPrize - xB*BPRESS)/xA

                //yB*BPRESS*xA - yA*xB*BPRESS == yPrize*xA - yA*xPrize;
                //(yB * xA - yA * xB)BPRESS = yPrize*xA -yA*xPrize;


                if (l[1] == "A:")
                {
                    xA = decimal.Parse(l[2].Substring(2, l[2].Length - 3));
                    yA = decimal.Parse(l[3].Substring(2, l[3].Length - 2));

                }
                else if (l[1] == "B:")
                {
                    xB = decimal.Parse(l[2].Substring(2, l[2].Length - 3));
                    yB = decimal.Parse(l[3].Substring(2, l[3].Length - 2));

                }
                else
                {
                    xPrize = decimal.Parse(l[1].Substring(2, l[1].Length - 3));
                    yPrize = decimal.Parse(l[2].Substring(2, l[2].Length - 2));
                    xPrize += 10000000000000;
                    yPrize += 10000000000000;

                    decimal temp = yB * xA - yA * xB;
                    decimal BPRESS = (decimal)(yPrize * xA - yA * xPrize) / (decimal)(yB * xA - yA * xB);
                    decimal APRESS = (xPrize - xB * BPRESS) / xA;


                    if (decimal.IsInteger(APRESS) && decimal.IsInteger(BPRESS) && APRESS >= 0 && BPRESS >= 0)
                    {
                        numTokens += (ulong)APRESS * 3 + (ulong)BPRESS;
                        Console.WriteLine("Added");

                    }
                    else
                    {
                        Console.WriteLine("Will never be right!");

                    }
                }
            }

        }
        Console.WriteLine("Answer part 2: " + numTokens);

        base.part2();
    }

}

