using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThirdPersonMovement : MonoBehaviour
{
    [Header("Character General Control")]
    [SerializeField] CharacterController controller;
    [SerializeField] Transform cam;
    [SerializeField] float speed = 6f;
    [SerializeField] Animator anim;
    [SerializeField] HPBar hpBar;
    [SerializeField] float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    [Header("Touch Movement")]
    [SerializeField] Joystick joystick;
    [SerializeField] TouchField touchField;
    [SerializeField] CinemachineFreeLook vCam;

    [Header("Special Attack VFX")]
    [SerializeField] Transform enemy;
    [SerializeField] ParticleSystem venom;
    [SerializeField] GameObject explosion;
    [SerializeField] float venomCD;

    [Header("Attack Buttons")]
    [SerializeField] Button venomButton;
    [SerializeField] Button kickButton;
    [SerializeField] Button punchButton;


    private bool attacking = false;
    private bool punching = false;
    private bool majic = false;
    private float actualCD;
    private bool isHitted = false;

    private void Start()
    {
        actualCD = 0;
    }

    void FixedUpdate()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (!attacking && !punching && !majic) {
            if (direction.magnitude >= 0.1f) {

                anim.SetBool("isRunning", true);
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDirection.normalized * speed * Time.deltaTime);
            } else {
                anim.SetBool("isRunning", false);
            }
        }

        vCam.m_XAxis.m_InputAxisValue = touchField.TouchDist.x;
        //vCam.m_YAxis.m_InputAxisValue = touchField.TouchDist.y;

        actualCD -= Time.deltaTime;



        if (Input.GetKeyDown("space")) {
            attackKickAnimation();
        }
        if (Input.GetKeyDown("p")) {
            attackPunchAnimation();
        }
        if (Input.GetKeyDown("j")) {
            attackMajicAnimation();
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("3D_Kick") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) {
            attacking = false;
            anim.SetBool("isAttacking", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("3D_Punch") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) {
            punching = false;
            anim.SetBool("isPunching", false);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("3D_MajicAttack") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) {
            majic = false;
            anim.SetBool("isMajic", false);
        }
        if (actualCD <= 0 && !isHitted) {
            venomButton.interactable = true;
            venomButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "";
        } else if (!isHitted) {
            venomButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = actualCD.ToString().Substring(0, 3);
            venomButton.interactable = false;
        } else {
            venomButton.interactable = false;
            venomButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "";
        }
    }

    public void attackKickAnimation()
    {
        if (!punching && !majic) {
            anim.SetTrigger("Kick");
            anim.SetBool("isAttacking", true);
            attacking = true;
        }
    }
    public void attackPunchAnimation()
    {
        if (!attacking && !majic) {
            anim.SetTrigger("Punch");
            anim.SetBool("isPunching", true);
            punching = true;
        }

    }
    public void attackMajicAnimation()
    {
        if (!attacking && !punching) {
            actualCD = venomCD;
            anim.SetTrigger("Majic");
            anim.SetBool("isMajic", true);
            majic = true;
            ParticleSystem venomInstance = Instantiate(venom, enemy.position, Quaternion.identity, gameObject.transform);

            Destroy(venomInstance.gameObject, 1.5f);
            StartCoroutine(waitToExplode());
        }

    }

    IEnumerator waitToExplode()
    {
        Vector3 explosionPos = enemy.position;
        explosionPos.y += 5;
        yield return new WaitForSeconds(1.5f);
        GameObject explosionInstance = Instantiate(explosion, explosionPos, Quaternion.identity, this.gameObject.transform);
        Destroy(explosionInstance, 2.5f);
    }

    public void hitted()
    {
        isHitted = true;
        anim.SetTrigger("Hit");
        venomButton.interactable = false;
        venomButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "";
        kickButton.interactable = false;
        punchButton.interactable = false;
        StartCoroutine(waitHit());
    }

    IEnumerator waitHit()
    {
        yield return new WaitForSeconds(1.5f);
        venomButton.interactable = true;
        kickButton.interactable = true;
        punchButton.interactable = true;
        isHitted = false;
    }

    public HPBar getHPBar() { return this.hpBar; }

}
