using BattleCottage.Services.LFGPosts;

namespace BattleCottage.Web.Dtos
{
    public class LFGPostOptionsDto
    {
        public LFGPostOptionsDto(
            IList<GameModeDto> gameModes,
            IList<GameStyleDto> gameStyles,
            IList<GameRoleDto> gameRoles,
            IList<DurationInMinutesDto> durationsInMinutes
        )
        {
            GameModes = gameModes;
            GameStyles = gameStyles;
            GameRoles = gameRoles;
            DurationsInMinutes = durationsInMinutes;
        }

        public IList<GameModeDto> GameModes { get; set; }
        public IList<GameStyleDto> GameStyles { get; set; }
        public IList<GameRoleDto> GameRoles { get; set; }
        public IList<DurationInMinutesDto> DurationsInMinutes { get; set; }

        public static LFGPostOptionsDto FromLFGPostFormOptions(LFGPostFormOptions options)
        {
            return new LFGPostOptionsDto(
                options.GameModes?.Select(gm => new GameModeDto(gm.Id, gm.Name)).ToList() ?? new List<GameModeDto>(),
                options.GameStyles?.Select(gs => new GameStyleDto(gs.Id, gs.Name)).ToList() ?? new List<GameStyleDto>(),
                options.GameRoles?.Select(gr => new GameRoleDto(gr.Id, gr.Name)).ToList() ?? new List<GameRoleDto>(),
                options.LFGPostDurations
                    ?.Select(d => new DurationInMinutesDto(d.Id, d.Name, d.DurationInMinutes))
                    .ToList() ?? new List<DurationInMinutesDto>()
            );
        }
    }
}
