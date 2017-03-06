using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Epimon
{
    public class Move
    {
        private int _id;
        private string _name;
        private string _type;
        private int _dmg;

        public Move(string name, string type, int dmg, int id = 0)
        {
            _id = id;
            _name = name;
            _type = type;
            _dmg = dmg;
        }

        public static List<Move> GetAllMoves()
        {
            List<Move> allMoves = new List<Move>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM moves;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int moveId = rdr.GetInt32(0);
                string moveName = rdr.GetString(1);
                string moveType = rdr.GetString(2);
                int moveDmg = rdr.GetInt32(3);
                Move newMove = new Move(moveName, moveType, moveDmg, moveId);
                allMoves.Add(newMove);

                if(rdr != null)
                {
                    rdr.Close();
                }
                if(conn != null)
                {
                    conn.Close();
                }
            }
            return allMoves;
        }

        public override bool Equals(System.Object otherMove)
        {
            if (!(otherMove is Move))
            {
                return false;
            }
            else
            {
                Move newMove = (Move) otherMove;
                bool nameEquality = this.GetMoveName() == newMove.GetMoveName();
                bool typeEquality = this.GetMoveType() == newMove.GetMoveType();
                bool dmgEquality = this.GetMoveDmg() == newMove.GetMoveDmg();
                return (nameEquality && typeEquality && dmgEquality);
            }
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM moves;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public int GetMoveId()
        {
            return _id;
        }
        public string GetMoveName()
        {
            return _name;
        }
        public string GetMoveType()
        {
            return _type;
        }
        public int GetMoveDmg()
        {
            return _dmg;
        }
    }
}
