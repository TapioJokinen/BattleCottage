using BattleCottage.Core.Entities;

namespace BattleCottage.Web.Dtos
{

    public class LFGPostDto
    {
        public class GameRoleDto
        {
            public GameRoleDto(LFGPostGameRole LFGPostGameRole)
            {
                GameRoleId = LFGPostGameRole.GameRoleId;
                GameRole = LFGPostGameRole.GameRole.Name;
            }

            public int GameRoleId { get; set; }
            public string? GameRole { get; set; } = null!;
        }

        public LFGPostDto(LFGPost LFGPost)
        {
            Id = LFGPost.Id;
            DateAdded = LFGPost.DateAdded;
            Description = LFGPost.Description;
            DurationInMinutes = LFGPost.DurationInMinutes;
            GameId = LFGPost.GameId;
            GameName = LFGPost.Game.Name;
            GameModeId = LFGPost.GameModeId;
            GameMode = LFGPost.GameMode.Name;
            GameStyleId = LFGPost.GameStyleId;
            GameStyle = LFGPost.GameStyle.Name;
            Title = LFGPost.Title;
            UserId = LFGPost.UserId;
            GameRoles = LFGPost.LFGPostGameRoles != null ? LFGPost.LFGPostGameRoles.Select(x => new GameRoleDto(x)).ToList() : new List<GameRoleDto>();
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
        public IList<GameRoleDto> GameRoles { get; set; }
    }
}