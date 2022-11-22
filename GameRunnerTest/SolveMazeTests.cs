using GameRunner;

namespace GameRunnerTest
{
    [TestClass]
    public class SolveMazeTests
    {
        [TestMethod]
        public void SolveMazeWithExit()
        {
            Maze maze = new Maze(@"TestData\mapValid1.txt");
            Assert.IsNull(maze.pathToExitLength);
            Game.FindShortestPathLength(maze, maze.startPositionX, maze.startPositionY);
            Assert.AreEqual(5, maze.pathToExitLength);
        }

        [TestMethod]
        public void SolveMazeWithMultipleExits()
        {
            Maze maze = new Maze(@"TestData\mapValid2.txt");
            Assert.IsNull(maze.pathToExitLength);
            Game.FindShortestPathLength(maze, maze.startPositionX, maze.startPositionY);
            Assert.AreEqual(1, maze.pathToExitLength);
        }

        [TestMethod]
        public void SolveMazeWithStartingPositionOnExit()
        {
            Maze maze = new Maze(@"TestData\mapValid3.txt");
            Assert.IsNull(maze.pathToExitLength);
            Game.FindShortestPathLength(maze, maze.startPositionX, maze.startPositionY);
            Assert.AreEqual(0, maze.pathToExitLength);
        }

        [TestMethod]
        public void SolveMazeWithNoExit()
        {
            Maze maze = new Maze(@"TestData\mapValid4.txt");
            Assert.IsNull(maze.pathToExitLength);
            Game.FindShortestPathLength(maze, maze.startPositionX, maze.startPositionY);
            Assert.AreEqual(null, maze.pathToExitLength);
        }

        [TestMethod]
        public void CheckIfOutOfBorderCellIsValidToMoveTo()
        {
            Maze maze = new Maze(@"TestData\mapValid1.txt");
            Assert.AreEqual(false, Game.IsValid(maze, -1, -1));
            Assert.AreEqual(false, Game.IsValid(maze, -1, 0));
            Assert.AreEqual(false, Game.IsValid(maze, 0, -1));
            Assert.AreEqual(false, Game.IsValid(maze, 0, 6));
            Assert.AreEqual(false, Game.IsValid(maze, 6, 0));
            Assert.AreEqual(false, Game.IsValid(maze, 5, 6));
        }

        [TestMethod]
        public void CheckIfPathOnBorderCellIsValidToMoveTo()
        {
            Maze maze = new Maze(@"TestData\mapValid3.txt");
            Assert.AreEqual(true, Game.IsValid(maze, 0, 1));
            Assert.AreEqual(true, Game.IsValid(maze, 3, 0));
            Assert.AreEqual(true, Game.IsValid(maze, 3, 4));
            Assert.AreEqual(true, Game.IsValid(maze, 4, 2));
        }

        [TestMethod]
        public void CheckIfWallCellIsValidToMoveTo()
        {
            Maze maze = new Maze(@"TestData\mapValid3.txt");
            Assert.AreEqual(false, Game.IsValid(maze, 0, 0));
            Assert.AreEqual(false, Game.IsValid(maze, 2, 2));
            Assert.AreEqual(false, Game.IsValid(maze, 4, 4));
        }
    }
}
