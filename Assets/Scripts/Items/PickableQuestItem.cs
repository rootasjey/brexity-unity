using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class PickableQuestItem : Interactable
    {
        private RectTransform _rectTransform;
        private float _timeAnimation = .5f;
        private float _deltaV = 0.01f;
        public int questIndex = -1;
        public int itemId = -1;
        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (_timeAnimation > 0)
            {
                _timeAnimation -= Time.deltaTime;
                _rectTransform.transform.position =
                    _rectTransform.transform.position - new Vector3(0, _deltaV);

            }
            else
            {
                _timeAnimation = 1f;
                _deltaV = -_deltaV;
            }

            CheckInteractionInput();
        }

        public override void Interact()
        {
            
            playerStats.QuestIndexGameObject = new KeyValuePair<int, GameObject>(questIndex, Instantiate(gameObject));
            Destroy(gameObject);
            // Stage2Quest stage2Quest = PersistentQuest.instance.QuestPrefab.GetComponent<Stage2Quest>();
            // if (stage2Quest.Quest.CurrentStep.Equals(stepQuest) && stage2Quest.Quest.Objectives.Count > stepQuest)
            // {
            //    stage2Quest.Quest.Objectives[stepQuest].IsComplete = true;
            // }
        }
    }
}
