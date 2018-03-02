using UnityEngine;

namespace Assets.Scripts.Stages {
    public class GameManager : MonoBehaviour {
        public static GameManager instance = null;
        //public Stage2Orchestrator Orchestrator;
        public GameObject UIPrefab;
        public GameObject HUDPrefab;

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
                var ui = Instantiate(UIPrefab);
                ui.name = "UI";
            }

            if (GameObject.Find("HUD") == null) {
                var hud = Instantiate(HUDPrefab);
                hud.name = "HUD";

                var world = GameObject.Find("GamePlay");
                if (world != null) hud.transform.parent = world.transform;
            }
        }
    }
}
