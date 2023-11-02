namespace BattleCottage.Web.Dtos
{
    public class GameModeDto
    {
        public GameModeDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
