using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Staircases;
// git push https://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@github.com/Syzygy1974/icebreaker.git

public enum GroundType
{
    None,
    Soft,
    Hard,
    Staircases
}


public struct WhatDoIHaveUnder
{
    public GroundType Ground;
    public bool inContact;
}

public class PlayerController2D : MonoBehaviour
{
    readonly Vector3 flippedScale = new Vector3(-1, 1, 1);
    readonly Quaternion flippedRotation = new Quaternion(0, 0, 1, 0);

    [Header("Character")]
    [SerializeField] Animator animator = null;
    [SerializeField] Transform puppet = null;
    // [SerializeField] CharacterAudio audioPlayer = null;

    // [Header("Equipment")]
    // [SerializeField] Transform handAnchor = null;
    // [SerializeField] SpriteLibrary spriteLibrary = null;

    [Header("Movement")]
    [SerializeField] bool inertiaX = false;                         // Inercia en el eje X Si o No.
    [SerializeField] bool jumping = false;                          // Se permite saltar Si o No.
    [SerializeField] float acceleration = 0.0f;
    [SerializeField] float maxSpeed = 0.0f;
    [SerializeField] float jumpForce = 0.0f;
    [SerializeField] float minFlipSpeed = 0.1f;
    [SerializeField] float jumpGravityScale = 1.0f;
    [SerializeField] float fallGravityScale = 1.0f;
    [SerializeField] float groundedGravityScale = 1.0f;
    [SerializeField] bool resetSpeedOnLand = false;
    bool autowalk = false;

    private Rigidbody2D controllerRigidbody;
    public static Collider2D controllerCollider { get; set; }
    private LayerMask softGroundMask;
    private LayerMask hardGroundMask;
    private LayerMask staircasesGroundMask;
    private GameObject debugText;
    private GameObject debugText2;
    private GameObject debugText3;

    private Vector2 movementInput;
    private bool jumpInput;
    private bool GreetingInput;

    // Optional collisions.
    // public static bool StaircasesColission { get; set; } =  false;

    // private Vector2 prevVelocity;
    private GroundType groundType;
    private WhatDoIHaveUnder groundType2;
    private bool isFlipped;
    private bool isJumping;
    private bool isFalling;

    private bool isGreeting;
    private bool testigo = false;
    public static bool isWalkingRight  { get; set; } =  false;
    public static bool isWalkingLeft  { get; set; } =  false;

    private int animatorWalking;
    private int animatorGroundedBool;
    private int animatorRunningSpeed;
    private int animatorJumpTrigger;

    private int contador = 0;

    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite GreetingSprite;
    bool enLaEscalera = false;
    bool collisionConElFuckingAccess = false;
    public bool CanMove { get; set; }

    public static StaircasesManager staircasesManager = new StaircasesManager();


    public void UseElevator(ElevatorData data) {
        Debug.Log ("Intenta usar el Asensor.");
        data.elevator.SendMessage("Call");
    }

    // ======================== USE RECEIVER ========================
    // Esta funcion es llamada por "Use Button" y recive la informacion sobre la escalera actual.
    // Pone al personaje en movimiento en direccion hacia la escalera y envia los datos
    // de la escalera al staircasesManager, mediante la funcion useButton().
    // =============================================================
    public void UseStaircases( StaircasesData data ) {

        // Informa al staircasesManeger que se pulso el boton "Use".
        staircasesManager.useButton(data);

        // Calcula la direccion en la que se movera el personaje para acceder a la escalera
        // e inicia el movimiento en esa direccion.
        Vector2 relativePosition;
        relativePosition = (data.initialPosition - controllerRigidbody.position);
        if (relativePosition.x > 0) { 
            isWalkingRight = true;
        }
        else {
            isWalkingLeft = true;
        }
    }

    void ChangeSprite()
    {
        spriteRenderer.sprite = GreetingSprite;
    }

    public void Greeting()
    {
        isGreeting = true;
    }

    public void WalkingRightDown()
    {
        isWalkingRight = true;
        isWalkingLeft = false;
    }

    public void WalkingRightUp()
    {
        isWalkingRight = false;
    }

    public void WalkingLeftDown()
    {
        isWalkingLeft = true;
        isWalkingRight = false;
    }

    public void WalkingLeftUp()
    {
        isWalkingLeft = false;
    }

    void Awake()
    {

        //spriteRenderer = GetComponent<RightHand>();

        debugText = GameObject.Find("DebugText");
        debugText2 = GameObject.Find("DebugText2");
        debugText3 = GameObject.Find("DebugText3");
        // debugText.SendMessage ("DisplayText", "Julieta");

        // Inicializa todos los componentes que necesito del engine.
        controllerRigidbody = GetComponent<Rigidbody2D>();
        controllerCollider = GetComponent<Collider2D>();

        softGroundMask = LayerMask.GetMask("Ground Soft");
        hardGroundMask = LayerMask.GetMask("Ground Hard");
        staircasesGroundMask = LayerMask.GetMask("Staircases");

        animator = GetComponent<Animator>();
        animatorWalking = Animator.StringToHash("Walking");
        // animatorGroundedBool = Animator.StringToHash("Grounded");
        // animatorRunningSpeed = Animator.StringToHash("RunningSpeed");
        // animatorJumpTrigger = Animator.StringToHash("Jump");

        CanMove = true;
        Physics2D.IgnoreLayerCollision(13, 0, true);
    }


void Update()
    {
        // Control de desplazamiento por teclado.
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        if (horizontalInput != 0) {
            Vector2 moveHorizontal;
            moveHorizontal = new Vector2(0f, 0f);
            if (horizontalInput > 0) moveHorizontal = new Vector2(15f, 0f);
            if (horizontalInput < 0) moveHorizontal = new Vector2(-15f, 0f);
            movementInput = moveHorizontal;
        }

        if (isWalkingRight)
        {
            Vector2 moveHorizontal;
            moveHorizontal = new Vector2(15f, 0f);
            movementInput = moveHorizontal; //??????????????????????????????????? LO USO PARA EL JUMP Y/O SUBIR/BAJAR O LO SACO?
        }

        if (isWalkingLeft)
        {
            Vector2 moveHorizontal;
            // float horizontalInput = Input.GetAxisRaw("Horizontal");
            moveHorizontal = new Vector2(-15f, 0f);
            movementInput = moveHorizontal; //??????????????????????????????????? LO USO PARA EL JUMP Y/O SUBIR/BAJAR O LO SACO?
        }

        // Salto.
        // Unicamente si "jumping" es true, verifica el boton de salto.
        if (jumping) {
            if (Input.GetButtonDown("Jump")) {
                jumpInput = true;
            }
        }

        // Solo lee la entrada de "Fire1" (Greeting) si la animacion de Greeting no esta reproduciendose.
        if (!(animator.GetCurrentAnimatorStateInfo(1).IsName("Greeting")))
        {
            //if (Input.GetButtonDown("Fire1")) {
            if (isGreeting)
            {
                GreetingInput = true;
            }
        }
        isGreeting = false;
    }

void FixedUpdate()
    {
        UpdateGrounding();
        UpdateStaircasesCollision();
        UpdateFriction();
        UpdateVelocity();
        UpdateDirection();
        // if (jumping) { UpdateJump(); }
        // UpdateGreeting();
        // UpdateGravityScale();

        // PARA QUE SE USA???
        // prevVelocity = controllerRigidbody.velocity;
    }


private void UpdateFriction() {
    if (movementInput == new Vector2 (0,0) && groundType2.Ground == GroundType.Staircases && controllerRigidbody.sharedMaterial.friction == 0) {
        controllerCollider.enabled =  false;
        controllerRigidbody.sharedMaterial.friction = 2f;
        controllerCollider.enabled =  true;
    } else if (movementInput != new Vector2 (0,0) && controllerRigidbody.sharedMaterial.friction != 0)
    {
        controllerCollider.enabled =  false;
        controllerRigidbody.sharedMaterial.friction = 0f;
        controllerCollider.enabled =  true;

    }
}


private void UpdateStaircasesCollision() {
    if (groundType2.Ground == GroundType.Staircases) {
        staircasesManager.groundStaircases();
    }
    else {
        staircasesManager.groundNoStaircases();
    }
}


private void UpdateVelocity()
    {

        Vector2 velocity = controllerRigidbody.velocity;

        // Calcula aceleracion.
        velocity += movementInput * acceleration * Time.fixedDeltaTime;
        if (!inertiaX) { velocity.x = movementInput.normalized.x * 15.0f; }
        // Setea la velocidad de la animacion "Walking" en funcion de la velocidad de movimiento en x.
        animator.SetFloat("WalkingSpeed", velocity.x/5);

        // Clamp horizontal speed.  ???????????????
        // velocity.x = Mathf.Clamp(velocity.x, -maxSpeed, maxSpeed);

        if (groundType2.Ground == GroundType.Staircases) {
            velocity.x = movementInput.normalized.x * 10.0f;
            if (groundType2.inContact == false) {
                velocity.y = -20f;
            }
        }

        // Una vez aplicada de aceleracion de movementInput, lo dejo en 0.
        movementInput = Vector2.zero;

        // Asigna la velocidad al RigidBody.
        controllerRigidbody.velocity = velocity;
        
        if (controllerRigidbody.velocity.x >0 || controllerRigidbody.velocity.y >0) {
            Debug.DrawLine(transform.position, transform.position * controllerRigidbody.velocity, Color.red);
        }
        else if (controllerRigidbody.velocity.x <0 || controllerRigidbody.velocity.y <0) {
            Debug.DrawLine(transform.position, transform.position * -controllerRigidbody.velocity, Color.red);
        }


        if (velocity.x != 0 ){
             animator.SetBool("Walking", true);
        }
        else { 
            animator.SetBool("Walking", false); 
        }


        // Update animator running speed
        // var horizontalSpeedNormalized = Mathf.Abs(velocity.x) / maxSpeed;
        // animator.SetFloat(animatorWalking, horizontalSpeedNormalized);
        // animator.SetFloat(animatorRunningSpeed, horizontalSpeedNormalized);

        // Play audio
        // audioPlayer.PlaySteps(groundType, horizontalSpeedNormalized);
    }


private void UpdateDirection()
    {
        // Pon el personaje en la direccion de su velocidad (flip)
        if (controllerRigidbody.velocity.x > minFlipSpeed && isFlipped)
        {
            isFlipped = false;
            puppet.localScale = Vector3.one;
        }
        else if (controllerRigidbody.velocity.x < -minFlipSpeed && !isFlipped)
        {
            isFlipped = true;
            puppet.localScale = flippedScale;
        }

    }


private void UpdateJump()
    {
        // Setea flag de caida.
        if (isJumping && controllerRigidbody.velocity.y < 0)
            isFalling = true;

        // Jump
        if (jumpInput && groundType != GroundType.None)
        {
            controllerRigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);

            // Set animator
            // animator.SetTrigger(animatorJumpTrigger);

            // Una vez aplicada de aceleracion de jumpInput, lo dejo en 0.
            jumpInput = false;

            // Setea flag de salto.
            isJumping = true;

            // Play audio
            // audioPlayer.PlayJump();
        }
    }

    private void UpdateGreeting()
    {
        if (GreetingInput) {
            // debugText.SendMessage ("DisplayText", "Saluda...");
            // animator.SetBool("Greeting", true);
            animator.SetTrigger("Greeting");
            // animator.Play("Greeting");
            GreetingInput = false;
            //ChangeSprite();
        }
    }

void UpdateGrounding() {
    Vector2 position = transform.position;
    Vector2 direction = Vector2.down;
    float distance = 5f;

    RaycastHit2D hit = Physics2D.Raycast(controllerRigidbody.position, direction, distance, staircasesGroundMask);
    if (hit.collider != null) {
        groundType2.Ground = GroundType.Staircases;
        if (controllerCollider.IsTouchingLayers(staircasesGroundMask)) {
            groundType2.inContact = true;
        }
        else {
            groundType2.inContact = false;
        }
        return;
    }

    hit = Physics2D.Raycast(controllerRigidbody.position, direction, distance, softGroundMask);
    if (hit.collider != null) {
        groundType2.Ground = GroundType.Soft;
        if (controllerCollider.IsTouchingLayers(softGroundMask)){
            groundType2.inContact = true;
        }
        else
            groundType2.inContact = false;
        return;
    }

    groundType2.Ground = GroundType.None;
    groundType2.inContact = false;
}

}
