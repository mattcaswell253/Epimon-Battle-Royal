// using Xunit;
// using System.Collections.Generic;
// using System;
// using System.Data;
// using System.Data.SqlClient;
//
// namespace Epimon
// {
//     public class MoveTest : IDisposable
//     {
//         public static Move generalMove1 = new Move("tackle" ,"normal", 10);
//         public static Move generalMove2 = new Move("tackle" ,"normal", 10);
//         public static Character firstCharacter = new Character("Fish", "Gyrados", 5, 5, 5, "/content/img/rubydude1.png", "/content/img/rubydude2.png");
//
//         public MoveTest()
//         {
//             DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=epimon_test;Integrated Security=SSPI;";
//         }
//
//         [Fact]
//         public void MoveDatabaseEmpty()
//         {
//             //Arrange, act
//             int result = Move.GetAllMoves().Count;
//
//             //Assert
//             Assert.Equal(0,result);
//         }
//
//         [Fact]
//         public void Test_EqualOverrideTrueForSameMoveName()
//         {
//             // Arragne, Act, Assert
//             Assert.Equal(generalMove1, generalMove2);
//         }
//
//         [Fact]
//         public void Test_SaveToDatabase()
//         {
//             // Arrange
//             generalMove1.Save();
//
//             // act
//             List<Move> result = Move.GetAllMoves();
//             List<Move> testList = new List<Move>{generalMove1};
//
//             // Assert
//             Assert.Equal(testList, result);
//         }
//
//         [Fact]
//         public void Test_SaveAssignsIdToObject()
//         {
//             // Arrange
//             generalMove1.Save();
//
//             // Act
//             Move testMove = Move.GetAllMoves()[0];
//             int result = generalMove1.GetMoveId();
//             int testId = testMove.GetMoveId();
//
//             // Assert
//             Assert.Equal(testId, result);
//         }
//
//         [Fact]
//         public void Test_Find_FindsMoveInDatabase()
//         {
//             //Arrange
//             generalMove1.Save();
//             //Act
//             Move foundMove = Move.Find(generalMove1.GetMoveId());
//
//             //Asswert
//             Assert.Equal(generalMove1, foundMove);
//         }
//
//         [Fact]
//         public void Test_GetCharacters_ReturnAllCharactersInMove()
//         {
//             // Arragne
//             generalMove1.Save();
//             firstCharacter.Save();
//
//             // Act
//             generalMove1.AddCharacter(firstCharacter);
//             List<Character> savedCharacter = generalMove1.GetCharacters();
//             List<Character> testList = new List<Character> {firstCharacter};
//
//             // Assert
//             Assert.Equal(testList, savedCharacter);
//         }
//
//         public void Dispose()
//         {
//             Move.DeleteAll();
//             Character.DeleteAll();
//
//         }
//     }
// }
