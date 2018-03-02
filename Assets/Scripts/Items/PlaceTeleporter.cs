using UnityEngine;

namespace Assets.Scripts.Items {
    public class PlaceTeleporter : Interactable {
        private RectTransform _rectTransform;
        private float _timeAnimation = .5f;
        private float _deltaV = 0.01f;

        public GameObject teleporterDestination;

        private void Start() {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update() {
            if (_timeAnimation > 0) {
                _timeAnimation -= Time.deltaTime;
                _rectTransform.transform.position =
                    _rectTransform.transform.position - new Vector3(0, _deltaV);

            } else {
                _timeAnimation = 1f;
                _deltaV = -_deltaV;
            }

            CheckInteractionInput();
        }

        public override void Interact() {
            var pos = teleporterDestination.transform.position;

            var orchestrator = GameObject.Find("Orchestrator").GetComponent<Stage2Orchestrator>();
            orchestrator.Player.transform.position = new Vector3(
                pos.x,
                pos.y + 3
                );
        }
    }    
}
