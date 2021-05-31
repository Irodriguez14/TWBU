using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy, SelectEnemy, TurnMove }
public class BattleSystem : MonoBehaviour
{
    [SerializeField] List<BattleCharacterBase> playerChars;
    [SerializeField] List<BattleCharacterBase> enemyChars;
    [SerializeField] BattleDialogBox dialogBox;
    [SerializeField] Camera battleCam;
    [SerializeField] Animator animUnit1;
    [SerializeField] GameObject blurPanel;
    [SerializeField] GameObject playerUnitZoomed;
    [SerializeField] GameObject enemyUnitZoomed;
    [SerializeField] GameObject effects;
    [SerializeField] GameObject statsPanel;
    [SerializeField] GameObject backButton;
    [SerializeField] StatsLvlUpManager statsLvlUp;
    [SerializeField] GameObject menuObjects;
    [SerializeField] ObjectsBattle objectsBattle;
    [SerializeField] ItemUseMenu usePanel;
    private GameObject enemySelected;
    private GameObject allySelected;

    private NavMeshSurface2d hola;

    BattleState state;

    private string selectedAbility;
    private BattleCharacter enemyBCSelectedAttack;
    private BattleCharacter allyBCSelectedAttack;
    private int deadEnemyCount;
    private int deadPlayerCount;
    private GameObject[] enemyUnitsLayout;
    private GameObject[] playerUnitsLayout;
    private List<BattleUnit> enemyUnits;
    private List<BattleUnit> playerUnits;
    private bool selectEnemyToAttack;
    private bool selectAllyToAttack;

    private BattleUnit currentTurnBU;
    private bool isTurning;
    BattleUnit lastTurn;
    List<BattleUnit> allUnits;
    bool battleFinished = false;

    List<BattleUnit> stats;

    private void Start()
    {
        deadPlayerCount = 0;
        deadEnemyCount = 0;
        randomQuantityEnemies();
        enemyUnits = new List<BattleUnit>();
        playerUnits = new List<BattleUnit>();
        allUnits = new List<BattleUnit>();
        enemyUnitsLayout = GameObject.FindGameObjectsWithTag("EnemyUnitslayout");
        playerUnitsLayout = GameObject.FindGameObjectsWithTag("PlayerUnitsLayout");
        checkLayout();
        StartCoroutine(SetupBattle());

    }

    public void randomQuantityEnemies()
    {
        int allies = GameManager.gameManager.team.Count;
        Debug.Log(allies);
        if (allies == 1) {
            int random = Random.Range(1, 3);
            for (int i = 0; i < random; i++) {
                enemyChars.Add(GameManager.gameManager.enemy);
            }
        }
        if (allies == 2) {
            int random = Random.Range(1, 4);
            for (int i = 0; i < random; i++) {
                enemyChars.Add(GameManager.gameManager.enemy);
            }
        }
        if (allies == 3) {
            int random = Random.Range(2, 4);
            for (int i = 0; i < random; i++) {
                enemyChars.Add(GameManager.gameManager.enemy);
            }
        }
    }

    public IEnumerator SetupBattle()
    {


        GameManager.gameManager.firstBattle = false;
        foreach (BattleUnit bu in enemyUnits) {
            bu.setup();
            bu.gameObject.transform.GetChild(0).GetComponent<BattleHud>().setData(bu.getBattleCharacter());
        }

        chooseIdle();

        dialogBox.buttonsActionSelector(false);
        yield return dialogBox.typeDialog("¡Te has encontrado con un enemigo!");
        yield return new WaitForSeconds(1f);
        prepareBattle();
    }
    IEnumerator playerAction()
    {
        yield return new WaitForSeconds(0.5f);
        dialogBox.setMoveNames(currentTurnBU.bc.abilities);
        state = BattleState.PlayerAction;
        yield return dialogBox.typeDialog("Elige una acción");
        dialogBox.buttonsActionSelector(true);
        dialogBox.enableActionSelector(true);
    }

    public void playerMove()
    {
        state = BattleState.PlayerMove;
        dialogBox.enableActionSelector(false);
        dialogBox.enableDialogText(false);
        dialogBox.enableMoveSelector(true);
        backButton.SetActive(true);

    }

    public void showMenuObjects()
    {
        backButton.SetActive(true);
        menuObjects.SetActive(true);
        dialogBox.enableActionSelector(false);
        objectsBattle.UpdateUI();

    }


    public void selectAlly(Button button)
    {
        if (selectAllyToAttack) {
            allySelected = button.gameObject;
            allyBCSelectedAttack = button.GetComponent<BattleUnit>().bc;
            Item item = usePanel.getItem();
            ConsumableBase consumable = (ConsumableBase)(item.item);
            selectAllyToAttack = false;
            if (consumable.getConsumableType() == ConsumableType.HEALTH) {
                allyBCSelectedAttack.addHp(consumable.getAmountConsumable());
                StartCoroutine(updateHPBarConsumable());
                objectsBattle.removeItemBattle(item, 1);
                menuObjects.SetActive(false);
            }
            currentTurnBU.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("isTurn", false);
            allUnits.Remove(currentTurnBU);
            isTurning = false;
            StartCoroutine(turnsBattle());
        }
    }

    private IEnumerator updateHPBarConsumable()
    {
        Debug.Log(allySelected);
        yield return allySelected.gameObject.transform.GetChild(0).GetComponent<BattleHud>().updateHP();
    }

    public void useItem()
    {
        usePanel.gameObject.SetActive(false);
        menuObjects.SetActive(false);
        StartCoroutine(objectUseDialog());
    }
    private IEnumerator objectUseDialog()
    {
        backButton.SetActive(false);
        yield return dialogBox.typeDialog("Selecciona el aliado con el que quieres usarlo");
        selectAllyToAttack = true;
    }

    IEnumerator performPlayerAbility()
    {
        state = BattleState.Busy;
        Ability ability = null;
        foreach (Ability a in currentTurnBU.getBattleCharacter().abilities) {
            Debug.Log("ultima " + a.getAbilityBase().getAbilityName() + "    selected " + selectedAbility);
            if (a.getAbilityBase().getAbilityName().Equals(selectedAbility)) {
                ability = a;
            }
        }
        if (currentTurnBU.getBattleCharacter().calculateMajic(ability, currentTurnBU.getBattleCharacter())) {
            yield return dialogBox.typeDialog(currentTurnBU.getBattleCharacter().getBattleCharacterBase().getCharacterName() + " ha usado " + ability.abilityBase.getAbilityName());
            yield return new WaitForSeconds(1f);
            zoomToAttack(currentTurnBU.gameObject, enemySelected, ability.abilityBase.getAbilityAnim(), true);
            yield return new WaitForSeconds(2.5f);
            bool isFainted = enemyBCSelectedAttack.TakeDamage(ability, currentTurnBU.bc);
            BattleUnit enemyUnit = getEnemyUnitWithBC(enemyBCSelectedAttack);
            yield return enemyUnit.gameObject.transform.GetChild(0).GetComponent<BattleHud>().updateHP();
            yield return currentTurnBU.gameObject.transform.GetChild(0).GetComponent<BattleHud>().updateMajic();
            if (isFainted) {
                deadEnemyCount++;
                if (enemyUnit != null) {
                    yield return dialogBox.typeDialog(enemyBCSelectedAttack.getBattleCharacterBase().getCharacterName() + " ha muerto");
                    yield return new WaitForSeconds(0.5f);
                    enemyUnit.setDead();
                    //TODO animacion se muere y fade out
                    enemyUnit.gameObject.SetActive(false);
                    enemyUnits.Remove(enemyUnit);
                    if (enemyUnit.isDead()) {
                        allUnits.Remove(enemyUnit);
                    }
                }
            }
            currentTurnBU.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("isTurn", false);
            allUnits.Remove(currentTurnBU);
            isTurning = false;
            StartCoroutine(turnsBattle());
        } else {
            yield return dialogBox.typeDialog("No tienes suficiente Majic");
            yield return playerAction();
        }

    }

    IEnumerator enemyMove()
    {
        state = BattleState.EnemyMove;
        var ability = currentTurnBU.bc.getRandomAbility();
        yield return dialogBox.typeDialog(currentTurnBU.getBattleCharacter().getBattleCharacterBase().getCharacterName() + " ha usado " + ability.abilityBase.getAbilityName());
        yield return new WaitForSeconds(1f);
        int randomAttack = Random.Range(0, playerUnits.Count);
        if (playerUnits[randomAttack].getBattleCharacter().isGuard()) {
            yield return dialogBox.typeDialog(playerUnits[randomAttack].getBattleCharacter().getBattleCharacterBase().getCharacterName() + " se ha cubierto");
            yield return new WaitForSeconds(1f);
            playerUnits[randomAttack].bc.toggleGuard();
        } else {
            zoomToAttack(playerUnits[randomAttack].gameObject, currentTurnBU.gameObject, ability.abilityBase.getAbilityAnim(), false);
            yield return new WaitForSeconds(2.5f);
            bool isFainted = playerUnits[randomAttack].bc.TakeDamage(ability, currentTurnBU.bc);
            yield return playerUnits[randomAttack].gameObject.transform.GetChild(0).GetComponent<BattleHud>().updateHP();
            yield return currentTurnBU.gameObject.transform.GetChild(0).GetComponent<BattleHud>().updateMajic();

            if (isFainted) {
                yield return dialogBox.typeDialog(playerUnits[randomAttack].bc.getBattleCharacterBase().name + " ha muerto");
                yield return new WaitForSeconds(0.5f);
                playerUnits[randomAttack].setDead();
                deadPlayerCount++;
                playerUnits[randomAttack].gameObject.SetActive(false);
                if (playerUnits[randomAttack].isDead()) {
                    allUnits.Remove(playerUnits[randomAttack]);
                }
                playerUnits.Remove(playerUnits[randomAttack]);
                Debug.Log("muerto");
            }
        }
        if (lastTurn != null) {
            if (lastTurn.bc.isGuard()) { lastTurn.bc.toggleGuard(); }
        }

        currentTurnBU.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("isTurn", false);
        isTurning = false;
    }

    public void basicAttackSelect()
    {
        dialogBox.enableActionSelector(false);
        foreach (Ability a in currentTurnBU.getBattleCharacter().abilities) {
            if (a.abilityBase.isBasicAttack()) {
                selectedAbility = a.abilityBase.getAbilityName();
            }
        }
        StartCoroutine(selectEnemy());
    }


    public void guardSelect()
    {
        currentTurnBU.getBattleCharacter().toggleGuard();
        StartCoroutine(guard());

    }

    IEnumerator guard()
    {
        yield return dialogBox.typeDialog(currentTurnBU.getBattleCharacter().getBattleCharacterBase().getCharacterName() + " se está cubriendo");
        yield return new WaitForSeconds(1f);
        //make an animation to guard
        allUnits.Remove(currentTurnBU);
        isTurning = false;
        StartCoroutine(turnsBattle());
    }

    IEnumerator selectEnemy()
    {
        backButton.SetActive(false);
        yield return dialogBox.typeDialog("Selecciona el enemigo al que quieres atacar");
        state = BattleState.SelectEnemy;
        selectEnemyToAttack = true;
    }

    public void playerAbility()
    {
        dialogBox.enableMoveSelector(false);
        dialogBox.enableDialogText(true);
        selectedAbility = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;

        StartCoroutine(selectEnemy());
    }

    //Deprecated
    public void checkWhichEnemy()
    {
        GameObject prueba = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        enemyBCSelectedAttack = prueba.GetComponent<BattleUnit>().bc;
        StartCoroutine(performPlayerAbility());
    }

    public void selectEnemy(Button button)
    {
        if (selectEnemyToAttack) {
            enemySelected = button.gameObject;
            enemyBCSelectedAttack = button.GetComponent<BattleUnit>().bc;
            selectEnemyToAttack = false;
            StartCoroutine(performPlayerAbility());
        }

    }


    private void prepareBattle()
    {
        foreach (BattleUnit bu in enemyUnits) {
            if (!bu.isDead()) {
                allUnits.Add(bu);
            }
        }
        foreach (BattleUnit bu in playerUnits) {
            if (!bu.isDead()) {
                allUnits.Add(bu);
            }
        }
        allUnits.Sort(BattleUnit.SortBySpeed);
        allUnits.Reverse();
        StartCoroutine(turnsBattle());
    }

    private IEnumerator turnsBattle()
    {
        if (allUnits.Count != 0) {
            state = BattleState.TurnMove;
            List<BattleUnit> temp = new List<BattleUnit>(allUnits);
            foreach (BattleUnit bu in temp) {
                lastTurn = currentTurnBU;
                if (deadEnemyCount == enemyChars.Count) {
                    battleFinished = true;
                }
                if (deadPlayerCount == GameManager.gameManager.team.Count) {
                    battleFinished = true;
                }
                if (!battleFinished) {
                    if (lastTurn != bu) {
                        currentTurnBU = bu;
                        if (!isTurning) {
                            Debug.Log(bu.isDead());
                            if (!bu.isDead()) {
                                if (bu.isPlayerUnit()) {
                                    if (lastTurn != null) if (lastTurn.bc.isGuard()) { lastTurn.bc.toggleGuard(); }
                                    bu.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("isTurn", true);
                                    isTurning = true;
                                    break;
                                } else {
                                    bu.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("isTurn", true);
                                    isTurning = true;
                                    yield return StartCoroutine(enemyMove());
                                    lastTurn = bu;
                                    allUnits.Remove(currentTurnBU);
                                }
                            } else {
                                allUnits.Remove(currentTurnBU);
                            }
                        }
                    }
                } else {
                    isTurning = false;
                    yield return dialogBox.typeDialog("Fin del combate");
                    Debug.Log("Hola");
                    currentTurnBU = null;
                    if (deadEnemyCount == enemyChars.Count) {
                        ItemBase item = GameManager.gameManager.drops[Random.Range(0, GameManager.gameManager.drops.Count)];
                        yield return dialogBox.typeDialog("Has conseguido " + item.getItemName());
                        yield return new WaitForSeconds(1f);
                        Inventory.inventory.addItem(item);
                        foreach (BattleUnit battleUnit in playerUnits) {
                            if (battleUnit.bc.addExperience(calculateExperience())) {
                                yield return dialogBox.typeDialog(battleUnit.bc.getBattleCharacterBase().getCharacterName() + " ha subido de nivel");
                                yield return new WaitForSeconds(1f);
                                statsLvlUp.loadCharacter(battleUnit);
                                if (statsLvlUp.pointsToAdd > 0) {
                                    blurPanel.gameObject.SetActive(true);
                                    statsPanel.SetActive(true);
                                } else {
                                    blurPanel.gameObject.SetActive(false);
                                    statsPanel.SetActive(false);
                                }

                            } else {
                                GameManager.gameManager.lastScene = SceneManager.GetActiveScene().name;
                                GameManager.gameManager.gameObject.GetComponent<PauseManager>().uiCombateUp();
                                SceneManager.LoadScene(GameManager.gameManager.sceneBeforeCombat);
                            }
                        }
                    } else {
                        GameManager.gameManager.gameObject.GetComponent<PauseManager>().uiCombateUp();
                        GameManager.gameManager.lastScene = SceneManager.GetActiveScene().name;
                        foreach (BattleCharacter bc in GameManager.gameManager.saveStats) {
                            bc.heal();
                        }
                        if (!GameManager.gameManager.isItzie) {
                            SceneManager.LoadScene("CasaVy");
                        } else if (GameManager.gameManager.isItzie) {
                            SceneManager.LoadScene("BaseIkigai");
                        }
                    }
                    break;
                }
            }

            if (currentTurnBU != null && !currentTurnBU.isDead()) {
                if (currentTurnBU.isPlayerUnit()) {
                    if (isTurning) {
                        yield return playerAction();
                    }
                }
            }
            if (allUnits.Count == 0 && deadEnemyCount != enemyChars.Count && deadPlayerCount != GameManager.gameManager.team.Count) {
                prepareBattle();
            } else if (deadPlayerCount == GameManager.gameManager.team.Count) {
                GameManager.gameManager.gameObject.GetComponent<PauseManager>().uiCombateUp();
                GameManager.gameManager.lastScene = SceneManager.GetActiveScene().name;
                foreach (BattleCharacter bc in GameManager.gameManager.saveStats) {
                    bc.heal();
                }
                if (!GameManager.gameManager.isItzie) {
                    SceneManager.LoadScene("CasaVy");
                } else if (GameManager.gameManager.isItzie) {
                    SceneManager.LoadScene("BaseIkigai");
                }
            }
        } else {
            prepareBattle();
        }
    }

    private void Update()
    {

        if (statsLvlUp.pointsToAdd <= 0) {
            statsPanel.SetActive(false);
            blurPanel.SetActive(false);
            GameManager.gameManager.lastScene = SceneManager.GetActiveScene().name;
            GameManager.gameManager.gameObject.GetComponent<PauseManager>().uiCombateUp();
            SceneManager.LoadScene(GameManager.gameManager.sceneBeforeCombat);
        }
        if (currentTurnBU != null) currentTurnBU.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("isTurn", true);
        if (lastTurn != null) lastTurn.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().SetBool("isTurn", false);


    }

    private int calculateExperience()
    {
        return 15 * enemyChars.Count;
    }

    private void makeAllEnemyButtonsNotInteractable()
    {
        foreach (BattleUnit bu in enemyUnits) {
            bu.makeButtonInteractable(false);
        }
    }

    private void makeAllEnemyButtonsInteractable()
    {
        foreach (BattleUnit bu in enemyUnits) {
            bu.makeButtonInteractable(true);
        }
    }

    private BattleUnit getEnemyUnitWithBC(BattleCharacter bc)
    {
        foreach (BattleUnit bu in enemyUnits) {
            if (bu.GetComponent<BattleUnit>().bc == bc) {
                //Debug.Log(bu.name);
                return bu;
            }
        }
        return null;
    }

    private void checkLayout()
    {
        foreach (GameObject go in playerUnitsLayout) {
            int i = 0;
            foreach (Transform children in go.transform) {
                if (i < GameManager.gameManager.team.Count) {
                    children.gameObject.AddComponent<BattleUnit>();
                    children.GetComponent<BattleUnit>().setBcb(GameManager.gameManager.team[i]);
                    children.GetComponent<BattleUnit>().setLvl(1);
                    children.GetComponent<BattleUnit>().setPlayerUnit();
                    children.GetComponent<BattleUnit>().setButton(children.GetComponent<Button>());
                    playerUnits.Add(children.gameObject.GetComponent<BattleUnit>());
                    if (GameManager.gameManager.saveStats.Count == 2 /*&& GameManager.gameManager.saveStats.Count != 0*/) {
                        if (GameManager.gameManager.saveStats[i].getCurrentHP() <= 0) {
                            playerUnits[i].dead = true;
                            deadPlayerCount++;
                            Debug.Log(deadPlayerCount);
                            children.gameObject.SetActive(false);
                        }
                    }
                    i++;
                } else {
                    children.gameObject.SetActive(false);
                }

            }
        }
        if (GameManager.gameManager.saveStats.Count == 2) {
            for (int i = 0; i < playerUnits.Count; i++) {
                if (GameManager.gameManager.firstBattle) {
                    playerUnits[i].setup();
                    GameManager.gameManager.saveStats.Add(playerUnits[i].bc);
                } else {
                    playerUnits[i].setupWithData(GameManager.gameManager.saveStats[i]);
                }
                playerUnits[i].gameObject.transform.GetChild(0).GetComponent<BattleHud>().setData(playerUnits[i].getBattleCharacter());
            }
        } else {
            if (!GameManager.gameManager.firstBattle) {
                if(playerUnits.Count == 2) {
                    playerUnits[0].setupWithData(GameManager.gameManager.saveStats[0]);
                    playerUnits[1].setup();
                    playerUnits[0].gameObject.transform.GetChild(0).GetComponent<BattleHud>().setData(playerUnits[0].getBattleCharacter());
                    playerUnits[1].gameObject.transform.GetChild(0).GetComponent<BattleHud>().setData(playerUnits[1].getBattleCharacter());
                    GameManager.gameManager.saveStats.Add(playerUnits[1].bc);
                }
                else {
                    playerUnits[0].setup();
                    playerUnits[0].gameObject.transform.GetChild(0).GetComponent<BattleHud>().setData(playerUnits[0].getBattleCharacter());
                }
               
            } else {
                for (int i = 0; i < playerUnits.Count; i++) {
                    if (GameManager.gameManager.firstBattle) {
                        playerUnits[i].setup();
                        GameManager.gameManager.saveStats.Add(playerUnits[i].bc);
                    } else {
                        playerUnits[i].setupWithData(GameManager.gameManager.saveStats[i]);
                    }
                    playerUnits[i].gameObject.transform.GetChild(0).GetComponent<BattleHud>().setData(playerUnits[i].getBattleCharacter());
                }
            }
               
        }

        if (playerUnits.Count == 2 && playerUnits[0].dead) {
            playerUnits.Remove(playerUnits[0]);
        }
        if (playerUnits.Count == 2 && playerUnits[1].dead) {
            playerUnits.Remove(playerUnits[1]);
        }


        foreach (GameObject go in enemyUnitsLayout) {
            int i = 0;
            foreach (Transform children in go.transform) {
                if (i < enemyChars.Count) {
                    int random = 1;
                    if (playerUnits[0].bc.getLvl() == 1) {
                        random = Random.Range(playerUnits[0].bc.getLvl(), (playerUnits[0].bc.getLvl() + 3));
                    } else {
                        random = Random.Range(playerUnits[0].bc.getLvl() - 1, (playerUnits[0].bc.getLvl() + 3));
                    }
                    children.gameObject.AddComponent<BattleUnit>();
                    children.GetComponent<BattleUnit>().setBcb(enemyChars[i]);
                    children.GetComponent<BattleUnit>().setLvl(random);
                    children.GetComponent<BattleUnit>().setButton(children.GetComponent<Button>());
                    enemyUnits.Add(children.gameObject.GetComponent<BattleUnit>());
                    i++;
                } else {
                    children.gameObject.SetActive(false);
                }
            }
        }


    }

    private IEnumerator huirRandom()
    {
        float random = Random.Range(0f, 101.0f);
        if (random >= 50) {
            yield return dialogBox.typeDialog("Has conseguido huir");
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("PruebaRPG");

        } else {
            yield return dialogBox.typeDialog("No has podido huir");
            lastTurn = currentTurnBU;
            StartCoroutine(turnsBattle());
            Debug.Log("no huir");
        }
    }

    public void huir()
    {
        StartCoroutine(huirRandom());
    }

    private void chooseIdle()
    {
        foreach (BattleUnit bu in playerUnits) {
            Animator anim = bu.gameObject.GetComponent<Animator>();
            anim.Play("Base Layer." + bu.bc.getBattleCharacterBase().getIdleAnim());
            Debug.Log(bu.bc.getBattleCharacterBase().getIdleAnim());
        }
        foreach (BattleUnit bu in enemyUnits) {
            Animator anim = bu.gameObject.GetComponent<Animator>();
            anim.Play("Base Layer." + bu.bc.getBattleCharacterBase().getIdleAnim());
        }
    }

    private void zoomToAttack(GameObject unit, GameObject enemyUnit, string anim, bool playerIsAttacking)
    {
        IEnumerator cachedCoroutine = zoomUnit(unit, enemyUnit);
        Vector3 unitLocation = unit.GetComponent<RectTransform>().anchoredPosition;
        Vector3 unitEnemyLocation = enemyUnit.GetComponent<RectTransform>().anchoredPosition;
        Vector3 unitSize = unit.transform.localScale;
        Vector3 unitEnemySize = enemyUnit.transform.localScale;
        GameObject unitHud = unit.transform.GetChild(0).gameObject;
        GameObject enemyHud = enemyUnit.transform.GetChild(0).gameObject;
        IEnumerator cachedCoroutineZoomOut = zoomOut(unit, enemyUnit, unitLocation, unitEnemyLocation, unitSize, unitEnemySize);
        StartCoroutine(enableZoom(cachedCoroutine, unit, enemyUnit, anim, playerIsAttacking));
        Debug.Log("Hola");
        StartCoroutine(disableZoom(cachedCoroutineZoomOut, unit, enemyUnit, unitLocation, unitEnemyLocation, unitSize, unitEnemySize));
        unit.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("Base Layer.CardZoomOut");
        enemyUnit.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("Base Layer.CardZoomOut");
    }

    IEnumerator zoomUnit(GameObject unit, GameObject enemyUnit)
    {
        float timeOfTravel = 0.5f; //tiempo hasta llegar a destino
        float currentTime = 0; // tiempo que pasa
        float normalizedValue;

        while (currentTime <= timeOfTravel) {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / timeOfTravel;

            unit.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(unit.gameObject.transform.localPosition, new Vector3(9f, -115f, 180f), normalizedValue);
            unit.transform.localScale = Vector3.Lerp(unit.gameObject.transform.localScale, new Vector3(0.2f, 0.2f, 0.2f), normalizedValue);
            enemyUnit.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(enemyUnit.gameObject.transform.localPosition, new Vector3(-178, -115f, 180f), normalizedValue);
            enemyUnit.transform.localScale = Vector3.Lerp(enemyUnit.gameObject.transform.localScale, new Vector3(0.2f, 0.2f, 0.2f), normalizedValue);
            yield return null;
        }
    }

    private void copyImage(GameObject unit, GameObject enemyUnit)
    {
        playerUnitZoomed.gameObject.SetActive(true);
        effects.gameObject.SetActive(true);
        playerUnitZoomed.GetComponent<SpriteRenderer>().sprite = unit.GetComponent<BattleUnit>().bc.getBattleCharacterBase().getSprite();
        enemyUnitZoomed.gameObject.SetActive(true);
        enemyUnitZoomed.GetComponent<SpriteRenderer>().sprite = enemyUnit.GetComponent<BattleUnit>().bc.getBattleCharacterBase().getSprite();
        playerUnitZoomed.GetComponent<Animator>().Play("Base Layer." + unit.GetComponent<BattleUnit>().bc.getBattleCharacterBase().idleAnimZoom);
        enemyUnitZoomed.GetComponent<Animator>().Play("Base Layer." + enemyUnit.GetComponent<BattleUnit>().bc.getBattleCharacterBase().idleAnimZoom);
    }

    IEnumerator enableZoom(IEnumerator cachedCoroutine, GameObject unit, GameObject enemyUnit, string anim, bool playerIsAttacking)
    {
        yield return cachedCoroutine;
        unit.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("Base Layer.CardZoom");
        enemyUnit.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("Base Layer.CardZoom");
        unit.GetComponent<Image>().enabled = false;
        enemyUnit.GetComponent<Image>().enabled = false;
        copyImage(unit, enemyUnit);
        blurPanel.gameObject.SetActive(true);
        Animator animUnitZoom;
        if (playerIsAttacking) {
            animUnitZoom = playerUnitZoomed.GetComponent<Animator>();
        } else {
            animUnitZoom = enemyUnitZoomed.GetComponent<Animator>();
        }
        animUnitZoom.Play("Base Layer." + anim);
    }
    IEnumerator disableZoom(IEnumerator cachedCoroutine, GameObject unit, GameObject enemyUnit, Vector3 unitLocation, Vector3 unitEnemyLocation, Vector3 unitSize, Vector3 unitEnemySize)
    {
        yield return new WaitForSeconds(2.5f);
        yield return cachedCoroutine;
    }

    IEnumerator zoomOut(GameObject unit, GameObject enemyUnit, Vector3 unitLocation, Vector3 unitEnemyLocation, Vector3 unitSize, Vector3 unitEnemySize)
    {
        float timeOfTravel = 2f; //tiempo hasta llegar a destino
        float currentTime = 0; // tiempo que pasa
        float normalizedValue;

        while (currentTime <= timeOfTravel) {

            unit.GetComponent<Image>().enabled = true;
            enemyUnit.GetComponent<Image>().enabled = true;
            playerUnitZoomed.gameObject.SetActive(false);
            enemyUnitZoomed.gameObject.SetActive(false);
            effects.gameObject.SetActive(false);
            blurPanel.gameObject.SetActive(false);
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / timeOfTravel;

            unit.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(unit.gameObject.transform.localPosition, unitLocation, normalizedValue);
            unit.transform.localScale = Vector3.Lerp(unit.gameObject.transform.localScale, unitSize, normalizedValue);
            enemyUnit.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(enemyUnit.gameObject.transform.localPosition, unitEnemyLocation, normalizedValue);
            enemyUnit.transform.localScale = Vector3.Lerp(enemyUnit.gameObject.transform.localScale, unitEnemySize, normalizedValue);
            yield return null;
        }
    }

    public void goBackToAction()
    {
        backButton.SetActive(false);
        menuObjects.SetActive(false);
        dialogBox.enableActionSelector(true);
        dialogBox.enableDialogText(true);
        dialogBox.enableMoveSelector(false);

    }

}
