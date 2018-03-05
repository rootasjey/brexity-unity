using Assets.Scripts.Persistent;
using UnityEngine;

namespace Assets.Scripts.Stages {
    public class Loader : MonoBehaviour {
        public HUD hud;
        public Menus ui;
        public Story quest;
        public Fonts fonts;
        public Cinematic cinematic;

        private void Awake() {
            if (HUD.Instance == null) {
                Instantiate(hud);
            }

            if (Menus.Instance == null) {
                Instantiate(ui);
            } else {
                Menus.Instance.Revive();
            }

            if (Story.Instance == null) {
                Instantiate(quest);
            }

            if (Cinematic.Instance == null) {
                Instantiate(cinematic);
            }

            if (Fonts.Instance == null) {
                Instantiate(fonts);
            }

            Story.Instance.ResetTimer();
        }
    }
}
