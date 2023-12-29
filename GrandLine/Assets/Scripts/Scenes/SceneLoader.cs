using UnityEngine.SceneManagement;

namespace GrandLine.Scenes
{
    public static class SceneLoader
    {
        public static void LoadHideout(bool save = true)
        {
            LoadScene("Hideout", save);
        }

        public static void LoadWorld(bool save = true)
        {
            LoadScene("World", save);
        }

        public static void LoadMainMenu(bool save = true)
        {
            LoadScene("Start", save);
        }

        private static void LoadScene(string name, bool save = false)
        {
            Game.Instance.SavegameManager.Save();
            SceneManager.LoadScene(name);
        }
    }
}