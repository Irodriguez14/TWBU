using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //public GameManager gm;

    [Header("Plot Booleans")]
    public bool exitTown1;
    public bool letter;
    public bool daggerGift;
    public bool croquetillasTown1;
    public bool bayaRojaTown1;
    [Header("Scene Stuff")]
    public string lastScene;
    public string currentScene;
    public bool firstBattle;
    public bool cuerdaRuta1;
    public bool cremaIkigai;
    public bool oroRuta2;
    public bool armaGod;
    public bool isItzie;
    public bool bossDefeated;
    public List<ItemsSaveFile> items;
    public List<CharactersSaveFile> chars;
    public List<string> charactersInTeam;
    public List<ItemsSaveFile> equipment;

    public float[] characPos = new float[3];

    public List<QuestsSaveFile> questsSaved;

    


    public PlayerData()
    {
        items = new List<ItemsSaveFile>();
        chars = new List<CharactersSaveFile>();
        equipment = new List<ItemsSaveFile>();
        charactersInTeam = new List<string>();
        exitTown1 = GameManager.gameManager.exitTown1;
        letter = GameManager.gameManager.letter;
        daggerGift = GameManager.gameManager.daggerGift;
        croquetillasTown1 = GameManager.gameManager.croquetillasTown1;
        bayaRojaTown1 = GameManager.gameManager.bayaRojaTown1;
        lastScene = GameManager.gameManager.lastScene;
        currentScene = GameManager.gameManager.currentScene;
        firstBattle = GameManager.gameManager.firstBattle;
        cuerdaRuta1 = GameManager.gameManager.cuerdaRuta1;
        cremaIkigai = GameManager.gameManager.cremaIkigai;
        oroRuta2 = GameManager.gameManager.oroRuta2;
        armaGod = GameManager.gameManager.armaGod;
        isItzie = GameManager.gameManager.isItzie;
        bossDefeated = GameManager.gameManager.bossDefeated;
        //team = GameManager.gameManager.team;
        //saveStats = GameManager.gameManager.saveStats;

        foreach (Item i in Inventory.inventory.items) {
            this.items.Add(new ItemsSaveFile(i.amount, i.item.getItemName()));
        }
        foreach (BattleCharacter bc in GameManager.gameManager.saveStats) {
            this.chars.Add(new CharactersSaveFile(bc.getBattleCharacterBase().getCharacterName(), bc.getLvl(), 
                bc.getCurrentHP(), bc.getCurrentMajic(), bc.isGuard(), bc.getExp(), bc.getTurnsGuarded(), bc.getBattleCharacterBase().getPoints()));
        }

        foreach(BattleCharacterBase bcb in GameManager.gameManager.team) {
            charactersInTeam.Add(bcb.getCharacterName());
        }
        if(GameManager.gameManager.characterEquipment.headItem.item != null) {
            equipment.Add(new ItemsSaveFile(1, GameManager.gameManager.characterEquipment.headItem.item.getItemName()));
        } else {
            equipment.Add(new ItemsSaveFile(0,""));
        }
        if (GameManager.gameManager.characterEquipment.trunkItem.item != null) {
            equipment.Add(new ItemsSaveFile(1, GameManager.gameManager.characterEquipment.trunkItem.item.getItemName()));
        } else {
            equipment.Add(new ItemsSaveFile(0, ""));
        }
        if (GameManager.gameManager.characterEquipment.legsItem.item != null) {
            equipment.Add(new ItemsSaveFile(1, GameManager.gameManager.characterEquipment.legsItem.item.getItemName()));
        } else {
            equipment.Add(new ItemsSaveFile(0, ""));
        }
        if (GameManager.gameManager.characterEquipment.feetItem.item != null) {
            equipment.Add(new ItemsSaveFile(1, GameManager.gameManager.characterEquipment.feetItem.item.getItemName()));
        } else {
            equipment.Add(new ItemsSaveFile(0, ""));
        }
        if (GameManager.gameManager.characterEquipment.weaponItem.item != null) {
            equipment.Add(new ItemsSaveFile(1, GameManager.gameManager.characterEquipment.weaponItem.item.getItemName()));
            Debug.Log("save " + equipment);
        } else {
            equipment.Add(new ItemsSaveFile(0, ""));
        }
        characPos[0] = GameManage.instance.player.position.x;
        characPos[1] = GameManage.instance.player.position.y;
        characPos[2] = GameManage.instance.player.position.z;

        questsSaved = new List<QuestsSaveFile>();

        foreach(Quest quest in GameManager.gameManager.questList) {

            questsSaved.Add(new QuestsSaveFile(quest.MyTitle, quest.MyDescription, quest.showObjectives,
                quest.collectObjectives[0].MyAmount, quest.collectObjectives[0].MyType, quest.collectObjectives[0].eraseItems));
        }


    }


}
