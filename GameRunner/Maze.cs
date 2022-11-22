namespace GameRunner
{
    /// <summary>
    /// Maze object is used to store all the necessary information about the maze
    /// map is an array where 0s represent walls and 1s represent open paths
    /// visited is an array that is used to keep track of visited cells while solving the maze
    /// </summary>
    public class Maze
    {
        public int[,] map { get; set; }
        public int[,] visited { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int startPositionX { get; set; }
        public int startPositionY { get; set; } 
        public int? pathToExitLength { get; set; }

        public Maze(string filePath)
        {
            ReadFromFile(filePath);
            pathToExitLength = null;
            InitiateVisitedMatrix();
        }

        /// <summary>
        /// Reads a given maze file
        /// This method is used when creating a Maze object or throw an exception if there are issues with the file
        /// </summary>
        /// <param name="filePath"></param>
        /// <exception cref="InvalidMazeException"></exception>
        public void ReadFromFile(string filePath)
        {
            startPositionY = int.MaxValue;
            startPositionX = int.MaxValue;
            bool startPositionIsPresent = false;
            string[] lines = File.ReadAllLines(filePath);
            height = lines.Length;
            width = lines[0].Length;
            map = new int[height, width];

            for (int i = 0; i < height; i++)
            {
                // Check if all the lines are of the same length (first line is used as reference)
                // If the length of lines differs, throw an exception because the map is not rectangular
                if (lines[i].Length != width)
                {
                    throw new InvalidMazeException("File corrupt, line length inconsistent");
                }
                for (int j = 0; j < width; j++)
                {
                    // Reformat map from the file
                    // 1s are marked as 0s
                    // empty spaces are marked as 1s
                    // start position is marked as 1 and stored in Maze object properties (startPositionX and startPositionY)
                    // characters other than ' ', '1' and 'X' are not permitted and an exception is thrown
                    switch (lines[i][j])
                    {
                        case '1':
                            map[i, j] = 0;
                            break;
                        case ' ':
                            map[i, j] = 1;
                            break;
                        case 'X':
                            // Checking if a starting position was already found among previous characters
                            // Duplicate starting positions are not permitted, throw an exception in that case
                            if (startPositionIsPresent)
                            {
                                throw new InvalidMazeException($@"File corrupt, multiple starting positions were found");
                            } 
                            else
                            {
                                map[i, j] = 1;
                                startPositionX = i;
                                startPositionY = j;
                                startPositionIsPresent = true;
                                break;
                            }
                        default:
                            throw new InvalidMazeException(@$"File corrupt, invalid charater ({lines[i][j]}) found in line {i + 1}, column {j + 1}");
                    }
                }
            }

            // An exception is thrown if no starting position is found in the map file
            if (startPositionY == int.MaxValue)
            {
                throw new InvalidMazeException("File corrupt, no starting position was found");
            }
        }

        /// <summary>
        /// Creates an array of the same size as the map of the maze filled with 0s
        /// This array is used later to keep track of visited cells when solving the maze
        /// </summary>
        public void InitiateVisitedMatrix()
        {
            visited = new int[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    visited[i, j] = 0;
                }
            }
        }
    }

    public class InvalidMazeException : Exception
    {
        public InvalidMazeException(string message) : base(message)
        {

        }
    }
}