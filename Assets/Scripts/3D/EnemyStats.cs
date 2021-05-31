using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStats : Stats
{
    public override void Die()
    {
        base.Die();
        StartCoroutine(changeScene());
    }

    IEnumerator changeScene() {
        yield return new WaitForSeconds(1f);
        GameManager.gameManager.bossDefeated = true;
        Destroy(gameObject);
        SceneManager.LoadScene("VideoCapitulo2");

    }
}
