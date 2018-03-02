using UnityEngine;

public class HoboBehavior : Interactable {
    private GUIContent _hintSkipText;
    private GUIStyle _fontStyle;

    private float _timeLastMessage = 5f;

    private bool _hasInteracted { get; set; }

    public override void Interact() {
        _hasInteracted = true;

        _hintSkipText = new GUIContent("YOU SHOULD CHECK THE SEWER");
        _fontStyle = new GUIStyle {
            font = customFont,
            fontSize = 50,
            normal = new GUIStyleState() { textColor = Color.white }
        };

    }
    

    private void OnGUI() {
        if (!_hasInteracted) return;

        if (_timeLastMessage <= 0) {
            var orchestrator = GameObject.Find("Orchestrator").GetComponent<Stage2Orchestrator>();
            orchestrator.PlayerStats.Kill();
        }

        _timeLastMessage -= Time.deltaTime;

        GUI.Label(new Rect(0, 20, 100, 500), _hintSkipText, _fontStyle);
    }
    
}
