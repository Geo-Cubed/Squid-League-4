namespace CubedApi.Api.Models.DTOs
{
    public class GameSettingDto
    {
        public int Id { get; set; }

        public int GameMapId { get; set; }

        public string BracketStage { get; set; }

        public int SortOrder { get; set; }
    }
}
