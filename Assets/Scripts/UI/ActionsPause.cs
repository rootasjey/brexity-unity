using Assets.Scripts.Stages;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionsPause : MonoBehaviour {
    private Stage2Orchestrator _orchestrator;

    // Use this for initialization
    void Start () {
        _orchestrator = GameObject.Find("Orchestrator")
                        .GetComponent<Stage2Orchestrator>();
    }

    public void Restart() {
        //GameManager.instance.RestartScene();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Story.Instance.GetComponent<Stage2Quest>().ResetQuest();
    }

    public void Resume() {
        _orchestrator.ResumeGame();
    }

    public void ReturnToScreenTitle() {
        SceneManager.LoadScene("ScreenTitle");
    }

    public void GoToSettings() {
        Menus.Instance.Settings.SetActive(true);
        Menus.Instance.Pause.SetActive(true);
    }

    public void ExitGame() {
        Application.Quit();
    }
}
