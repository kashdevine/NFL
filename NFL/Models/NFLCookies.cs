using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFL.Models
{
    public class NFLCookies
    {
        private const string TeamsKey = "myteams";
        private const string Delimiter = "-";

        private IRequestCookieCollection requestCookies { get; set; }
        private IResponseCookies responseCookies { get; set; }

        public NFLCookies(IRequestCookieCollection cookies)
        {
            requestCookies = cookies;
        }

        public NFLCookies(IResponseCookies cookies)
        {
            responseCookies = cookies;
        }

        public void SetMyTeamIDs(List<Team> myTeams)
        {
            List<string> ids = myTeams.Select(t => t.TeamID).ToList();
            string idsString = String.Join(Delimiter, ids);
            CookieOptions options = new CookieOptions { Expires = DateTime.Now.AddDays(30) };
            RemoveMyTeamsIDs();
            responseCookies.Append(TeamsKey, idsString, options);
        }

        public string[] GetMyTeamsIDs() 
        {
            string cookies = requestCookies[TeamsKey];

            if (string.IsNullOrEmpty(cookies))
                return new string[] { };
            else
                return cookies.Split(Delimiter);
        }

        public void RemoveMyTeamsIDs()
        {
            responseCookies.Delete(TeamsKey);
        }
    }
}
