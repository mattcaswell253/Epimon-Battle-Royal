using System.Collections.Generic;
using Nancy;
using System;
using Nancy.ViewEngines.Razor;

namespace Epimon
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => {
                return View["index.cshtml"];
            };

            Get["/character-select"] = _ => {
                List<Character> allCharacters = Character.GetAllCharacters();
                return View["character-select.cshtml", allCharacters];
            };
            Post["/character-selected"] = _ => {
                Dictionary<string, object> model = new Dictionary<string, object>{};
                Character character1 = Character.Find(Request.Form["character1-id"]);
                Character character2 = Character.Find(Request.Form["character2-id"]);
                List<Move> character1Moves = character1.GetMoves();
                List<Move> character2Moves = character2.GetMoves();
                Character.player1 = Character.Find(Request.Form["character1-id"]);
                Character.player2 = Character.Find(Request.Form["character2-id"]);
                int health1 = Character.player1.GetHealth();
                int health2 = Character.player2.GetHealth();
                model.Add("p1health", health1);
                model.Add("p2health", health2);
                model.Add("character1", character1);
                model.Add("character2", character2);
                model.Add("character1Moves", character1Moves);
                model.Add("character2Moves", character2Moves);
                return View["arena.cshtml", model];
            };
            Post["/attack1"] = _ => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Character character1 = Character.Find(Request.Form["character1-id"]);
                Character character2 = Character.Find(Request.Form["character2-id"]);
                List<Move> character1Moves = character1.GetMoves();
                // List<Move> character2Moves = character2.GetMoves();
                int health1 = Character.player1.GetHealth();
                int health2 = Character.player2.GetHealth();
                Character.player2.Attack2(Move.Find(Request.Form["character1Attack"]));
                model.Add("p1health", health1);
                model.Add("p2health", health2);
                model.Add("character1", character1);
                model.Add("character2", character2);
                model.Add("character1Moves", character1Moves);
                // model.Add("character2Moves", character2Moves);
                return View["arena.cshtml", model];
            };
            Post["/attack2"] = _ => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Character character1 = Character.Find(Request.Form["character1-id"]);
                Character character2 = Character.Find(Request.Form["character2-id"]);
                // List<Move> character1Moves = character1.GetMoves();
                List<Move> character2Moves = character2.GetMoves();
                int health1 = Character.player1.GetHealth();
                int health2 = Character.player2.GetHealth();
                Character.player1.Attack1(Move.Find(Request.Form["character2Attack"]));
                model.Add("p1health", health1);
                model.Add("p2health", health2);
                model.Add("character1", character1);
                model.Add("character2", character2);
                // model.Add("character1Moves", character1Moves);
                model.Add("character2Moves", character2Moves);
                return View["arena.cshtml", model];
            };




        }
    }
}
