using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class PickableQuestItem : Interactable
    {
        public int questIndex = -1;
        public int itemId = -1;

        public override void Interact()
        {
            playerStats.QuestIndexGameObject = new KeyValuePair<int, GameObject>(questIndex, Instantiate(gameObject));
            Destroy(gameObject);
        }
    }
}
