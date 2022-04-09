using csm6.Data;
using csm6.Models;
using csm6.ViewModels;

namespace csm6.Services
{
    public class RandomServices
    {
        private static readonly CSMContext _context = new CSMContext();

        public static Team CreateTeam(MemberViewModel memberVM)
        {
            var team = new Team();
            team.Id = team.Id;
            team.TeamName = "Team Test";
            team.MemberId = memberVM.Id;
            // team.LeagueId = 1;

            return team;
        }
    }
}
