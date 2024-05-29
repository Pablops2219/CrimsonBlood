using System.Collections;
using UnityEngine;

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

    public float disparoCooldown = 0.2f; // Cooldown time between shots
    public float atacarCooldown = 1f; 
    private bool cooldownReady = true; // Indicates if the cooldown is ready

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the cooldown is ready
        if (cooldownReady)
        {
            if (Input.GetAxisRaw("Fire1")>0 && weaponEquipped == 1)
            {
                DispararPistola();
                StartCoroutine(StartCooldown(disparoCooldown));
            }
        }
        if (!atacando)
        {
            if (Input.GetButtonDown("Fire1") && weaponEquipped == 0)
            {
                Atacar();
                StartCoroutine(StartCooldownAtaque());
            }
        }

        // Check if the player is walking for animation
        WalkingAnimator();

        // Toggle between weapons
        if (Input.GetKeyDown(KeyCode.E))
        {
            weaponEquipped = (weaponEquipped == 0) ? 1 : 0;
            animator.SetInteger("weaponEquipped",weaponEquipped);
            Debug.Log(weaponEquipped);
        }
    }

    public void WalkingAnimator()
    {
        isWalking = _playerMovement.m_Movement.magnitude > 0;

        if (isWalking && !atacando)
        {
            animator.SetBool("isWalking", true) ;
        }
        else if (!atacando)
        {
            animator.SetBool("isWalking", false) ;
        }
    }

    private void DispararPistola()
    {
        // Instantiate bullet and set its direction
        GameObject balaInst = Instantiate(bala, puntoDisparo.position, puntoDisparo.rotation);
        balaInst.GetComponent<Bala>().SetPuntoDisparo(PlayerMovement.playerMovement.mousePos);
    }

    IEnumerator StartCooldown(float cooldown)
    {
        // Set cooldown flag to false
        cooldownReady = false;
        // Wait for the cooldown duration
        yield return new WaitForSeconds(cooldown);
        // Set cooldown flag to true after cooldown duration
        cooldownReady = true;
    }

    bool atacando;
    private void Atacar()
    {
        Debug.Log("X");
        animator.SetTrigger("atacando");
    }
    
    IEnumerator StartCooldownAtaque()
    {
        atacando = true;
        // Wait for the cooldown duration
        yield return new WaitForSeconds(atacarCooldown);
        atacando = false;
        
    }

}
