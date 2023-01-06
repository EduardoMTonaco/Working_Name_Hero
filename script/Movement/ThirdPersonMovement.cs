using System.Collections;
using System.Collections.Generic;
using Assets;
using Unity.VisualScripting;
using UnityEngine;

public class ThirdPersonMovement : FindEnemyList
{
    #region Variables
    public CharacterController Controller;
    public float WalkSpeed = 6f;
    public float RunSpeed = 12f;
    public float TurnSmooothTime = 0.1f;
    public Transform Cam;
    public float Gravity = -9.81f;
    public Transform GroundCheck;
    public float GroundDistance = 0.3f;
    public LayerMask GroundMask;
    public float JumpHieght = 2f;
    public PlayerStatus PlayerStatus;
    public MenuController Menu;
    public AudioSource WalkStep;
    public AudioSource RunStep;
    public Animator anim;
    public int xp;

    [HideInInspector]
    public UiHealthBar HealthBar;
    
    private Vector2 velocity;
    private bool isGrounded;
    private float turnSmoothVelocity;
    private float speed;

    private CharactereList CharList;

    #endregion

    void Start()
    {
        CharList = FindObjectOfType<CharactereList>();
        CharList.AllyList.Add(this.gameObject);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        xp = PlayerStatus.stats.Xp;
        Move(GetDirection());

        if (Input.GetButton("Fire1"))
        {
            GetAnimationAttack();
        }
        PlayerStatus.HealthBar.SetHealth(PlayerStatus.stats.GetHealthPoints());
        Jump();
        GetMenu();
        CheckDeath();
    }
    private static Vector3 GetDirection()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        return direction;
    }
    private void Move(Vector3 direction)
    {

        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);
        anim.SetBool("IsGrounded", isGrounded);
        if (isGrounded && velocity.y <= 0)
        {
            velocity.y = -2f;
        }
        if (direction.magnitude > 0f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, TurnSmooothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            if (!isGrounded)
            {
                Controller.Move(moveDir.normalized * speed * Time.deltaTime);
                return;
            }
            if (Input.GetKey(KeyCode.LeftShift) && isGrounded)
            {
                GetAnimationMove(1);
                speed = RunSpeed;
                Controller.Move(moveDir.normalized * speed * Time.deltaTime);
                PlaySteps(WalkStep);
                return;
            }
            GetAnimationMove(0.5f);
            PlaySteps(RunStep);
            speed = WalkSpeed;
            Controller.Move(moveDir.normalized * speed * Time.deltaTime);
            return;
        }
        GetAnimationMove(0);
    }
    private void GetAnimationMove(float value)
    {
        anim.SetFloat("Speed", value, 0.2f, Time.deltaTime);
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(JumpHieght * -2f * Gravity);
            JumpAnimation();
        }
        GravityForce();
    }
    private void JumpAnimation()
    {
        anim.SetTrigger("Jumping");
        GravityForce();
    }
    private void GravityForce()
    {
        velocity.y += Gravity * Time.deltaTime;
        Controller.Move(velocity * Time.deltaTime);
    }
    private void GetAnimationAttack()
    {
        anim.SetTrigger("Attack");
    }
    public void CheckDeath()
    {
        if (PlayerStatus.stats.GetHealthPoints() <= 0)
        {
            Menu.ShowMenu(true, 0);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
    void GetMenu()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (PlayerStatus.stats.GetHealthPoints() >= 0)
            {
                if (Menu.MenuFull.isActiveAndEnabled)
                {
                    Menu.ShowMenu(false, 1);
                    Cursor.lockState = CursorLockMode.Locked;
                }
                else
                {
                    Menu.ShowMenu(true, 0);
                    Cursor.lockState = CursorLockMode.Confined;
                }
            }
        }
    }
    public void PlaySteps(AudioSource audioSource)
    {
        if(!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
