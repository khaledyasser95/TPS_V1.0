using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MoveController))]
[RequireComponent(typeof(PlayerState1))]

public class Player : MonoBehaviour {
    [System.Serializable]
    public class MouseInput
    {
        public Vector2 Damping;
        public Vector2 Sensitivity;
        public bool LockMouse;
    }
    [SerializeField] float runSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float crouchSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] AudioControl footSteps;
    [SerializeField] float MinTreshold;
	public PlayerAim playeraim;

    Vector3 previousPosition;
    [SerializeField] MouseInput MouseControl;
    private MoveController m_moveController;
    public MoveController MoveController
    {
        get
        {
            if (m_moveController == null)
                m_moveController = GetComponent<MoveController>();
            return m_moveController;
        }
    }
    //private Crosshair m_Crosshair;
    //public Crosshair Crosshair
    //{
    //    get
    //    {
    //        if (m_Crosshair == null)
    //            m_Crosshair = GetComponentInChildren<Crosshair>();
    //        return m_Crosshair;
    //    }
    //}
    
	private PlayerShoot m_PlayerShoot;
	public PlayerShoot PlayerShoot
	{
		get
		{
			if (m_PlayerShoot == null)
				m_PlayerShoot = GetComponent<PlayerShoot>();
			return m_PlayerShoot;
		}
	}

	private PlayerState1 m_PlayerState;
	public PlayerState1 PlayerState
	{
		get
		{
			if (m_PlayerState == null)
				m_PlayerState = GetComponent<PlayerState1>();
            Debug.Log("Og got", m_PlayerState);
            return m_PlayerState;
		}
       
	}
    Player_ML playerInput;
    Vector2 mouseInput;
	// Use this for initialization
	void Awake () {
        name = this.gameObject.name;
        if (name == "Player")
            playerInput = ML_Manager.Instance.playerInput;
        else
            playerInput = ML_Manager.Instance2.playerInput;

        GameManager.Instance.LocalPlayer = this;
        if (MouseControl.LockMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
        LookAround();

    }

    private void LookAround()
    {
        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.MouseInput.x, 1f / MouseControl.Damping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerInput.MouseInput.y, 1f / MouseControl.Damping.y);


        transform.Rotate(Vector3.up * mouseInput.x * MouseControl.Sensitivity.x);
		 
    	  //Crosshair.LookHeight(mouseInput.y * MouseControl.Sensitivity.y);
		 
		   playeraim.setRotation(mouseInput.y * MouseControl.Sensitivity.y);
    }

    void Move()
    {
        float moveSpeed = runSpeed;

        if (playerInput.IsWalking)
            moveSpeed = walkSpeed;

        if (playerInput.IsRunning)
            moveSpeed = sprintSpeed;

        if (playerInput.IsCrouched)
            moveSpeed = crouchSpeed;

        Vector2 direction = new Vector2(playerInput.Vertical * moveSpeed, playerInput.Horizontal * moveSpeed);
        
        MoveController.Move(direction);

        if (Vector3.Distance(transform.position,previousPosition)> MinTreshold)
            footSteps.Play();
        previousPosition = transform.position;
    }
}
