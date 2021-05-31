using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.UI;

[System.Serializable]
public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public Inventory inventory;
    private string sceneName;
    [SerializeField] BattleUnit vy;
    [SerializeField] BattleUnit itzie;
    [SerializeField] public BattleCharacterBase vyBase;
    [SerializeField] public BattleCharacterBase itzieBase;
    public BattleCharacterBase enemy;
    public bool firstBattle = true;
    public List<BattleCharacterBase> allPlayers;
    public List<BattleCharacterBase> team;
    public List<BattleCharacter> saveStats;
    public float[] position;
    [SerializeField] public CharacterEquipment characterEquipment;
    public RenderPipelineAsset lights2D;
    public RenderPipelineAsset lights3D;
    public List<Quest> questList;
    //private Options options;
    public List<ItemBase> drops;
    public GameObject popupSave;
    public AbilityTree abilityTree;

    [Header("Plot Booleans")]
    public bool exitTown1 = false;
    public bool letter = false;
    public bool daggerGift = false;
    public bool croquetillasTown1 = false;
    public bool bayaRojaTown1 = false;
    public bool cuerdaRuta1 = false;
    public bool cremaIkigai = false;
    public bool oroRuta2 = false;
    public bool armaGod = false;
    public bool enableRandom = true;
    public bool isItzie = false;
    public bool bossDefeated = false;
    public bool fileExists = false;

    [Header("Scene Stuff")]
    public string lastScene;
    public string currentScene;
    public string sceneBeforeCombat;
    public Vector3 positionBeforeCombat;

    [Header("Canvas & UI Stuff")]
    public Button buttonInteract;
    public Button buttonSave;
    [SerializeField] GameObject inventoryCanvas;
    [SerializeField] Equipment equipmentUI;
    public QuestGiver quests;
    public List<ItemBase> allExistingItems;
    public QuestLog questLog;
    public GameObject mapa;
    public Menu menu;

    public GameObject CharacterVy;
    public GameObject CharacterItzy;

    [Header("Sound")]
    public AudioSource typing;
    public AudioSource itemReceive;


    private void Awake()
    {
        if (gameManager == null) {
            gameManager = this;
        } else {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);

    }

    public void getBools(PlayerData data)
    {
        exitTown1 = data.exitTown1;
        letter = data.letter;
        daggerGift = data.daggerGift;
        croquetillasTown1 = data.croquetillasTown1;
        bayaRojaTown1 = data.bayaRojaTown1;
        lastScene = data.lastScene;
        currentScene = data.currentScene;
        firstBattle = data.firstBattle;
        cuerdaRuta1 = data.cuerdaRuta1;
        cremaIkigai = data.cremaIkigai;
        oroRuta2 = data.oroRuta2;
        armaGod = data.armaGod;
        isItzie = data.isItzie;
        bossDefeated = data.bossDefeated;
        Inventory.inventory.items = new List<Item>();
        foreach (ItemsSaveFile it in data.items) {
            Inventory.inventory.items.Add(new Item(searchForItemBase(it.itemName), it.amount));
        }
        gameManager.saveStats = new List<BattleCharacter>();
        foreach (CharactersSaveFile it in data.chars) {
            saveStats.Add(new BattleCharacter(searchForCharacterBase(it.charName), it));
        }
        team = new List<BattleCharacterBase>();
        foreach (string s in data.charactersInTeam) {
            team.Add(searchForCharacterBase(s));
        }

        Item _0;
        Item _1;
        Item _2;
        Item _3;
        Item _4;
        if (data.equipment[0].amount == 0) {
            _0 = new Item(null, 0);
        } else {
            _0 = new Item(searchForItemBase(data.equipment[0].itemName), 1);
        }
        if (data.equipment[1].amount == 0) {
            _1 = new Item(null, 0);
        } else {
            _1 = new Item(searchForItemBase(data.equipment[1].itemName), 1);
        }
        if (data.equipment[2].amount == 0) {
            _2 = new Item(null, 0);
        } else {
            _2 = new Item(searchForItemBase(data.equipment[2].itemName), 1);
        }
        if (data.equipment[3].amount == 0) {
            _3 = new Item(null, 0);
        } else {
            _3 = new Item(searchForItemBase(data.equipment[3].itemName), 1);
        }
        if (data.equipment[4].amount == 0) {
            _4 = new Item(null, 0);
        } else {
            _4 = new Item(searchForItemBase(data.equipment[4].itemName), 1);
        }

        characterEquipment.setHeadItem(_0);
        characterEquipment.setTrunkItem(_1);
        characterEquipment.setLegsItem(_2);
        characterEquipment.setFeetItem(_3);
        characterEquipment.setWeaponItem(_4);

        position = data.characPos;

        questList = new List<Quest>();
        foreach (QuestsSaveFile qs in data.questsSaved) {
            questList.Add(new Quest(qs.title, qs.description, new CollectObjective(qs.eraseItem, qs.type, qs.amount),
                qs.showObjectives));
        }

        foreach (Quest quest in questList) {
            Debug.Log(QuestLog.MyInstance);
            QuestLog.MyInstance.AcceptQuest(quest);
        }

        QuestLog.MyInstance.questDescription.text = "";
        QuestLog.MyInstance.questDescriptionTitle.text = "";
        QuestLog.MyInstance.questObjectivesTitle.text = "";
        QuestLog.MyInstance.questObjectivesList.text = "";
    }

    public ItemBase searchForItemBase(string name)
    {
        foreach (ItemBase ib in allExistingItems) {
            if (ib.getItemName().Equals(name)) {
                Debug.Log(ib.getItemName());
                return ib;
            }
        }
        return null;
    }
    public BattleCharacterBase searchForCharacterBase(string name)
    {
        foreach (BattleCharacterBase ib in allPlayers) {
            if (ib.getCharacterName().Equals(name)) {
                return ib;
            }
        }
        return null;
    }
    public void errorLoading()
    {
        StartCoroutine(menu.errorLoading());
    }

    private void Start()
    {
        if (vyBase != null && itzieBase != null) {
            characterCreate();
        }

        equipmentUI.setCharacterEquipment(characterEquipment);
        lastScene = SceneManager.GetActiveScene().name;
        questList = new List<Quest>();

    }
    void Update()
    {
        Debug.Log(Application.persistentDataPath);
        if (saveStats.Count != 0) {
        }
        if (buttonSave != null) {
            buttonSave.onClick.RemoveAllListeners();
            buttonSave.onClick.AddListener(saveData);
        }

        if (mapa == null) {
            mapa = GameObject.FindGameObjectWithTag("Map");
        }

        if (isItzie) {
            abilityTree.player = itzieBase;
            abilityTree.loadAbilities();
        } else {
            abilityTree.player = vyBase;
            abilityTree.loadAbilities();
        }

        currentScene = SceneManager.GetActiveScene().name;
        Scene currentScene1 = SceneManager.GetActiveScene();
        sceneName = currentScene1.name;
        Debug.Log(currentScene1.isLoaded);
        if (sceneName == "PruebaTurnBased") {
            GraphicsSettings.renderPipelineAsset = null;

        } else if (sceneName == "Prueba3D") {
            GraphicsSettings.renderPipelineAsset = null;

        } else if (sceneName == "CasaVy") {
            GraphicsSettings.renderPipelineAsset = lights2D;
            isItzie = false;
            CharacterItzy.SetActive(false);
            CharacterVy.SetActive(true);
        } else if (sceneName == "Pueblo1") {
            GraphicsSettings.renderPipelineAsset = lights2D;
            isItzie = false;
            CharacterItzy.SetActive(false);
            CharacterVy.SetActive(true);
        } else if (sceneName == "CasaTown1_1") {
            GraphicsSettings.renderPipelineAsset = lights2D;
            isItzie = false;
            CharacterItzy.SetActive(false);
            CharacterVy.SetActive(true);
        } else if (sceneName == "CasaTown1_2") {
            GraphicsSettings.renderPipelineAsset = lights2D;
            isItzie = false;
            CharacterItzy.SetActive(false);
            CharacterVy.SetActive(true);
        } else if (sceneName == "Ruta1") {
            GraphicsSettings.renderPipelineAsset = lights2D;
            isItzie = false;
            CharacterItzy.SetActive(false);
            CharacterVy.SetActive(true);
        } else if (sceneName == "Ruta2") {
            GraphicsSettings.renderPipelineAsset = lights2D;
            isItzie = false;
            CharacterItzy.SetActive(false);
            CharacterVy.SetActive(true);
        } else if (sceneName == "BaseIkigai") {
            GraphicsSettings.renderPipelineAsset = lights2D;
            isItzie = true;
            CharacterItzy.SetActive(true);
            CharacterVy.SetActive(false);
        }
    }

    public CharacterEquipment getCharacterEquipment() { return this.characterEquipment; }
    public void hideInventory() { inventoryCanvas.SetActive(false); }
    public void showInventory() { inventoryCanvas.SetActive(true); }
    public void readLetter() { letter = true; }
    public bool letterComplete() { return letter; }
    public bool canExitTown1() { return exitTown1; }
    public void changeCanExitTown1() { exitTown1 = true; }
    public bool isDaggerGift() { return daggerGift; }
    public void changeDaggerGift() { daggerGift = true; }
    public bool isCroquetillaTown1() { return croquetillasTown1; }
    public void changeCroquetillaTown1() { croquetillasTown1 = true; }
    public bool isBayaRojaTown1() { return bayaRojaTown1; }
    public void changeBayaRojaTown1() { bayaRojaTown1 = true; }
    public bool isCuerdaRuta1() { return cuerdaRuta1; }
    public void changeCuerdaRuta1() { cuerdaRuta1 = true; }
    public bool isCremaIkigai() { return cremaIkigai; }
    public void changeCremaIkigai() { cremaIkigai = true; }
    public bool isArmaGod() { return armaGod; }
    public void changeArmaGod() { armaGod = true; }

    public void setItzie()
    {
        GameManage.instance.player.gameObject.GetComponent<MoveTo>().anim.SetBool("isItzie", true);
    }

    public void goToMenu()
    {
        lastScene = SceneManager.GetActiveScene().name;
        Debug.Log("last scene " + lastScene);
        SceneManager.LoadScene("Menu");
    }

    public void goToScene3D()
    {
        lastScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Prueba3D");
    }


    public void saveData()
    {
        SaveManager.SavePlayer();
        StartCoroutine(waitToSave());
    }

    public IEnumerator waitToSave()
    {
        popupSave.SetActive(true);
        yield return new WaitForSeconds(1f);
        popupSave.SetActive(false);
    }

    private void characterCreate()
    {
        team = new List<BattleCharacterBase>();
        team.Add(vyBase);
        //team.Add(itzieBase);

        saveStats = new List<BattleCharacter>();

    }

    //public void saveBattleUnits(List<GameObject> team)
    //{
    //    team.Clear();
    //    foreach(GameObject go in team) {
    //        if(go.GetComponent<BattleUnit>().getBcb().getCharacterName() == "Vy") {
    //            vy = go;
    //            team.Add(vy);
    //        }
    //        if (go.GetComponent<BattleUnit>().getBcb().getCharacterName() == "Itzie") {
    //            itzie = go;
    //            team.Add(itzie);
    //        }
    //    }


    //}
}
