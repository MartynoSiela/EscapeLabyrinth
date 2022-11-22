using GameRunner;

namespace GameRunnerTest
{
    [TestClass]
    public class CreateMazeTests
    {
        [TestMethod]
        public void CreateValidMaze()
        {
            Maze maze = new Maze(@"TestData\mapValid1.txt");
            Assert.AreEqual(typeof(Maze), maze.GetType());
            Assert.AreEqual(6, maze.height);
            Assert.AreEqual(5, maze.width);
            Assert.AreEqual(2, maze.startPositionY);
            Assert.AreEqual(1, maze.startPositionX);
            Assert.AreEqual(null, maze.pathToExitLength);
        }

        [TestMethod]
        public void CreateMazeWithNoStartPosition()
        {
            string filePath = @"TestData\mapInvalid1.txt";
            try
            {
                Maze maze = new Maze(filePath);
            }
            catch (InvalidMazeException ex)
            {
                Assert.AreEqual("File corrupt, no starting position was found", ex.Message);
            }
        }

        [TestMethod]
        public void CreateMazeWithNonRectangularDimensions()
        {
            string filePath = @"TestData\mapInvalid2.txt";
            try
            {
                Maze maze = new Maze(filePath);
            } catch (InvalidMazeException ex)
            {
                Assert.AreEqual("File corrupt, line length inconsistent", ex.Message);
            }
        }

        [TestMethod]
        public void CreateMazeWithInvalidCharacters()
        {
            string filePath = @"TestData\mapInvalid3.txt";
            try
            {
                Maze maze = new Maze(filePath);
            }
            catch (InvalidMazeException ex)
            {
                Assert.AreEqual("File corrupt, invalid charater (C) found in line 4, column 3", ex.Message);
            }
        }

        [TestMethod]
        public void CreateMazeWithMultipleStartingPositions()
        {
            string filePath = @"TestData\mapInvalid4.txt";
            try
            {
                Maze maze = new Maze(filePath);
            }
            catch (InvalidMazeException ex)
            {
                Assert.AreEqual("File corrupt, multiple starting positions were found", ex.Message);
            }
        }
    }
}