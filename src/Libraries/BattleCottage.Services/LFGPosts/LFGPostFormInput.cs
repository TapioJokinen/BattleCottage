namespace BattleCottage.Services.LfgPosts

{
    public class LfgPostFormInput
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Duration { get; set; }
        public int GameId { get; set; }
        public int GameModeId { get; set; }
        public int GameStyleId { get; set; }
        public int[]? GameRoleIds { get; set; }
    }
}