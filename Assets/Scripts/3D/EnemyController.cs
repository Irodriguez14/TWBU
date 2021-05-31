using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    [SerializeField] Transform target;
    [SerializeField] Stats playerStats;
    NavMeshAgent agent;
    CharacterCombat combat;
    [SerializeField] Animator anim;
    public float cd;
    private float actualCD;
    private bool isAttacking = false;
    private Vector3 position;


    void Start()
    {
        actualCD = cd;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
        position = gameObject.transform.GetChild(0).gameObject.transform.localPosition;
    }

    void Update()
    {
        actualCD -= Time.deltaTime;
        if (target != null) {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= lookRadius) {
                anim.SetBool("isRunning", true);
                agent.SetDestination(target.position);
                if (distance <= agent.stoppingDistance) {
                    if (actualCD <= 0) {
                        if (!isAttacking) {
                            //position = transform.position;
                            StartCoroutine(waitForAttack());
                        }
                    }
                    faceTarget();
                }
            } else {
                anim.SetBool("isRunning", false);
            }
        }
    }

    private void faceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    IEnumerator waitForAttack()
    {
        //agent.isStopped = true;
        isAttacking = true;
        anim.SetBool("isAttacking", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("isAttacking", false);
        gameObject.transform.GetChild(0).gameObject.transform.localPosition = position;
        isAttacking = false;
        actualCD = cd;
        combat.Attack(playerStats);
    }

}
