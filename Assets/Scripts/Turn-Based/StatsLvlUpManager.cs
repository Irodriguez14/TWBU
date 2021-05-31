using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsLvlUpManager : MonoBehaviour
{
    BattleUnit characterLvlUp;
    public int pointsToAdd = 7;
    [SerializeField] TextMeshProUGUI points;


    public void loadCharacter(BattleUnit bu)
    {
        characterLvlUp = bu;
        pointsToAdd = 7;
        points.SetText(pointsToAdd.ToString());
    }


    public void attackPoints()
    {
        if(pointsToAdd > 0) {
            characterLvlUp.bc.getBattleCharacterBase().actualAttack++;
            pointsToAdd--;
            points.SetText(pointsToAdd.ToString());
        }
    }
    public void defensePoints()
    {
        if (pointsToAdd > 0) {
            characterLvlUp.bc.getBattleCharacterBase().actualDefense++;
            pointsToAdd--;
            points.SetText(pointsToAdd.ToString());
        }
    }
    public void majicPoints()
    {
        if (pointsToAdd > 0) {
            characterLvlUp.bc.getBattleCharacterBase().actualMajic++;
            pointsToAdd--;
            points.SetText(pointsToAdd.ToString());
        }
    }
    public void speedPoints()
    {
        if (pointsToAdd > 0) {
            characterLvlUp.bc.getBattleCharacterBase().actualSpeed++;
            pointsToAdd--;
            points.SetText(pointsToAdd.ToString());
        }
    }

}
