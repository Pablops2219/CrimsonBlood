using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool isWalking { get; private set; } = false;
    public bool isAttacking { get; private set; } = false;
    private Animator animator;
    public PlayerMovement _playerMovement;
    public int weaponEquipped = 0;
    string[,] animationWeaponStates = { {"PlayerWalk" ,"PlayerAnim" ,"PlayerAttack" },
                                        {"PlayerWalkShotgun" ,"PlayerAnimShotgun" ,"PlayerAttackShotgun" }};

    public Transform bulletsSpawnPos;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            WalkingAnimator();
        }
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

        if (Input.GetKeyDown(KeyCode.Q) && !isAttacking)
        {
            StartCoroutine(AttackAnimator());
            
        }
    }

    public void WalkingAnimator()
    {
        isWalking = _playerMovement.m_Movement.magnitude > 0;

        
        //Elige la animacion de caminar segun con que arma este
        animator.Play(animationWeaponStates[weaponEquipped,0]);
        
        if (!isWalking)
        {
            //Sobreescribe la animacion si esta parado
            animator.Play(animationWeaponStates[weaponEquipped,1]);
            
        }
    }

    
    IEnumerator AttackAnimator()
    {
        isAttacking = true;
        animator.Play(animationWeaponStates[weaponEquipped,2]);
        yield return new WaitForSeconds(0.3f);
        isAttacking = false;
        
    }

    public void Shoot()
    {
        
        //GameObject bulletsSpawned = (GameObject)Instantiate(bulletPrefab, bulletsSpawnPos);
    } 
}
