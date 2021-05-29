namespace GeoCubed.SquidLeague4.Application.Features.GameSettings.Queries.GetGameSettingsForAdmin
{
    public class GameSettingAdminVm
    {
        public int Id { get; set; }

        public int GameMapId { get; set; }

        public int GameModeId { get; set; }

        public string BracketStage { get; set; }

        public int SortOrder { get; set; }
    }
}