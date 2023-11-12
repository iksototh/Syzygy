using GrandLine.Triggers;
using GrandLine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GrandLine.Towns
{
    public class TownController : MonoBehaviour, ITrigger
    {
        void Start()
        {

        }

        public void OnTrigger()
        {
            var questDialog = Game.GameManager.QuestDialog.GetComponent<QuestDialog>();
            questDialog.AcceptAction = SpawnShark;
            Game.GameManager.QuestDialog.gameObject.SetActive(true);


            //var confirmMenu = Game.GameManager.ConfirmMenu.GetComponent<ConfirmMenu>();
            //confirmMenu.EnterBtn.onClick.AddListener(OnEnter);
            //confirmMenu.Show();
        }

        private void SpawnShark()
        {
            Debug.Log("Spawn shark");
            var townCell = Game.WorldMap.WorldToCell(gameObject.transform.position);
            var x = Random.Range(1, 10) + 0.5f;
            var y = Random.Range(1, 10) + 0.5f;
            x = Random.Range(0, 1) == 0 ? -x : x;
            y = Random.Range(0, 1) == 0 ? -y : y;
            
            Instantiate(Game.GameManager._sharkPrefab, new Vector3(x + townCell.x, y + townCell.y), Quaternion.identity);
        }

        private void OnEnter()
        {
            Debug.Log("Enter");
            Game.SavegameManager.Save();
            SceneManager.LoadScene("Town");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Trigger town");
        }
    }
}