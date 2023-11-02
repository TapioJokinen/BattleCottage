namespace BattleCottage.Web.Dtos
{
    public class GameDto
    {
        public GameDto(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
