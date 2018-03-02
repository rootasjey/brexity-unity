using System.Collections.Generic;

namespace Assets.Scripts.Stages {
    public class Quest {
        public string Name { get; set; }

        public bool IsMain { get; set; }

        public bool IsSecondary { get; set; }

        public List<Objective> Objectives { get; set; }

        public void CompleteNext() {
            var nextQuestIncomplete = Objectives.Find(o => o.IsComplete == false);
            if (nextQuestIncomplete != null) nextQuestIncomplete.IsComplete = true; 
        }

        public bool IsComplete() {
            var isComplete = true;

            foreach (var objective in Objectives) {
                isComplete = isComplete && objective.IsComplete;
            }

            return isComplete;
        }
    }
}
