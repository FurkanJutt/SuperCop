using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [Header("Hero Info")]
    [SerializeField] float heroHealth = 100f;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float jumpForce = 3f;

    float movementX;
    bool isGrounded = true;
    bool nearFire = false;
    bool satNearFire = false;

    private Animator _animator;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;

    // fixed Variables
    private const string IS_RUNNING = "Running";
    private const string IS_JUMPING = "Jumping";
    private const string PUNCH = "Punch";
    private const string OPENBAG = "OpenBag";
    private const string SIT_DOWN_TO_FIRE = "SitDownToFire";
    private const string DEATH = "Death";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _HeroMovement();
        _AnimateHero();
        _HeroJump();
        _HeroAttack();
        _OpenBag();
        _SitToFire();
    }
    
    public void SetRunSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    private void _SitToFire()
    {
        if (satNearFire && Input.GetKeyDown(KeyCode.LeftControl))
        {
            _animator.SetBool(SIT_DOWN_TO_FIRE, false);
            satNearFire = false;
        }
        else if(nearFire && Input.GetKeyDown(KeyCode.LeftControl))
        {
            satNearFire = true;
            _animator.SetBool(SIT_DOWN_TO_FIRE, true);
        }
    }

    private void _OpenBag()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && !_animator.GetBool(IS_RUNNING) && isGrounded)
        {
            _animator.SetTrigger(OPENBAG);
        }
    }

    private void _HeroMovement()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movementX, 0, 0) * Time.deltaTime * moveSpeed;
        
    }

    private void _HeroJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            _rigidbody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            _animator.SetBool(IS_JUMPING, true);
        }
    }

    private void _HeroAttack()
    {
        if (Input.GetButtonDown("Fire1") && isGrounded)
        {
            _animator.SetTrigger(PUNCH);
        }
    }

    private void _AnimateHero()
    {
        if (movementX > 0 && isGrounded)
        {
            transform.eulerAngles = new Vector3(0, 0f, 0);
            _animator.SetBool(IS_RUNNING, true);
        }
        else if (movementX < 0 && isGrounded)
        {
            transform.eulerAngles = new Vector3(0, 180f, 0);
            _animator.SetBool(IS_RUNNING, true);
        }
        else
        {
            _animator.SetBool(IS_RUNNING, false);
        }
    }

    private void _HeroDeath()
    {
        if (heroHealth <= 0)
        {
            _animator.SetTrigger(DEATH);
        }
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            _animator.SetBool(IS_JUMPING, false);
        }
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.gameObject.name);
        if (collider.gameObject.CompareTag("Fire"))
        {
            nearFire = true;
        }
        if (collider.GetComponent<CircleCollider2D>() && collider.gameObject.CompareTag("Fire"))
        {
            heroHealth -= 10f;
            _spriteRenderer.color = Color.red;
            _HeroDeath();
            yield return new WaitForSeconds(0.5f);
            _spriteRenderer.color = Color.white;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Fire"))
        {
            nearFire = false;
        }
    }
}
