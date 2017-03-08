
using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Epimon
{
    public class Character
    {
        private int _id;
        private string _type;
        private string _name;
        private int _health;
        private int _attack;
        private int _speed;
        private string _img;


        public static Character player1;
        public static Character player2;


        public Character(string Type, string Name, int Health, int Attack, int Speed, string img, int Id = 0)
        {
            _id = Id;
            _type = Type;
            _name = Name;
            _health = Health;
            _attack = Attack;
            _speed = Speed;
            _img = img;
        }
        public override bool Equals(System.Object otherCharacter)
        {
            if(!(otherCharacter is Character))
            {
                return false;
            }
            else
            {
                Character newCharacter = (Character) otherCharacter;
                bool idEquality = this.GetId() == newCharacter.GetId();
                bool typeEquality = this.GetType() == newCharacter.GetType();
                bool nameEquality = this.GetName() == newCharacter.GetName();
                bool healthEquality = this.GetHealth() == newCharacter.GetHealth();
                bool attackEquality = this.GetAttack() == newCharacter.GetAttack();
                bool speedEquality = this.GetSpeed() == newCharacter.GetSpeed();
                return (idEquality && typeEquality && nameEquality && healthEquality && attackEquality && speedEquality);
            }
        }
        public int GetId()
        {
            return _id;
        }
        public string GetCharType()
        {
            return _type;
        }
        public void SetType(string newType)
        {
            _type = newType;
        }
        public string GetName()
        {
            return _name;
        }
        public string GetImg()
        {
            return _img;
        }
        public void SetName(string newName)
        {
            _name = newName;
        }
        public int GetHealth()
        {
            return _health;
        }
        public void SetHealth(int newHealth)
        {
            _health = newHealth;
        }
        public int GetAttack()
        {
            return _attack;
        }
        public void SetAttack(int newAttack)
        {
            _attack = newAttack;
        }
        public int GetSpeed()
        {
            return _speed;
        }
        public void SetSpeed(int newSpeed)
        {
            _speed = newSpeed;
        }

        public static List<Character> GetAllCharacters()
        {
            List<Character> allCharacters = new List<Character> {};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM characters;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int characterId = rdr.GetInt32(0);
                string characterType = rdr.GetString(1);
                string characterName = rdr.GetString(2);
                int characterHealth = rdr.GetInt32(3);
                int characterAttack = rdr.GetInt32(4);
                int characterSpeed = rdr.GetInt32(5);
                string characterImg = rdr.GetString(6);

                Character newCharacter = new Character(characterType, characterName, characterHealth, characterAttack, characterSpeed, characterImg, characterId);

                allCharacters.Add(newCharacter);
            }

            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return allCharacters;

        }
        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO characters(type, name, health, attack, speed, img) OUTPUT INSERTED.id VALUES (@CharacterType, @CharacterName, @CharacterHealth, @CharacterAttack, @CharacterSpeed, @CharacterImg)", conn);

            SqlParameter typeParameter = new SqlParameter("@CharacterType", this.GetType());
            cmd.Parameters.Add(typeParameter);

            SqlParameter nameParameter = new SqlParameter("@CharacterName", this.GetName());
            cmd.Parameters.Add(nameParameter);

            SqlParameter healthParameter = new SqlParameter("@CharacterHealth", this.GetHealth());
            cmd.Parameters.Add(healthParameter);

            SqlParameter attackParameter = new SqlParameter("@CharacterAttack", this.GetAttack());
            cmd.Parameters.Add(attackParameter);

            SqlParameter speedParameter = new SqlParameter("@CharacterSpeed", this.GetSpeed());
            cmd.Parameters.Add(speedParameter);

            SqlParameter imgParameter = new SqlParameter("@CharacterImg", this.GetImg());
            cmd.Parameters.Add(imgParameter);

            SqlDataReader rdr = cmd.ExecuteReader();


            while(rdr.Read())
            {
                this._id = rdr.GetInt32(0);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
        }
        public static Character Find(int id)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM characters WHERE id = @CharacterId", conn);

            SqlParameter characterIdParameter = new SqlParameter("@CharacterId", id.ToString());
            cmd.Parameters.Add(characterIdParameter);

            SqlDataReader rdr = cmd.ExecuteReader();

            int foundCharacterId = 0;
            string foundCharacterType = null;
            string foundCharacterName = null;
            int foundCharacterHealth = 0;
            int foundCharacterAttack = 0;
            int foundCharacterSpeed = 0;
            string foundCharacterImg = null;

            while(rdr.Read())
            {
                foundCharacterId = rdr.GetInt32(0);
                foundCharacterType = rdr.GetString(1);
                foundCharacterName = rdr.GetString(2);
                foundCharacterHealth = rdr.GetInt32(3);
                foundCharacterAttack = rdr.GetInt32(4);
                foundCharacterSpeed = rdr.GetInt32(5);
                foundCharacterImg = rdr.GetString(6);
            }

            Character foundCharacter = new Character(foundCharacterType, foundCharacterName, foundCharacterHealth, foundCharacterAttack, foundCharacterSpeed, foundCharacterImg, foundCharacterId);

            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return foundCharacter;
        }
        public void AddMove(Move newMove)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();


            SqlCommand cmd = new SqlCommand("INSERT INTO characters_moves (character_id, move_id) VALUES (@CharacterId, @MoveId)", conn);

            SqlParameter characterIdParameter = new SqlParameter("@CharacterId", this.GetId());
            SqlParameter moveIdParameter = new SqlParameter("@MoveId", newMove.GetMoveId());

            cmd.Parameters.Add(characterIdParameter);
            cmd.Parameters.Add(moveIdParameter);

            cmd.ExecuteNonQuery();

            if (conn != null);
            {
                conn.Close();
            }

        }
        public List<Move> GetMoves()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT moves.* FROM characters Join characters_moves ON (characters.id = characters_moves.character_id) JOIN moves ON (characters_moves.move_id = moves.id) WHERE characters.id = @CharacterId;", conn);

            SqlParameter characterIdParameter = new SqlParameter("@CharacterId", this.GetId().ToString());

            cmd.Parameters.Add(characterIdParameter);

            List<Move> moveList = new List<Move> {};

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int moveId = rdr.GetInt32(0);
                string moveName = rdr.GetString(1);
                string moveType = rdr.GetString(2);
                int moveDmg = rdr.GetInt32(3);

                Move newMove = new Move(moveName, moveType, moveDmg, moveId);
                moveList.Add(newMove);
            }
            if (rdr != null)
            {
                rdr.Close();
            }
            if (conn != null)
            {
                conn.Close();
            }
            return moveList;
        }
        public static void Attack1(Move attackMove)
        {
            player1._health -= attackMove.GetMoveDmg();
        }
        public static void Attack2(Move attackMove)
        {
            player2._health -= attackMove.GetMoveDmg();
        }
        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM characters;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
