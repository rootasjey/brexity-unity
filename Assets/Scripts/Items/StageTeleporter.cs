using UnityEngine.SceneManagement;

namespace Assets.Scripts.Items {
    public class StageTeleporter : Interactable {
        public string stageDestination;

        public override void Interact() {
            SceneManager.LoadScene(stageDestination);
        }
    }
}
