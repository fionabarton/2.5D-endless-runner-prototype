using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Actions performed by player
public enum ePlayerMode {
    idle, walkLeft, walkRight, runLeft, runRight, jumpFull, jumpHalf, attack, hanger, duck
};

// Handles player GameObject functions, including input, movement, and animation.
public class PlayerManager : MonoBehaviour {
    [Header("Set in Inspector")]
    // Transform attached to player, its position is used to check if grounded   
    public Transform            groundCheck;

    // List of layers that player can stand on
    public LayerMask            groundLayer;

    // Prevents the game from ending after the player has collided with a single red obstacle
    public GameObject           shield;

    [Header("Set Dynamically")]
    // Current mode of action performed by player
    public ePlayerMode          mode = ePlayerMode.idle;

    // Movement speeds applied to RigidBody2D velocity
    public float                walkSpeed = 4f;
    private float               jumpSpeed = 13f;

    // Velocity for RigidBody2D when jumping horizontally from hanger
    private Vector3             hangerJumpLeftVelocity = new Vector3(-1, 0.75f, 0);
    private Vector3             hangerJumpRightVelocity = new Vector3(1, 0.75f, 0);

    // Tracks whether player is standing on ground
    public bool                 isGrounded;

    // Tracks whether sprite is facing left or right
    public bool                 facingRight = true;

    // Tracks whether the player is shielded
    public bool                 isShielded = false;

    // Tracks whether the player is able to move via player input
    public bool                 isMobile = false;

    // Location to respawn player
    public Vector3              respawnPos;

    // Unity components
    public Rigidbody2D          rigid;
    private CircleCollider2D    circleColl;
    private Animator            anim;

    // Single instance of this class, which provides global acess from other scripts
    private static PlayerManager _S;
    public static PlayerManager S { get { return _S; } set { _S = value; } }

    // Single instance of whether or not this object already exists
    private static bool         exists;

    void Awake() {
        // Populate singleton with this instance
        S = this;

        // If an instance of this object doesn't already exist...
        if (!exists) {
            // On new scene load, do not destroy this object
            exists = true;
            DontDestroyOnLoad(gameObject);
        } else {
            // Destroy this object
            Destroy(gameObject);
        }

        // Get components attached to this GameObject
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        circleColl = GetComponent<CircleCollider2D>();
    }

    // Move player horizontally at walking speed
    void CheckForWalkInput() {
        if (Input.GetAxisRaw("Attack/Run") <= 0 && Input.GetAxisRaw("Horizontal") > 0f) {
            mode = ePlayerMode.walkRight;
        } else if (Input.GetAxisRaw("Attack/Run") <= 0 && Input.GetAxisRaw("Horizontal") < 0f) {
            mode = ePlayerMode.walkLeft;
        }
    }

    // Move player horizontally at running speed (x2)
    void CheckForRunInput() {
        if (Input.GetAxisRaw("Horizontal") > 0f && Input.GetAxisRaw("Attack/Run") > 0) {
            mode = ePlayerMode.runRight;
        } else if (Input.GetAxisRaw("Horizontal") < 0f && Input.GetAxisRaw("Attack/Run") > 0) {
            mode = ePlayerMode.runLeft;
        }
    }

    // If grounded, propel player upwards
    void CheckForJumpInput(float speed, bool _isGrounded) {
        if (_isGrounded) {
            if (Input.GetButtonDown("Jump")) {
                rigid.velocity = Vector3.up * speed;
                mode = ePlayerMode.jumpFull;
            } else if (Input.GetButtonUp("Jump")) {
                if (rigid.velocity.y > 0) {
                    mode = ePlayerMode.jumpHalf;
                }
            }
        }
    }

    // Propel player horizontally from hanger it was holding onto
    void CheckForJumpLeftOrRightFromHangerInput() {
        if (Input.GetButtonDown("Jump") && Input.GetAxisRaw("Horizontal") < 0f) {
            // Launch player left/up off of hanger
            rigid.velocity = hangerJumpRightVelocity * jumpSpeed;
        } else if (Input.GetButtonDown("Jump") && Input.GetAxisRaw("Horizontal") > 0f) {
            // Launch player right/up off of hanger
            rigid.velocity = hangerJumpLeftVelocity * jumpSpeed;
        }
    }

    // Decrease player collider size
    public void CheckForDuckInput() {
        // On down arrow input...
        if (Input.GetAxisRaw("Vertical") < 0) {
            // Reset player mode to (duck)
            mode = ePlayerMode.duck;

            // Decrease collider radius & offset
            circleColl.radius = 0.2f;
            circleColl.offset = new Vector2(0, -0.3f);
        }
    }

    // Freeze gravity scale & player RigidBody2D velocity
    public void GrabHanger() {
        // Set player mode to (hanger)
        mode = ePlayerMode.hanger;

        // Freeze gravity scale
        rigid.gravityScale = 0;

        // Freeze velocity
        rigid.velocity = Vector3.zero;

        // Play hanging animation clip
        PlayClip("Player_Hanging");
    }

    // Reset gravity scale & player collider size
    public void ReleaseHanger() {
        // Reset player mode and collider
        Idle();

        // Reset gravity scale 
        rigid.gravityScale = 2;
    }

    // Reset player mode and circle collider properties to default values
    public void Idle() {
        // Reset player mode to (idle)
        mode = ePlayerMode.idle;

        // Reset collider radius & offset
        circleColl.radius = 0.35f;
        circleColl.offset = new Vector2(0, -0.15f);
    }

    void Update() {
        if (isMobile) {
            // Depending on current value of mode...
            switch (mode) {
                // ...offer user different sets of actions to be performed via keyboard input
                case ePlayerMode.idle:
                    // (Duck)
                    CheckForDuckInput();
                    // On (shift) pressed and is grounded, (attack)
                    if (Input.GetAxisRaw("Attack/Run") > 0 && isGrounded) {
                        mode = ePlayerMode.attack;
                    }
                    // (Walk)
                    CheckForWalkInput();
                    // (Run)
                    CheckForRunInput();
                    // (Jump up)
                    CheckForJumpInput(jumpSpeed, isGrounded);
                    break;
                case ePlayerMode.walkLeft:
                    // On (right arrow) pressed or (left arrow) released, (idle)
                    if (Input.GetAxisRaw("Horizontal") >= 0) {
                        Idle();
                    }
                    // On (left arrow) and (shift) pressed, (run left)
                    if (Input.GetAxisRaw("Horizontal") < 0f && Input.GetAxisRaw("Attack/Run") > 0) {
                        mode = ePlayerMode.runLeft;
                    }
                    // (Jump up)
                    CheckForJumpInput(jumpSpeed, isGrounded);
                    break;
                case ePlayerMode.walkRight:
                    // On (left arrow) pressed or or (right arrow) released, (idle)
                    if (Input.GetAxisRaw("Horizontal") <= 0) {
                        Idle();
                    }
                    // On (right arrow) and (shift) pressed, (run right)
                    if (Input.GetAxisRaw("Horizontal") > 0f && Input.GetAxisRaw("Attack/Run") > 0) {
                        mode = ePlayerMode.runRight;
                    }
                    // (Jump up)
                    CheckForJumpInput(jumpSpeed, isGrounded);
                    break;
                case ePlayerMode.runLeft:
                    // On (right arrow) pressed or (left arrow) released, (idle)
                    if (Input.GetAxisRaw("Horizontal") >= 0) {
                        Idle();
                    }
                    // On (left arrow) pressed and (shift) released, (walk left)
                    if (Input.GetAxisRaw("Horizontal") < 0f && Input.GetAxisRaw("Attack/Run") <= 0) {
                        mode = ePlayerMode.walkLeft;
                    }
                    // On (right arrow) and (shift) pressed, (run right)
                    if (Input.GetAxisRaw("Horizontal") > 0f && Input.GetAxisRaw("Attack/Run") > 0) {
                        mode = ePlayerMode.runRight;
                    }
                    // (Jump up)
                    CheckForJumpInput(jumpSpeed, isGrounded);
                    break;
                case ePlayerMode.runRight:
                    // On (left arrow) pressed or (right arrow) released, (idle)
                    if (Input.GetAxisRaw("Horizontal") <= 0) {
                        Idle();
                    }
                    // On (right arrow) pressed and (shift) released, (walk right)
                    if (Input.GetAxisRaw("Horizontal") > 0f && Input.GetAxisRaw("Attack/Run") <= 0) {
                        mode = ePlayerMode.walkRight;
                    }
                    // On (left arrow) and (shift) pressed, (run left)
                    if (Input.GetAxisRaw("Horizontal") < 0f && Input.GetAxisRaw("Attack/Run") > 0) {
                        mode = ePlayerMode.runLeft;
                    }
                    // (Jump up)
                    CheckForJumpInput(jumpSpeed, isGrounded);
                    break;
                case ePlayerMode.attack:
                    // On (shift) released, (idle)
                    if (Input.GetAxisRaw("Attack/Run") <= 0) {
                        Idle();
                    }
                    // (Walk)
                    CheckForWalkInput();
                    // (Run)
                    CheckForRunInput();
                    // (Jump up)
                    CheckForJumpInput(jumpSpeed, isGrounded);
                    break;
                case ePlayerMode.jumpFull:
                    if (isGrounded) {
                        // On (shift) pressed, (attack)
                        if (Input.GetAxisRaw("Attack/Run") > 0) {
                            mode = ePlayerMode.attack;
                        }
                        // (Walk) 
                        CheckForWalkInput();
                        // (Run)
                        CheckForRunInput();
                    }
                    break;
                case ePlayerMode.jumpHalf:
                    if (isGrounded) {
                        // On (shift) pressed, (attack)
                        if (Input.GetAxisRaw("Attack/Run") > 0) {
                            mode = ePlayerMode.attack;
                        }
                        // (Walk) 
                        CheckForWalkInput();
                        // (Run)
                        CheckForRunInput();
                    }
                    break;
                case ePlayerMode.hanger:
                    // Jump up off hanger (at reduced speed)
                    CheckForJumpInput(jumpSpeed / 1.25f, true);

                    // On (down arrow) pressed, (release hanger)
                    if (Input.GetAxisRaw("Vertical") < 0f) {
                        ReleaseHanger();
                    }
                    // Jump left or right off hanger
                    CheckForJumpLeftOrRightFromHangerInput();
                    break;
                case ePlayerMode.duck:
                    // On (down arrow) released, (idle)
                    if (Input.GetAxisRaw("Vertical") >= 0) {
                        Idle();
                    }
                    // (Jump up)
                    CheckForJumpInput(jumpSpeed, isGrounded);
                    break;
            }
        }
    }

    void FixedUpdate() {  
            // If player GameObject is active in current scene...
            if (gameObject.activeInHierarchy) {
                // Check whether the player's feet are on the ground
                isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.25f, groundLayer);

                // If player moves in direction it's not facing,
                // swap direction its sprite is facing
                if (Input.GetAxisRaw("Horizontal") > 0 && !facingRight) {
                    SwapDirectionFacing();
                } else if (Input.GetAxisRaw("Horizontal") < 0 && facingRight) {
                    SwapDirectionFacing();
                }

                // Simulate drag, or natural energy loss, on player horizontal movement
                // by multiplying velocity.x of its RigidBody2D by a value slightly
                // less than 1.0f
                Vector2 vel = rigid.velocity;
                vel.x *= 1.0f - 0.5f;
                rigid.velocity = vel;

                // If not grounded, play (jump) animation clip
                if (!isGrounded) {
                    if (mode != ePlayerMode.attack && mode != ePlayerMode.hanger && mode != ePlayerMode.duck) {
                        PlayClip("Player_Jump", 0);
                    }
                }


                // Depending on mode, set RigidBody velocity & animation clip to play
                switch (mode) {
                    case ePlayerMode.idle:
                        if (isGrounded) {
                            // If grounded, play (idle) animation clip
                            PlayClip("Player_Idle");
                        } else {
                            // If not grounded, play (jump) animation clip
                            PlayClip("Player_Jump", 0);
                        }
                        break;
                    case ePlayerMode.walkLeft:
                        // Set velocity to (walk left) speed
                        rigid.velocity = new Vector2(-walkSpeed, rigid.velocity.y);

                        // If grounded, play (walk) animation clip
                        if (isGrounded) {
                            PlayClip("Player_Walk");
                        }
                        break;
                    case ePlayerMode.walkRight:
                        // Set velocity to (walk right) speed
                        rigid.velocity = new Vector2(walkSpeed, rigid.velocity.y);

                        // If grounded, play (walk) animation clip
                        if (isGrounded) {
                            PlayClip("Player_Walk");
                        }
                        break;
                    case ePlayerMode.attack:
                        // Play (attack) animation clip
                        PlayClip("Player_Attack", 0);
                        break;
                    case ePlayerMode.jumpFull:
                        // Set player mode to (idle)
                        Idle();
                        break;
                    case ePlayerMode.jumpHalf:
                        // Set velocity to (jump half) speed
                        rigid.velocity *= 0.5f;

                        // Set player mode to (idle)
                        Idle();
                        break;
                    case ePlayerMode.runLeft:
                        // Set velocity to (run left) speed
                        rigid.velocity = new Vector2(-walkSpeed * 2, rigid.velocity.y);

                        // If grounded, play (run) animation clip
                        if (isGrounded) {
                            PlayClip("Player_Run");
                        }
                        break;
                    case ePlayerMode.runRight:
                        // Set velocity to (run right) speed
                        rigid.velocity = new Vector2(walkSpeed * 2, rigid.velocity.y);

                        // If grounded, play (run) animation clip
                        if (isGrounded) {
                            PlayClip("Player_Run");
                        }
                        break;
                    case ePlayerMode.duck:
                        // Play (duck) animation clip
                        PlayClip("Player_Duck", 0);
                        break;
                }
            }
        
    }

    // Set player scale on x-axis to its additive inverse,
    // which swaps whether the sprite is facing left or right
    void SwapDirectionFacing() {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    // Select and play clip on player animator
    public void PlayClip(string animName = "Player_Idle", int animSpeed = 1) {
        anim.speed = animSpeed;
        anim.CrossFade(animName, 0);
    }

    // Relocate player to starting position
    public void MoveToStartingPosition() {
        transform.position = respawnPos;
    }

    // Activate the player's shield after colliding with a shield item
    public void ActivateShield() {
        shield.SetActive(true);
        isShielded = true;

        // Display text that shield has been activated
        AnnouncerManager.S.DisplayShield();
    }

    // Deactivate the player's shield after colliding with a red obstacle
    public void DeactivateShield() {
        shield.SetActive(false);
        isShielded = false;

        // Display text of a random interjection
        AnnouncerManager.S.DisplayRandomInterjection();
    }

    // End the game and reset all changed values to their default settings
    public void GameOver() {
        if (!isShielded) {
            // Reset shield & starting position
            DeactivateShield();
            MoveToStartingPosition();

            // Display text that the game has ended
            AnnouncerManager.S.DisplayGameOver();

            // Stop game and reactivate main menu
            MainMenu.S.StopGame();
        }
    }

    // Set whether the player is able to move via player input
    public void SetMobility(bool _isMobile) { 
        isMobile = _isMobile;

        // Reduce velocity to 0
        if (!isMobile) {
            rigid.velocity = Vector3.zero;
        }
    }
}