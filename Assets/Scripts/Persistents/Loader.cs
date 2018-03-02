using UnityEngine;

namespace Assets.Scripts.Stages {
    public class Loader : MonoBehaviour {
        public GameObject gameManager;

        public PersistentHUD hud;
        public PersistentUI ui;
        public PersistentQuest quest;

        private void Awake() {
            //if (GameManager.instance == null) {
            //    Instantiate(gameManager);
            //}

            if (PersistentHUD.instance == null) {
                Instantiate(hud);
            }

            if (PersistentUI.instance == null) {
                Instantiate(ui);
            } else {
                PersistentUI.instance.Revive();
            }

            if (PersistentQuest.instance == null) {
                Instantiate(quest);
            }

            PersistentQuest.instance.ResetTimer();
        }
    }
}
