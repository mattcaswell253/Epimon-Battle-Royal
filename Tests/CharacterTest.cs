using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Epimon
{
    public class CharacterTest : IDisposable
    {
        public CharacterTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=epimon_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void Test_SaveAndGet_SavesAndGetsCharacterObjAndId()
        {
            //Arrange
            Character firstCharacter = new Character("Fish", "Gyrados", 5, 5, 5);
            firstCharacter.Save();

            //Act
            Character savedCharacter = Character.GetAll()[0];

            int result = savedCharacter.GetId();
            int testId = firstCharacter.GetId();

            //Assert
            Assert.Equal(testId, result);
        }

        [Fact]
        public void Test_Find_FindsBandInDatabase()
        {
            //Arrange
            Character firstCharacter = new Character("Fish", "Gyrados", 5, 5, 5);
            firstCharacter.Save();

            //Act
            Character result = Character.Find(firstCharacter.GetId());

            //Assert
            Assert.Equal(firstCharacter, result);
        }
        public void Dispose()
       {
           Character.DeleteAll();
       }
    }
}
