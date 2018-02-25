using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Items {
    public class StageTeleporter : Interactable {

        public override void Interact() {
            SceneManager.LoadScene("Stage2_sewer");
        }
    }
}
