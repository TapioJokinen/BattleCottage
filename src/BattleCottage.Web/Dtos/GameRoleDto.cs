namespace BattleCottage.Web.Dtos
{
    public class GameRoleDto
    {
        public GameRoleDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
