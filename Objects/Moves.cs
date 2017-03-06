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
            }
                if(rdr != null)
                {
                    rdr.Close();
                }
                if(conn != null)
                {
                    conn.Close();
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

        public void Save()
       {
           SqlConnection conn = DB.Connection();
           conn.Open();

           SqlCommand cmd = new SqlCommand("INSERT INTO moves (name, type, dmg) OUTPUT INSERTED.id VALUES (@MoveName, @MoveType, @MoveDmg);", conn);
           SqlParameter nameParameter = new SqlParameter("@MoveName", this.GetMoveName());
           cmd.Parameters.Add(nameParameter);
           SqlParameter typeParameter = new SqlParameter("@MoveType", this.GetMoveType());
           cmd.Parameters.Add(typeParameter);
           SqlParameter dmgParameter = new SqlParameter("@MoveDmg", this.GetMoveDmg());
           cmd.Parameters.Add(dmgParameter);

           SqlDataReader rdr = cmd.ExecuteReader();
           while(rdr.Read())
           {
               this._id = rdr.GetInt32(0);
           }
           if(rdr != null)
           {
               rdr.Close();
           }
           if(conn != null)
           {
               conn.Close();
           }
       }

       public static Move Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM moves WHERE id = @MoveId;", conn);
            SqlParameter MoveIdParameter = new SqlParameter("@MoveId", id.ToString());
            cmd.Parameters.Add(MoveIdParameter);

            SqlDataReader rdr = cmd.ExecuteReader();
            int foundMoveId = 0;
            string foundMoveName = null;
            string foundMoveType = null;
            int foundMoveDmg = 0;

            while(rdr.Read())
            {
                foundMoveId = rdr.GetInt32(0);
                foundMoveName = rdr.GetString(1);
                foundMoveType = rdr.GetString(2);
                foundMoveDmg = rdr.GetInt32(3);
            }
            Move foundMove = new Move(foundMoveName, foundMoveType, foundMoveDmg, foundMoveId);

            if(rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return foundMove;
        }

        public void AddCharacter(Character newCharacter)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO characters_moves (character_id, move_id) VALUES (@CharacterId, @MoveId);", conn);
            SqlParameter characterIdParameter = new SqlParameter("@CharacterId", newCharacter.GetId());
            SqlParameter moveIdParameter = new SqlParameter("@MoveId", this.GetMoveId());
            cmd.Parameters.Add(characterIdParameter);
            cmd.Parameters.Add(moveIdParameter);

            cmd.ExecuteNonQuery();
            if(conn != null)
            {
                conn.Close();
            }
        }

        public List<Character> GetCharacters()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT characters.* FROM moves JOIN characters_moves ON (moves.id = characters_moves.move_id) JOIN characters ON (characters_moves.character_id = characters.id) WHERE moves.id = @MoveId;", conn);
            SqlParameter characterMove = new SqlParameter("@MoveId", this.GetMoveId().ToString());
            cmd.Parameters.Add(characterMove);
            SqlDataReader rdr = cmd.ExecuteReader();

            List<Character> newList = new List<Character>{};
            while(rdr.Read())
            {
                int characterId = rdr.GetInt32(0);
                string characterType = rdr.GetString(1);
                string characterName = rdr.GetString(2);
                int characterHealth = rdr.GetInt32(3);
                int characterAttack = rdr.GetInt32(4);
                int characterSpeed = rdr.GetInt32(5);

                Character newCharacter = new Character(characterType, characterName, characterHealth, characterAttack, characterSpeed, characterId);
                newList.Add(newCharacter);
            }
            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
             return newList;
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
