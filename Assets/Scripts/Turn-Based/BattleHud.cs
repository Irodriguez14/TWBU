using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI LvlText;
    [SerializeField] HPBar hpBar;
    [SerializeField] HPBar majicBar;
    [SerializeField] Image icon;
    BattleCharacter battleCharacter;
    
    public void setData(BattleCharacter bc)
    {
        battleCharacter = bc;
        nameText.text = bc.getBattleCharacterBase().getCharacterName();
        LvlText.text = "LvL: " + bc.getLvl();
        hpBar.SetHP((float) bc.getCurrentHP() / bc.getBattleCharacterBase().actualMaxHp);
        majicBar.SetHP((float) bc.getCurrentMajic() / bc.getBattleCharacterBase().actualMajic);
        icon.sprite = bc.getBattleCharacterBase().icon;
    }

    public IEnumerator updateHP()
    {
        yield return hpBar.setHPAnim((float)battleCharacter.getCurrentHP() / battleCharacter.getBattleCharacterBase().actualMaxHp);
    }

    public IEnumerator updateMajic() {
        Debug.Log(battleCharacter.getCurrentMajic());
        yield return majicBar.setHPAnim((float)battleCharacter.getCurrentMajic() / battleCharacter.getBattleCharacterBase().actualMajic);
    }


}
