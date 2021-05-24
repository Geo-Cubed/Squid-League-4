namespace GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetGamesByTeamId
{
    public class GameSettingsDto
    {
        public int SortOrder { get; set; }

        public string ModeName { get; set; }

        public string ModePicture { get; set; }

        public string MapName { get; set; }

        public string MapPicture { get; set; }
    }
}