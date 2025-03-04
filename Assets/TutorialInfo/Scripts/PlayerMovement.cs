using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;

public class PlayerMovement : MonoBehaviour
{
	private CharacterController cc;

	#region Movement
	[Header("Movement")]

	[Tooltip("Default Move Speed of the Character")]
	[SerializeField] private float moveSpeed = 10.0f;

	[Tooltip("Sprint speed multiplier")]
	[SerializeField] private float sprintMultipier = 1.5f;

	[Tooltip("Max Stamina of the player")]
	[SerializeField] private float MAX_STAMINA = 100;

	[Tooltip("Rate at which stamina decrease for the player")]
	[SerializeField] private float staminaDecreaseRate = 2;

	[Tooltip("Rate at which the player recovers stamina")]
	[SerializeField] private float staminaRecoveryRate = 1;
	private bool canSprint = true;
	private float verticalSpeed;
	private float gravity = -9.8f;
	private float speedMultiplier = 1.0f;
	private float staminaLeft;
	#endregion

	#region Interactables
	[Header("Interactables")]
	[Tooltip("The material for outlining the interactables when hovered")]
	[SerializeField]private Material outline;
	private GameObject lastHit;
	private Material[] defaultMaterials;
	#endregion

	#region Camera
	[Header("Camera")]
	[Tooltip("How quickly the mouse moves the camera")]
	[SerializeField] private float MouseSensitivity;
	private Transform CamTransform;
	private float camRotation = 0f;
	#endregion
	private void Start()
	{
		//Initialzes the heldItem, stamina, Character Controller, and camera
		lastHit = this.gameObject;
		staminaLeft = MAX_STAMINA;
		cc = this.gameObject.GetComponent<CharacterController>();

		//Locks cursor for mouse movement
		Cursor.lockState = CursorLockMode.Locked;
		CamTransform = this.gameObject.GetComponentInChildren<Camera>().gameObject.transform;
	}
	private void Update()
	{
		//Moves our character controller
		Vector3 movement = Vector3.zero;
		float forwardMovement = Input.GetAxis("Vertical") * moveSpeed * speedMultiplier * Time.deltaTime;
		float sideMovement = Input.GetAxis("Horizontal") * moveSpeed * speedMultiplier * Time.deltaTime;
		movement += (transform.forward * forwardMovement) + (transform.right * sideMovement);

		//Vertical stuff isnt really doing much rn but is probably good to keep if we add slopes
		verticalSpeed += (gravity*Time.deltaTime);

		if (cc.isGrounded){verticalSpeed = 0f;}

		movement += (transform.up*verticalSpeed);

		Sprint();

		cc.Move(movement);

		//Debug Message for checking stamina. Can be uncommented to check it
		//Debug.Log(staminaLeft + "Speed multiplier: " + speedMultiplier);


		float mouseInputY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
		camRotation -= mouseInputY;
		camRotation = Mathf.Clamp(camRotation, -90f, 90f);
		CamTransform.localRotation = Quaternion.Euler(camRotation, 0f, 0f);

		float mouseInputX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
		transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, mouseInputX));
		//shoot gun
		if (Input.GetMouseButtonDown(0))
		{
			SimpleRaycast();
		}
		else
        {
			Target();
		}
	}
	private void Sprint()
	{
		if (Input.GetButton("Sprint") && canSprint)
		{
			speedMultiplier = sprintMultipier;
			staminaLeft = Mathf.Clamp(staminaLeft-(staminaDecreaseRate*Time.deltaTime),0,MAX_STAMINA);
			if (staminaLeft==0.0f)
			{
				canSprint = false;
			}
		}
		else
		{
			speedMultiplier = 1.0f;
			staminaLeft = Mathf.Clamp(staminaLeft+(staminaRecoveryRate*Time.deltaTime),0,MAX_STAMINA);
			if (staminaLeft == MAX_STAMINA)
			{
				canSprint = true;
			}
		}
	}

	private void SimpleRaycast()
	{
		RaycastHit hit;
		if (Physics.Raycast(CamTransform.position, CamTransform.forward, out hit))
		{
			GameObject hitObject = hit.collider.gameObject;
			if (hitObject.GetComponent<I_Interactable>()!=null)
			{
				hitObject.GetComponent<I_Interactable>().Interact();
			}
        }

	}
	private void Target()
	{
		RaycastHit over;
		if (Physics.Raycast(CamTransform.position, CamTransform.forward, out over))
		{
			if (over.collider.gameObject!=lastHit)
			{
				if (lastHit.GetComponent<I_Interactable>()!=null)
				{
					lastHit.GetComponent<Renderer>().materials = defaultMaterials;
				}
				lastHit = over.collider.gameObject;
				if(over.collider.gameObject.GetComponent<I_Interactable>()!=null)
				{
					Renderer rend = over.collider.gameObject.GetComponent<Renderer>();
					defaultMaterials = rend.materials;
					Material[] newMaterials = new Material[defaultMaterials.Length+1];
					int i = 0;
					foreach (Material material in defaultMaterials)
					{
						newMaterials[i] = material;
						i++;
					}
					newMaterials[i] = outline;
					rend.materials=newMaterials;
				}
			}
		}

	}
}
