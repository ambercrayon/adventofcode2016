using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCodeDay1
{
    class taxicab
    {
        static string input = "../../Data/turns.txt";

        static void Main(string[] args)
        {
                 //get turns from file
             List<string> turns = GetTurnsFromFile().Split(',').Select(p => p.Trim()).ToList(); 

            Console.WriteLine("turnscount: " + turns.Count);

            //execute turns, find final coordinate
            int[] prevCoord = { 0, 0 };
            int[] coord = { 0, 0 }; //x (horizonal), y (vertical)
            List<int[]> coordinatesVisited = new List<int[]>();

            char direction = 'N';
            int tripLength = 0;
            int[] secondVisitCoord = { 0, 0 };
            bool secondVisitFound = false;
            int x = 1;

                
            foreach (string turn in turns){
               
                string dir = turn.Substring(0, 1);
                int blocks = Convert.ToInt16(turn.Substring(1));

                direction = GetNewDirection(direction, dir[0]);
 
                switch (dir) {
                    case "L":
                        Console.WriteLine(x++ + ": Turning Left " + blocks + " blocks");
                        break;
                    case "R":
                        Console.WriteLine(x++ + ": Turning Right " + blocks + " blocks");
                        break;
                }

                Console.WriteLine("Now Facing: " + direction);

                prevCoord = coord;
                coord = GetNewCoordinate(coord, blocks, direction);
                Console.WriteLine("New Coordinates: X=" + coord[0] + " Y=" + coord[1]);

                if (!secondVisitFound)
                {
                    //get list of coordinates traveled in this move
                    List<int[]> visitedThisMove = GetCoordinatesHit(prevCoord, coord);

                    foreach (int[] currentVisit in visitedThisMove)
                    {
                        if (!secondVisitFound)
                        {
                            if (coordinatesVisited.Exists(c => c[0] == currentVisit[0] && c[1] == currentVisit[1]))
                            {
                                secondVisitFound = true;
                                secondVisitCoord = currentVisit;
                            }
                            else
                            {
                                coordinatesVisited.Add(currentVisit);
                            }
                        }
                    }
                }
            }


            //use taxicab formula to get actual length of trip (number of bocks to Easter Bunny HQ)
            tripLength = Math.Abs( coord[0]) + Math.Abs(coord[1]);

            Console.WriteLine("Actual length of trip: " + tripLength);

            if (secondVisitFound)
            {
                int secondVisitTripLength = Math.Abs(secondVisitCoord[0]) + Math.Abs(secondVisitCoord[1]);
                Console.WriteLine("First Spot Hit Twice: X=" + secondVisitCoord[0] + " Y=" + secondVisitCoord[1]);
                Console.WriteLine("Length to first coordinate visited twice: " + secondVisitTripLength);
            }
        }

        private static List<int[]> GetCoordinatesHit(int[] prevCoord, int[] coord)
        {
            List<int[]> visitedThisMove = new List<int[]>();

            //y changed
            if (prevCoord[0] == coord[0])
            {
                //prev y is lower
                if (prevCoord[1] < coord[1])
                {
                    for (int i = prevCoord[1]+1; i <= coord[1]; i++)
                    {
                        visitedThisMove.Add(new int[] { prevCoord[0], i });
                    }
                }
                //prev y is higher
                else
                {
                    for (int i = prevCoord[1]-1; i >= coord[1]; i--)
                    {
                        visitedThisMove.Add(new int[] { prevCoord[0], i });

                    }
                }
            }
            //x changed
            else
            {                
                //prev x is lower
                if (prevCoord[0] < coord[0])
                {
                    for (int i = prevCoord[0]+1; i <= coord[0]; i++)
                    {
                        visitedThisMove.Add(new int[] { i, prevCoord[1] });
                    }
                }
                //prev y is higher
                else
                {
                    for (int i = prevCoord[0]-1; i >= coord[0]; i--)
                    {
                        visitedThisMove.Add(new int[] { i, prevCoord[1]});

                    }
                }

            }
            return visitedThisMove;
        }

        private static int[] GetNewCoordinate(int[] coordinate, int blocks, char direction)
        {
            int[] newCoordinates = { 0, 0 }; //x (horizontal), y (vertical)

            switch (direction)
            {
                case 'N':
                    newCoordinates[0] = coordinate[0];
                    newCoordinates[1] = coordinate[1] + blocks;
                    break;
                case 'S':
                    newCoordinates[0] = coordinate[0];
                    newCoordinates[1] = coordinate[1] - blocks;
                    break;
                case 'E':
                    newCoordinates[0] = coordinate[0] + blocks;
                    newCoordinates[1] = coordinate[1];
                    break;
                case 'W':
                    newCoordinates[0] = coordinate[0] - blocks;
                    newCoordinates[1] = coordinate[1];
                    break;
            }

            return newCoordinates;
        }

        private static char GetNewDirection(char startingDir, char turnDir)
        {
            char newDir = ' ';

            switch (startingDir)
            {
                case 'N':
                   newDir = turnDir == 'L' ? 'W' : 'E';
                    break;
                case 'S':
                    newDir = turnDir == 'L' ? 'E' : 'W';
                    break;
                case 'E':
                    newDir = turnDir == 'L' ? 'N' : 'S';
                    break;
                case 'W':
                    newDir = turnDir == 'L' ? 'S' : 'N';
                    break;
            }

            return newDir;
        }

        private static string GetTurnsFromFile()
        {
            string turns = "";

            try
            {
                int n = 1;
         
                // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(input))
                {
                    // Read the stream to a string, and write the string to the console.
                    String line = sr.ReadToEnd();
                    Console.WriteLine(n++ + ": " +  line);

                    turns = line;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return turns;
        }
    } 
}
