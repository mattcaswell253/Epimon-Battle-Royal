using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Epimon
{
    public class MoveTest : IDisposable
    {
        // public static Move generalMove = new Move("")


        public MoveTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=epimon_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void BandDatabaseEmpty()
        {
            //Arrange, act
            int result = Move.GetAllMoves().Count;

            //Assert
            Assert.Equal(0,result);
        }






        public void Dispose()
        {
            Move.DeleteAll();
            // Character.DeleteAll();
        }
    }
}
