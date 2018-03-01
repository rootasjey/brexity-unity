using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionsScreenTitle : MonoBehaviour {
    private ScreenTitleOrchestrator _orchestrator;

    private void Start() {
        _orchestrator = GameObject.Find("Orchestrator")
                        .GetComponent<ScreenTitleOrchestrator>();
    }
    
    // Use this for initialization
    // BUTTONS CLICK CALLBACKS
    // -----------------------
    public void StartGame() {
        SceneManager.LoadScene("Stage2_town");
    }

    public void GoToSettings() {
        _orchestrator.GetMainMenu().SetActive(false);
        _orchestrator.GetSettingsMenu().SetActive(true);
    }

    public void ExitGame() {
        Application.Quit();
    }
}
