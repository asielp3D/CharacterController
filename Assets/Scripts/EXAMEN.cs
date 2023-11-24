using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXAMEN : MonoBehaviour
{
    // variables para salto y gravedad
    private CharacterController _controller;
    private Transform _camera;
    private float _horizontal;
    private float _vertical;
    [SerializeField] private float _playerSpeed = 5;
    [SerializeField] private float _jumpHeight = 1;
    [SerializeField] private float _pushForce = 5;
    [SerializeField] private float _throwForce = 10;
    private float _gravity = -9.81f;
    private Vector3 _playerGravity;

    // variables para rotacion
    private float turnSmoothVelocity;
    [SerializeField] float turnSmoothTime = 0.1f;

    //variables para sensor
    [SerializeField] private Transform _sensorPosition;
    [SerializeField] private float _sensorRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;
    private bool _isGrounded;

    //Animacion
    private Animator _animator;

    // Start is called before the first frame update
    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
        _camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        Movement();
        Jump();
    }

    void Movement()
    {
        Vector3 direction = new Vector3(_horizontal, 0, _vertical);

        //animaciones
        _animator.SetFloat("VelX", 0);
        _animator.SetFloat("VelZ", direction.magnitude);

        if(direction != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);
            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            _controller.Move(moveDirection.normalized * _playerSpeed * Time.deltaTime);
        }
    }

    void Jump()
    {
        _isGrounded = Physics.CheckSphere(_sensorPosition.position, _sensorRadius, _groundLayer);
        //Animacion salto
        //_animator.Setbool("isJumping",! _isJumping);

        //Groundsensor version Raycast
        /*_isGrounded = Physics.Raycast(_sensorPosition.position, Vector3.down,_sensorRadius, _groundLayer);
        Debug.DrawRay(_sensorPosition.position, Vector3.down * _sensorRadius, Color.red);*/

        if(_isGrounded && _playerGravity.y < 0)
        {
            _playerGravity.y = -2;
        }
        
         if(_isGrounded && Input.GetButtonDown("Jump"))
        {
            _playerGravity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity); //_playerGravity = _jumpHeigth mas facil
        }
        _playerGravity.y += _gravity * Time.deltaTime;
        _controller.Move(_playerGravity * Time.deltaTime);
    }
}

//Buscar animaciones en mixamo


    //RAYCAST

{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            RayTest();
        }
    }
    
    void RayTest()
    {
        //Groundsensor version Raycast (en el Jump())
        /*_isGrounded = Physics.Raycast(_sensorPosition.position, Vector3.down,_sensorRadius, _groundLayer);
        Debug.DrawRay(_sensorPosition.position, Vector3.down * _sensorRadius, Color.red);*/
        
        //Raycast desde la camara
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition)

        //Raycast simple
       /* if(Physics.Raycast(transform.position, transform.forward, 10))
        {
            Debug.Log("Hit");
            Debug.DrawRay(transform.position, transform.forward * 10, Color.green);
        }

        else
        {
            Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
        }*/

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 10))
        {
            Debug.Log(hit.transform.name);
            Debug.Log(hit.transform.position);
            //Destroy(hit.transform.gameObject);
            Box caja = hit.transform.GetComponent<Box>();

            if(hit.transform.GetComponent)

            if(caja != null)
            {
                caja.TakeDamage(shootDamage);
            }
            
        }
    }
}

 Ray ray = Camera.mainCamera.ScreenPointToRay(Input.mousePosition)
 RaycastHit hit;
 if(Physics.Raycast(ray, out hit, Mathf.Infinity))
 {
    if(hit.transform.gameObject.tag == "Ejemplo1")
    {
        Debug.Log("Prueba")
    }

    else if(hit.transform.gameObject.tag == "Ejemplo2")
    {
        Destroy(hit.transform.gameObject)
    }
 }