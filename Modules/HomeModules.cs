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
                // Dictionary<string, object> model = new Dictionary<string, object>();
                List<Character> allCharacters = Character.GetAll();
                // List<Move> allMoves = Move.GetAllMoves();
                // model.Add("characters", allCharacters);
                // model.Add("moves", allMoves);
                return View["character-select.cshtml", allCharacters];
            };
            Post["/character-selected"] = _ => {
                Dictionary<string, object> model = new Dictionary<string, object>{};
                Character character1 = Character.Find(Request.Form["character1-id"]);
                Character character2 = Character.Find(Request.Form["character2-id"]);
                List<Move> character1Moves = character1.GetMoves();
                List<Move> character2Moves = character2.GetMoves();
                Character character1Attacked = Character.Find(Request.Form["character1-id"]);
                Character character2Attacked = Character.Find(Request.Form["character2-id"]);
                model.Add("character1", character1);
                model.Add("character2", character2);
                model.Add("character1Moves", character1Moves);
                model.Add("character2Moves", character2Moves);
                model.Add("character1Attacked", character1Attacked);
                model.Add("character2Attacked", character2Attacked);
                Console.WriteLine(model["character1Moves"]);
                return View["arena.cshtml", model];
            };
            Post["/attack"] = _ => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                Character character1 = Character.Find(Request.Form["character1-id"]);
                Character character2 = Character.Find(Request.Form["character2-id"]);
                List<Move> character1Moves = character1.GetMoves();
                List<Move> character2Moves = character2.GetMoves();
                Character character1Attacked = character1.Attack(Move.Find(Request.Form["character2Attack"]));
                Character character2Attacked = character2.Attack(Move.Find(Request.Form["character1Attack"]));
                model.Add("character1", character1);
                model.Add("character2", character2);
                model.Add("character1Moves", character1Moves);
                model.Add("character2Moves", character2Moves);
                model.Add("character1Attacked", character1Attacked);
                model.Add("character2Attacked", character2Attacked);
                return View["arena.cshtml", model];
            };



            // Post["/attack/{}"] = _ => {
            //     Band band = Band.Find(Request.Form["band-id"]);
            //     Venue venue = Venue.Find(Request.Form["venue-id"]);
            //     band.AddVenue(venue);
            //     return View["success.cshtml"];
        }
    }
}
