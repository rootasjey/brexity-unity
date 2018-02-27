using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Items {
    public class PlaceTeleporter : Interactable {
        private RectTransform _rectTransform;
        private float _timeAnimation = 1f;
        private float _deltaV = 0.01f;

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
        }
    }
}
