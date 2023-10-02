using BattleCottage.Core.Entities;

namespace BattleCottage.Web.Dtos
{

    public class LfgPostDto
    {
        public class GameRoleDto
        {
            public GameRoleDto(LfgPostGameRole lfgPostGameRole)
            {
                GameRoleId = lfgPostGameRole.GameRoleId;
                GameRole = lfgPostGameRole.GameRole.Name;
            }

            public int GameRoleId { get; set; }
            public string? GameRole { get; set; } = null!;
        }

        public LfgPostDto(LfgPost lfgPost)
        {
            Id = lfgPost.Id;
            DateAdded = lfgPost.DateAdded;
            Description = lfgPost.Description;
            DurationInMinutes = lfgPost.DurationInMinutes;
            GameId = lfgPost.GameId;
            GameName = lfgPost.Game.Name;
            GameModeId = lfgPost.GameModeId;
            GameMode = lfgPost.GameMode.Name;
            GameStyleId = lfgPost.GameStyleId;
            GameStyle = lfgPost.GameStyle.Name;
            Title = lfgPost.Title;
            UserId = lfgPost.UserId;
            UserEmail = lfgPost.User.Email;
            GameRoles = lfgPost.LfgPostGameRoles != null ? lfgPost.LfgPostGameRoles.Select(x => new GameRoleDto(x)).ToList() : new List<GameRoleDto>();
        }

        public int Id { get; set; }
        public DateTime DateAdded { get; set; }
        public string Description { get; set; }
        public int DurationInMinutes { get; set; }
        public int GameId { get; set; }
        public string GameName { get; set; }
        public int GameModeId { get; set; }
        public string GameMode { get; set; }
        public int GameStyleId { get; set; }
        public string GameStyle { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public IList<GameRoleDto> GameRoles { get; set; }
    }
}