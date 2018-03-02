using UnityEngine;

namespace Assets.Scripts.Stages {
    public class Loader : MonoBehaviour {
        public GameObject gameManager;

        private void Awake() {
            if (GameManager.instance == null) {
                Instantiate(gameManager);
            }
        }
    }
}
