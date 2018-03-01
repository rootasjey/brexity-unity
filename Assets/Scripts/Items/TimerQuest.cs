using UnityEngine;
using UnityEngine.EventSystems;

public class TimerQuest : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    //private GameObject _popup;
    private Stage2Orchestrator _orchestrator;

    public void OnPointerEnter(PointerEventData eventData) {
        _orchestrator.TimerPopup.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        _orchestrator.TimerPopup.SetActive(false);
    }

    // Use this for initialization
    void Start() {
        _orchestrator = GameObject.Find("Orchestrator")
            .GetComponent<Stage2Orchestrator>();
    }
}
