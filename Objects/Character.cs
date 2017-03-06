// using System.Collections.Generic;
// using System.Data.SqlClient;
// using System;
//
// namespace Epimon
// {
//     public class Character
//     {
//         private int _id;
//         private string _type;
//         private string _name;
//         private int _health;
//         private int _attack;
//         private int _speed;
//
//         public Character(string Type, string Name, int Health, int Attack, int Speed, int Id = 0)
//         {
//             _id = Id;
//             _type = Type;
//             _name = Name;
//             _health = Health;
//             _attack = Attack;
//             _speed = Speed;
//         }
//         public override bool Equals(System.Object otherCharacter)
//         {
//             if(!(otherCharacter is Character))
//             {
//                 return false;
//             }
//             else
//             {
//                 Character newCharacter = (Character) otherCharacter;
//                 bool idEquality = this.GetId() == newCharacter.GetId();
//                 bool typeEquality = this.GetType() == newCharacter.GetType();
//                 bool nameEquality = this.GetName() == newCharacter.GetName();
//                 bool healthEquality = this.GetHealth() == newCharacter.GetHealth();
//                 bool nameEquality = this.GetName() == newCharacter.GetName();
//                 bool nameEquality = this.GetName() == newCharacter.GetName();
//                 return (idEquality && nameEquality);
//             }
//         }
