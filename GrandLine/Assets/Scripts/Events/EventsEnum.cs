namespace GrandLine.Events
{
    public enum EventTypes
    {
        QuestLoad,
        QuestAccepted,
        QuestCompleted,
        QuestRejected,
        QuestFailed,

        ResourceChanged,

        EncounterStarted,
        EncounterCompleted,

        UiQuestLoad,
        UiQuestAccepted,

        DaytimeHour,
        DaytimeDusk,
        DaytimeDawn,
        DaytimeDay,
        DaytimeNight,
    }
}