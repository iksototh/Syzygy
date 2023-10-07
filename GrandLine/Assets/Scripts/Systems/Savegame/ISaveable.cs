namespace GrandLine.Systems.Savegame
{
    interface ISaveable
    {
        SaveState OnSave();
    }
}