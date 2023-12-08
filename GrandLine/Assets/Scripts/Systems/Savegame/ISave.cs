namespace GrandLine.Systems.Savegame
{
    public interface ISave
    {
        public object Save();
        public void Load(object data);
    }
}