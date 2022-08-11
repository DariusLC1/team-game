using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("----- Components -----")]
    [SerializeField] CharacterController controller;

    [Header("----- Player Stats -----")]
    [Range(1, 10)][SerializeField] float playerSpeed;
    [Range(1, 4)][SerializeField] float sprintMultiplyer;
    [Range(8, 18)][SerializeField] float jumpHeight;
    [Range(15, 30)][SerializeField] float gravity;
    [Range(1, 3)][SerializeField] int jumpsMax;
    [Range(0, 10)]public int HP;


    private Vector3 playerVelocity;
    Vector3 move = Vector3.zero;
    int timesJumped;
    float playerSpeedOriginal;
    bool isSprinting = false;
    int HPOrig;


    // Start is called before the first frame update
    void Start()
    {
        HPOrig = HP;
        playerSpeedOriginal = playerSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            takeDamage(1);
        }

        playerMovement();
        Sprint();
    }

    void playerMovement()
    {
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            timesJumped = 0;
        }

        //get input from Unity input system
        move = (transform.right * Input.GetAxis("Horizontal")) + (transform.forward * Input.GetAxis("Vertical"));

        // add our move vector to character controller
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && timesJumped < jumpsMax)
        {
            playerVelocity.y = jumpHeight;
            timesJumped++;
        }

        playerVelocity.y -= gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void Sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            isSprinting = true;
            playerSpeed = playerSpeed * sprintMultiplyer;
        }
        if (Input.GetButtonUp("Sprint"))
        {
            isSprinting = false;
            playerSpeed = playerSpeedOriginal;
        }
    }

    public void takeDamage(int dmg)
    {
        HP -= dmg;
        

        if (HP <= 0)
        {
            death();


        }
    }

    public void death()
    {
        gameManager.instance.cursorLockPause();
        gameManager.instance.menuOpen = gameManager.instance.playerDeadMenu;
        gameManager.instance.menuOpen.SetActive(true);
    }

    public void resetHP()
    {
        HP = HPOrig;
    }



}
