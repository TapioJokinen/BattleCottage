namespace BattleCottage.Services.LFGPosts

{
    public class LFGPostFormInput
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Duration { get; set; }
        public string? GameId { get; set; }
        public int GameModeId { get; set; }
        public int GameStyleId { get; set; }
        public string[]? Roles { get; set; }
    }
}