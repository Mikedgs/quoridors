﻿using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Quoridors.Models;
using Quoridors.Models.Database;
using Quoridors.Models.DatabaseModels;
using Quoridors.Models.Services;

namespace QuoridorsTests.Models.Services
{
    [TestFixture]
    public class BoardToJsonMapper_Tests
    {
        [Test]
        public void Given_a_valid_input_the_mapper_returns_the_expected_output_from_the_CreateBoardObject_method()
        {
            // Arrange
            var mock = new Mock<IPositionRepository>();
            var cut = new BoardToJsonMapper(mock.Object);
            var game = new Game();

            // Act
            var result = cut.CreateBoardObject(game.Board, game);

            // Assert
            Assert.IsInstanceOf(typeof(BoardToJson), result);

        }

        [Test]
        public void GetListOfBricks_returns_a_valid_output()
        {
            // Arrange
            var game = new Game();
            var cut = new BoardToJsonMapper(null);

            // Act
            var result = cut.GetListOfBricks(game.Board);

            // Assert
            Assert.IsInstanceOf(typeof(List<Brick>), result);
        }

        [Test]
        public void GetListOfBricks_returns_correct_number_of_bricks_in_list_for_given_board()
        {
            // Arrange
            var game = new Game();
            game.Board[6][7] = BoardCellStatus.Wall;
            game.Board[8][7] = BoardCellStatus.Wall;
            var cut = new BoardToJsonMapper(null);

            // Act
            var result = cut.GetListOfBricks(game.Board);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetListOfBricks_returns_correct_list_for_given_board()
        {
            // Arrange
            var game = new Game();
            game.Board[6][7] = BoardCellStatus.Wall;
            var cut = new BoardToJsonMapper(null);

            // Act
            var result = cut.GetListOfBricks(game.Board)[0];

            // Assert
            Assert.That(result.XPos, Is.EqualTo(3));
            Assert.That(result.YPos, Is.EqualTo(4));
        }

        [Test]
        public void GetListOfPlayerPositions_returns_the_value_from_the_repository_mapped_correctly()
        {
            // Arrange
            var mock = new Mock<IPositionRepository>();
            mock.Setup(x => x.All()).Returns(new List<PositionDb> {new PositionDb(1,2,3,1)});
            var cut = new BoardToJsonMapper(mock.Object);

            // Act
            var result = cut.GetListOfPlayerPositions();
            // Assert
            Assert.IsInstanceOf(typeof(List<PositionJson>), result);
            mock.Verify(x => x.All(), Times.Exactly(1));

        }
    }
}
