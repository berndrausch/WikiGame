using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace WikiGame.Logic
{
    public class Game
    {
        private readonly string _start;
        private readonly string _goal;
        private readonly List<string> _preferred;


        public Game(string start, string goal, IEnumerable<string> preferred)
        {
            _start = start;
            _goal = goal.ToLower();
            _preferred = preferred.Select(url => url.ToLower()).ToList();
        }

        public IEnumerable<string> FindWay(int maxHops)
        {
            List<string> way = new List<string>();
            HtmlHandling htmlHandling = new HtmlHandling();

            string currentUrl = _start;

            for (int i = 0; i < maxHops; i++)
            {
                currentUrl = htmlHandling.GetFullUrl(currentUrl);

                var web = new HtmlWeb();
                var doc = web.Load(currentUrl);

                way.Add($"{htmlHandling.GetTitle(doc)} ({currentUrl})");

                var links = htmlHandling.GetAllLinks(doc).ToList();

                if (links.Any(IsGoal))
                {
                    way.Add($"Page contains link to {_goal}");
                    return way;
                }

                currentUrl = links.FirstOrDefault(IsPreferred);
                if (string.IsNullOrWhiteSpace(currentUrl))
                {
                    // just use the link in the middle
                    currentUrl = links[links.Count / 2];
                }
            }

            way.Add($"Giving up. Way to {_goal} not found within {maxHops} Links");
            return way;
        }

        private bool IsGoal(string url)
        {
            return url.ToLower().EndsWith(_goal);
        }

        private bool IsPreferred(string url)
        {
            foreach (string preferred in _preferred)
            {
                if (url.ToLower().EndsWith(preferred))
                {
                    return true;
                }
            }

            return false;
        }

    }


}
