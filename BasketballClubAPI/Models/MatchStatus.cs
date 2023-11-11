namespace BasketballClubAPI.Models {
    public enum MatchStatus {
        [EnumStringValue("not started")]
        NotStarted,

        [EnumStringValue("in process")]
        InProcess,

        [EnumStringValue("finished")]
        Finished
    }
}
