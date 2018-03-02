using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Stages {
    public class GameManager : MonoBehaviour {
        public static GameManager instance = null;
        
        public GameObject UIPrefab;
        public GameObject HUDPrefab;
        public GameObject QuestPrefab;

        public GameObject _ui { get; set; }
        public GameObject _hud { get; set; }
        public GameObject _quest { get; set; }

        private void Awake() {
            if (instance == null) {
                instance = this;

            } else if (instance != this) {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
            //Orchestrator = GameObject.Find("Orchestrator").GetComponent<Stage2Orchestrator>();

            InitGame();
        }

        private void InitGame() {
            if (GameObject.Find("UI") == null) {
                //var ui = Instantiate(UIPrefab);
                _ui = Instantiate(UIPrefab);
                _ui.name = "UI";
            }

            if (GameObject.Find("HUD") == null) {
                //var hud = Instantiate(HUDPrefab);
                _hud = Instantiate(HUDPrefab);
                _hud.name = "HUD";

                var world = GameObject.Find("GamePlay");
                if (world != null) _hud.transform.SetParent(world.transform);
            }

            if (GameObject.Find("Quest") == null) {
                //var quest = Instantiate(QuestPrefab);
                _quest = Instantiate(QuestPrefab);
                _quest.name = "Quest";
            }
        }

        public void RestartScene() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            AttachToScene();
        }

        public void AttachToScene() {
            var world = GameObject.Find("GamePlay");
            _hud.transform.SetParent(world.transform);

            _ui.transform.SetParent(world.transform);
            _quest.transform.SetParent(world.transform);
        }
    }
}
