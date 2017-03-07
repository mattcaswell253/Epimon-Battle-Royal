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

            Get["/arena"] = _ => {
                Dictionary<string, object> model = new Dictionary<string, object>();
                List<Character> allCharacters = Character.GetAllCharacters();
                List<Move> allMoves = Move.GetAllMoves();
                model.Add("characters", allCharacters);
                model.Add("moves", allMoves);
                return View["arena.cshtml", model];
            };
        }
    }
}
