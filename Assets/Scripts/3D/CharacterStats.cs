using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : Stats
{
    public GameOverManager gameOver;

    public override void Die()
    {
        base.Die();

        Destroy(gameObject);
        gameOver.gameOver();
    }
}
