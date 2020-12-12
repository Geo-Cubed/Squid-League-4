namespace CubedApi.Models.DatabaseTables
{
    public class GameSetting
    {
        public int? Id { get; set; }

        public int? GameMapId { get; set; }

        public int? GameModeId { get; set; }

        public string BracketStage { get; set; }

        public int? SortOrder { get; set; }
    }
}
