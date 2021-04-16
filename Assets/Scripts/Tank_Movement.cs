using UnityEngine;

public class Tank_Movement : MonoBehaviour
{
    public int m_PlayerNumber = 1;         
    public float m_Speed = 12f;            
    public float m_TurnSpeed = 180f;       
    public AudioSource m_MovementAudio;    
    public AudioClip m_EngineIdling;       
    public AudioClip m_EngineDriving;      
    public float m_PitchRange = 0.2f;

    private string m_MovementAxisName;     
    private string m_TurnAxisName;         
    private Rigidbody m_Rigidbody;         
    private float m_MovementInputValue;    
    private float m_TurnInputValue;        
    private float m_OriginalPitch;         


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable ()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable ()
    {
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;

        m_OriginalPitch = m_MovementAudio.pitch;
    }


    private void Update()
    {
        // Store the player's input and make sure the audio for the engine is playing.
		m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
		m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

		EngineAudio();
    }


    private void EngineAudio()
    {
        // Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.
		if (Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f)
		{
			// If Tank is not moving.
			if (m_MovementAudio.clip == m_EngineDriving)
			{
				m_MovementAudio.clip = m_EngineIdling;
				m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
				m_MovementAudio.Play();
			}
		}
		else
		{
			// If Tank is moving.
			if (m_MovementAudio.clip == m_EngineIdling)
			{
				m_MovementAudio.clip = m_EngineDriving;
				m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
				m_MovementAudio.Play();
			}
		}
    }


    private void FixedUpdate()
    {
        // Move and turn the tank.
		Move();
		Turn();
    }


    private void Move()
    {
        /* ORIGINAL METHOD

        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);

       */

        transform.Translate(0f,0f,m_MovementInputValue * m_Speed * Time.deltaTime);

       
       /* THIS METHOD CANT BE USED AS TANK KA DIRN KIDHAR BHI RAHEGA WO MOVE KAREGA SIRF Z DIRN ME HI,
       HAMEIN TANK KO USKE CANON K ALONG MOVE KARANA HAI

       Vector3 position=transform.position;
       position.z+=m_MovementInputValue*m_Speed*Time.deltaTime;
       transform.position=position;
       */
    }


    private void Turn()
    {
        /*     ORIGINAL METHOD

		float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
		m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);

        */

        transform.Rotate(0f,m_TurnInputValue * m_TurnSpeed * Time.deltaTime,0f);
        
    }
}