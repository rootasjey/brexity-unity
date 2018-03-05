using UnityEngine;

namespace Assets.Scripts.Persistent {
    public class Fonts : MonoBehaviour {
        /* PRIVATE PROPS */
        private static Fonts _instance;

        /* PUBLIC PROPS */
        public static Fonts Instance { get { return _instance; } }

        private string Name { get { return "Fonts"; } }

        public Font[] fonts;
        
        private void Awake() {
            if (Instance == null) {
                _instance = this;

            } else if (Instance != this) {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);

            Instance.name = Name;
        }

        public static Font GetFont(string name) {
            foreach (var font in Instance.fonts) {
                if (font.name == name)
                    return font;
            }

            return null;
        }
    }
}
