using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Interactable
{
    [SerializeField] GameObject player;
    [SerializeField] HPBar hpBar;
    Stats myStats;

    private void Start()
    {
        myStats = GetComponent<Stats>();
    }

    public override void Interact()
    {
        base.Interact();
        player.GetComponent<CharacterCombat>().Attack(myStats);
    }

    public void pressButton()
    {
        base.buttonPressed = true;
    }
    public HPBar getHPBar() { return this.hpBar; }
}
