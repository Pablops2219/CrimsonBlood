using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class PlayerController : MonoBehaviour
{
    
    public bool isWalking { get; private set; } = false;
    private Animator animator;
    public PlayerMovement _playerMovement;
    public int weaponEquipped = 0;
    string[,] animationWeaponStates = { {"PlayerWalk" ,"PlayerAnim" ,"PlayerAttack" },
                                        {"PlayerWalkShotgun" ,"PlayerAnimShotgun" ,"PlayerAttackShotgun" }};

    public GameObject bala; // The bullet prefab
    public Transform puntoDisparo; // The shooting point as a Transform

    
    public float disparoCooldown = 0.5f;
    public bool cooldownReady = true;
    void Start()
    {
        Debug.Log(cooldownReady);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(cooldownReady);
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            Disparar();
        }
        WalkingAnimator();
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (weaponEquipped == 1)
            {
                weaponEquipped = 0;
            }
            else
            {
                weaponEquipped = 1;
            }
        }

        
    }

    public void WalkingAnimator()
    {
        isWalking = _playerMovement.m_Movement.magnitude > 0;

        if(isWalking)
        {
            animator.Play(animationWeaponStates[weaponEquipped,0]);
        }
        if (!isWalking)
        {
            animator.Play(animationWeaponStates[weaponEquipped,1]);
            
        }
    }

    private void Disparar()
    {
        if (!cooldownReady){ return; }
        GameObject balaInst = Instantiate(bala, puntoDisparo.position, puntoDisparo.rotation);
        balaInst.GetComponent<Bala>().SetPuntoDisparo(PlayerMovement.playerMovement.mousePos);
        StartCoroutine(StartCooldown(disparoCooldown));
    }

    IEnumerator StartCooldown(float cooldown)
    {
        cooldownReady = false;
        yield return new WaitForSeconds(cooldown);
        cooldownReady = true;
    }
}
