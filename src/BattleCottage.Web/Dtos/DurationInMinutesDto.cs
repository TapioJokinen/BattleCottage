namespace BattleCottage.Web.Dtos
{
    public class DurationInMinutesDto
    {
        public DurationInMinutesDto(int id, string name, int durationInMinutes)
        {
            Id = id;
            DurationInMinutes = durationInMinutes;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int DurationInMinutes { get; set; }
    }
}
