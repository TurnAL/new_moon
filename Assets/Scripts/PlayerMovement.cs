using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;


public class NewBehaviourScript : MonoBehaviour
{
    [Header("velocidade")] 
    public float current_speed;
    public float base_speed = 3;
    public float move_speed = 5;
    public float gravidade = -9.81f;
    private CharacterController _controller;
    private Transform _mycam;
    public float jump_force;

    [Header("Combate")] 
    public float alcance = 2f;
    public int dano = 10;
    public float cooldown = 1;
    private float ultimoataque;
    private LayerMask inimigo;
    
    
    private Animator _animator;
    public bool isgrounded;
    public bool iswalking;
    public bool isrunning;
    public LayerMask collisionmask;
    public Transform pe;
    public AudioSource footsteps;
    public AudioSource running;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>(); //atribui o obejto character controller a variavel
        _mycam = Camera.main.transform;
        _animator = GetComponent<Animator>();
        current_speed = base_speed;
        Cursor.lockState = CursorLockMode.Locked;
        

        

    }

    // Update is called once per frame
    void Update()
    {
     
        Inputhandler();

        

    }

    void Inputhandler()
    {
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movimento = new Vector3(horizontal, 0, vertical);
        movimento = _mycam.TransformDirection(movimento);
        movimento.y = 0;
        
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isrunning = true;
            current_speed = move_speed;
            
        }
        else
        {
            
            isrunning = false;
            current_speed = base_speed;
        }
        _animator.SetBool("Running",isrunning);
        
        isgrounded = Physics.CheckSphere(pe.position,0.3f,collisionmask);
        _animator.SetBool("Grounded",isgrounded);

        if (Input.GetButtonDown("Jump") && isgrounded == true)
        {
            gravidade = jump_force + current_speed / 2;
            _animator.SetTrigger("Saltar");
        }

        if (gravidade > -9.78f)
        {
            gravidade += -9.78f * Time.deltaTime;
        }

        _controller.Move(movimento * Time.deltaTime * current_speed);
        _controller.Move(new Vector3(0, gravidade, 0) * Time.deltaTime);
        if(movimento != Vector3.zero && isgrounded == true)
        {
            if(isrunning == true)
            {
                footsteps.enabled = false; 
                running.enabled = true;
            }
            else
            {
                footsteps.enabled = true;
                running.enabled = false;
            }
        }
        else
        {
            footsteps.enabled = false;
            running.enabled = false;
        }
        if (movimento != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(movimento), Time.deltaTime * 10);
        }
        _animator.SetBool("Andar", movimento != Vector3.zero);

        if (Input.GetButtonDown("Fire1") && Time.time >= ultimoataque + cooldown)
        {
            Atacar();
        }
    }

    public void Atacar()
    {
        ultimoataque = Time.time;
        _animator.SetTrigger("Atacar");
    }
}
