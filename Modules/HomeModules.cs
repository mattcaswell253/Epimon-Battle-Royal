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
            // takes you to homepage
            Get["/"] = _ => {
                return View["index.cshtml"];
            };
            // takes you to character select
            Get["/character-select"] = _ => {
                List<Character> allCharacters = Character.GetAllCharacters();
                return View["character-select.cshtml", allCharacters];
            };
            // after character select, takes you to arena1 for player 1's first attack
            Post["/arena"] = _ => {
                Dictionary<string, object> model = new Dictionary<string, object>{};
                Character character1 = Character.Find(Request.Form["character1-id"]);
                Character character2 = Character.Find(Request.Form["character2-id"]);
                List<Move> character1Moves = character1.GetMoves();
                Character.player1 = Character.Find(Request.Form["character1-id"]);
                Character.player2 = Character.Find(Request.Form["character2-id"]);
                int health1 = Character.player1.GetHealth();
                int health2 = Character.player2.GetHealth();
                model.Add("p1health", health1);
                model.Add("p2health", health2);
                model.Add("character1", character1);
                model.Add("character2", character2);
                model.Add("character1Moves", character1Moves);
                if (character1.GetSpeed() > character2.GetSpeed())
                {
                    return View["arena1.cshtml", model];
                }
                else
                {
                    return View["arena2.cshtml", model];
                }
            };
            // after player 1 attacks, takes you to arena2 for player 2's attack
            Post["/attack1"] = _ => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Character character1 = Character.Find(Request.Form["character1-id"]);
                Character character2 = Character.Find(Request.Form["character2-id"]);
                List<Move> character1Moves = character1.GetMoves();
                Character.Attack2(Move.Find(Request.Form["character1Attack"]));
                int health1 = Character.player1.GetHealth();
                int health2 = Character.player2.GetHealth();
                model.Add("p1health", health1);
                model.Add("p2health", health2);
                model.Add("character1", character1);
                model.Add("character2", character2);
                model.Add("character1Moves", character1Moves);
                if(health2 > 0)
                {
                    return View["arena2.cshtml", model];
                }
                else
                {
                    return View["game_over.cshtml"];
                }
            };
            // after player 2 attacks, takes you to arena1 for player 1's attack
            Post["/attack2"] = _ => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Character character1 = Character.Find(Request.Form["character1-id"]);
                Character character2 = Character.Find(Request.Form["character2-id"]);
                List<Move> character2Moves = character2.GetMoves();
                Character.Attack1(Move.Find(Request.Form["character2Attack"]));
                int health1 = Character.player1.GetHealth();
                int health2 = Character.player2.GetHealth();
                model.Add("p1health", health1);
                model.Add("p2health", health2);
                model.Add("character1", character1);
                model.Add("character2", character2);
                model.Add("character2Moves", character2Moves);
                if(health1 > 0)
                {
                    return View["arena1.cshtml", model];
                }
                else
                {
                    return View["game_over2.cshtml", model];
                }
            };
        }
    }
}
