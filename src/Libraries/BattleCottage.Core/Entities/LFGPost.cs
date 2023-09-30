namespace BattleCottage.Core.Entities
{
    public class LFGPost : BaseEntity
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required int DurationInMinutes { get; set; }
        public required int GameModeId { get; set; }
        public required GameMode GameMode { get; set; }
        public required int GameStyleId { get; set; }
        public required GameStyle GameStyle { get; set; }
        public required int GameId { get; set; }
        public required Game Game { get; set; }
    }
}