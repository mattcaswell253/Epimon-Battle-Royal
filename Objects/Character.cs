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

        public Character(string Type, string Name, int Health, int Attack, int Speed, int Id = 0)
        {
            _id = Id;
            _type = Type;
            _name = Name;
            _health = Health;
            _attack = Attack;
            _speed = Speed;
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
        public string GetType()
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

        public static List<Character> GetAll()
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

                Character newCharacter = new Character(characterType, characterName, characterHealth, characterHealth, characterSpeed, characterId);
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

            SqlCommand cmd = new SqlCommand("INSERT INTO characters(type, name, health, attack, speed) OUTPUT INSERTED.id VALUES (@CharacterType, @CharacterName, @CharacterHealth, @CharacterAttack, @CharacterSpeed)", conn);

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

            while(rdr.Read())
            {
                foundCharacterId = rdr.GetInt32(0);
                foundCharacterType = rdr.GetString(1);
                foundCharacterName = rdr.GetString(2);
                foundCharacterHealth = rdr.GetInt32(3);
                foundCharacterAttack = rdr.GetInt32(4);
                foundCharacterSpeed = rdr.GetInt32(5);
            }

            Character foundCharacter = new Character(foundCharacterType, foundCharacterName, foundCharacterHealth, foundCharacterAttack, foundCharacterSpeed, foundCharacterId);

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
