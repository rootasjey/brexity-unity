using UnityEngine;

namespace Assets.Scripts.Inv {
    public class EnergyItem : InvItem {
        public float Enerngy { get; set; }

        public override bool IsUsable {
            get {
                return base.IsUsable;
            }
        }

        public override bool IsQuestItem {
            get {
                return false;
            }
        }

        public override void Use() {
            var drone = GameObject.Find("Drone");
            if (!drone) return;

            // Fill up energy drone
        }
    }
}
