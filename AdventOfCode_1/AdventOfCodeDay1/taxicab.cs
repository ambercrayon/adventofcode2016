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
        static void Main(string[] args)
        {
            Console.WriteLine("Starting");

            //get turns from file
            string turnsData = GetTurnsFromFile();

            List<string> turns = turnsData.Split(',').Select(p => p.Trim()).ToList(); 

            Console.WriteLine("turnscount: " + turns.Count);


            //execute turns, find b
            int[] coordinate = { 0, 0 }; //x (horizonal), y (vertical)
            char direction = 'N';
            int tripLength = 0;
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

                coordinate = GetNewCoordinate(coordinate, blocks, direction);
                Console.WriteLine("New Coordinates: X=" + coordinate[0] + " Y=" + coordinate[1]);

            }


            //use taxicab formula to get actual length of trip (number of bocks to Easter Bunny HQ)
            tripLength = Math.Abs( coordinate[0]) + Math.Abs(coordinate[1]);

            Console.WriteLine("Actual length of trip: " + tripLength);

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
                using (StreamReader sr = new StreamReader("../../Data/turns.txt"))
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
