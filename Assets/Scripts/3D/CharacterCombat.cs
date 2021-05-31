using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    private Stats myStats;

    public float attackSpeed = 1f;
    private float attackCD = 0f;
    [SerializeField] ThirdPersonMovement playerMovement;
    [SerializeField] HPBar hpPlayer;
    [SerializeField] HPBar hpEnemy;

    private bool kick = false;
    private bool punch = false;
    private bool majic = false;

    private void Start()
    {
        myStats = GetComponent<Stats>();
    }

    public void Attack(Stats targetStats)
    {
        if(attackCD <= 0f) {
            if(kick) {
                playerMovement.attackKickAnimation();
                kick = false;
            }
            if (punch) {
                playerMovement.attackPunchAnimation();
                punch = false;
            }
            if (majic) {
                playerMovement.attackMajicAnimation();
                majic = false;
            }
            targetStats.takeDmg(myStats.getDamage());
            if (targetStats.getIsCharacter()) {
                StartCoroutine(targetStats.gameObject.GetComponent<ThirdPersonMovement>().getHPBar().setHPAnim((float)targetStats.getCurrentHealth() / targetStats.getMaxHP()));
                targetStats.gameObject.GetComponent<ThirdPersonMovement>().hitted();
            } else {
                StartCoroutine(targetStats.gameObject.GetComponent<Enemy>().getHPBar().setHPAnim((float)targetStats.getCurrentHealth() / targetStats.getMaxHP()));
            }
            attackCD = 1f / attackSpeed;
        }
        
    }

    private void Update()
    {
        attackCD -= Time.deltaTime;
    }

    private void pressKickButton(){ kick = true; }
    private void pressPunchButton() { punch = true; }
    private void pressMajicButton() { majic = true; }
}
