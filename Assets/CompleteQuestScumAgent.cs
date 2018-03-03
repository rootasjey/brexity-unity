using Assets.Scripts.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteQuestScumAgent : MonoBehaviour {

    private CutSceneStarter _cutSceneStarter;
    private PlayerStats _playerStat;
    public int itemId;
	// Update is called once per frame
	void Update () {
        if (_cutSceneStarter == null)
        {
            _cutSceneStarter = gameObject.GetComponent<CutSceneStarter>();
            return;
        }

         if (_playerStat == null)
        {
            _playerStat = _cutSceneStarter.localPlayerStats;
            return;
        }

        if (_playerStat.QuestIndexGameObject.Value == null || _playerStat.QuestIndexGameObject.Value.GetComponent<PickableQuestItem>() == null )
        {
            return;
        }

        if (_playerStat.QuestIndexGameObject.Value.GetComponent<PickableQuestItem>().itemId == itemId)
        {
            _cutSceneStarter.incrementQuestStep = true;
        }
    }
}
