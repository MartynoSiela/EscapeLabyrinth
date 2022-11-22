namespace GameRunner;

public class Game : IGame
{
    public int Run(string filePath)
    {
        // Create a Maze object or end the program if there are issues with the maze file
        Maze maze;
        try
        {
            maze = new Maze(filePath);
        } catch (InvalidMazeException)
        {
            return 0;
        }

        // Solve the maze
        FindShortestPathLength(maze, maze.startPositionX, maze.startPositionY);
        
        return 0;
    }

    /// <summary>
    /// This method checks that a given cell (by row and column) is a valid one to move into
    /// The cell has to be in the bounds of the maze
    /// It also has to be not previously visited and not a "wall"
    /// </summary>
    /// <param name="maze">Maze type object that has the map of the maze and matrix of visited cells</param>
    /// <param name="col">Y position of a cell to be checked</param>
    /// <param name="row">X position of a cell to be checked</param>
    public static bool IsValid(Maze maze, int row, int col)
    {
        return (col >= 0) && (row < maze.height) 
            && (row >= 0) && (col < maze.width)
            && maze.map[row,col] == 1 && maze.visited[row,col] == 0;
    }

    /// <summary>
    /// https://www.techiedelight.com/lee-algorithm-shortest-path-in-a-maze/
    /// This method uses Lee algorithm to find the shortest path from a maze
    /// A Maze type object needs to be provided together with a start position (defined by row and col parameters)
    /// </summary>
    /// <param name="maze">Maze type object that has the map of the maze and matrix of visited cells</param>
    /// <param name="col">X position of starting cell</param>
    /// <param name="row">Y position of starting cell</param>
    public static int FindShortestPathLength(Maze maze, int row, int col)
    {
        // Matrices for moving to all 4 possible directions from a cell
        int[] directionsRow = { -1, 0, 0, 1 };
        int[] directionsColumn = { 0, -1, 1, 0 };
        
        // A queue to keep track possible paths
        // Starting position (row, col) is enqueued as node and marked as visited
        // Starting position node has distance equal to 0
        Queue<Node> queue = new Queue<Node>();
        maze.visited[row, col] = 1;
        queue.Enqueue(new Node(col, row, 0));

        // Loop through the queue until it is not empty
        while (queue.Any())
        {
            // Pick a node from the queue and make it the current cell (row, col)
            // Node distance represents distance from starting position
            Node node = queue.Dequeue();
            int distance = node.distance;
            col = node.x;
            row = node.y;

            // Check if an exit has been reached (current position is on any edge of the maze)
            // Output the length of distance to starting position and break out of the loop
            if (col == 0 || row == 0 || row == maze.height - 1 || col == maze.width - 1)
            {
                maze.pathToExitLength = distance;
                Console.WriteLine(maze.pathToExitLength);
                break;
            }

            // Loop 4 times to check the validity of cells that are touching the current cell in all 4 possible directions
            // If a neighbouring cell is valid to be moved to, enqueue it and mark as visited
            for (int i = 0; i < 4; i++)
            {
                if (IsValid(maze, row + directionsColumn[i], col + directionsRow[i]))
                {
                    maze.visited[row + directionsColumn[i], col + directionsRow[i]] = 1;
                    queue.Enqueue(new Node(col + directionsRow[i], row + directionsColumn[i], distance + 1));
                }
            }
        }

        return 0;
    }
}