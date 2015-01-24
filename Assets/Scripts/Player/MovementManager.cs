using UnityEngine;
using System.Collections;

public class MovementManager : MonoBehaviour {
	public float startSpeed = 3f;
	public float minSoberSpeed = 10f;
	public float maxFullyDrunkSpeed = 10f;
	public float slowingFactor = 0.5f;
	public float boosterSpeed = 5f;
	public float fuckerSpeed = -3f;
	public float rotationSpeed = 20f;
	public float randomTilt = 0.5f;
	public float maxAngleRotation = 20f;
	public float screenWidth = .1f;

	public Transform rotationPoint;
	public Transform fuckerParticlePrefab;
	public Transform boosterParticlePrefab;

	private float MinAngleRotation; 
	private float MaxAngleRotation;
	private bool drunkCoroutineStarted = false;
	private float drunkRotation;
	
    private bool moving = false;
	private float movementDirection;
	private float movementSpeed;
    public float Speed { get { return movementSpeed; } }
	private CameraAnimationController cameraAnimController;

	void Start() {
		cameraAnimController = Camera.main.GetComponent<CameraAnimationController> ();
		MinAngleRotation = maxAngleRotation; 
		MaxAngleRotation = 360f - maxAngleRotation;
		movementSpeed = startSpeed;
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Booster")) {
			movementSpeed += boosterSpeed;
			cameraAnimController.playerCollectedBooster();
			GameObject instance = CFX_SpawnSystem.GetNextObject(boosterParticlePrefab.gameObject);
			instance.transform.position = transform.position;
		}

		if (other.CompareTag ("Fucker")) {
			movementSpeed += fuckerSpeed;
			GameObject instance = CFX_SpawnSystem.GetNextObject(fuckerParticlePrefab.gameObject);
			instance.transform.position = transform.position;
		}

		Destroy(other.gameObject);
	}

	void Update() {
        
		getInput ();
		float horizontalOffset = getHorizontalMovement();
		float verticalOffset = getVericalMovement();
		float rotationOffset = getRotation();
		startRandomDrunkness ();

		float currentRotationAngle = transform.rotation.eulerAngles.z;
		if (currentRotationAngle < MinAngleRotation ||
		    currentRotationAngle > MaxAngleRotation ||
		    currentRotationAngle < MaxAngleRotation-10 && rotationOffset > 0 ||
		    currentRotationAngle > MinAngleRotation+10 && rotationOffset < 0) 
		{
            if (moving)
            {
				Quaternion UProtation = Quaternion.LookRotation(Vector3.forward);
				transform.rotation = Quaternion.Slerp(transform.rotation, UProtation, movementDirection * rotationOffset);
			} else {
				transform.RotateAround (rotationPoint.position, Vector3.forward, rotationOffset );
			}
		}

		float movementBonus = Mathf.Sign (horizontalOffset) * Mathf.Abs(verticalOffset);
		horizontalOffset = moving ? horizontalOffset + movementBonus : horizontalOffset;
		transform.Translate (horizontalOffset, verticalOffset, 0);
	}

	private void getInput() {
        float speed = Input.GetAxis("Horizontal");
        moving = Mathf.Abs(Input.GetAxis("Horizontal")) > 0;
        movementDirection  = speed;
	}

	private void startRandomDrunkness() {
        float isDrunk = Random.Range(0.0f, 1.0f);
		if (isDrunk > 0.5f && !drunkCoroutineStarted) {
			StartCoroutine("drunknessRotation");
		}
	}

	public IEnumerator drunknessRotation () {
		drunkCoroutineStarted = true;
        drunkRotation = Random.Range(-randomTilt, randomTilt);
		
        for (int i=0; i < 10; i++) {
			yield return new WaitForSeconds (1f);
            drunkRotation +=  Mathf.Sign(drunkRotation) * 1.5f;
		}
		drunkCoroutineStarted = false;
		drunkRotation = 0;
		yield return null;
	}

	private float getRotation() {
        if (moving)
        {
			return movementDirection * rotationSpeed * Time.deltaTime;
		} else {
			return drunkRotation * Time.deltaTime;
		}
	}

	private float getHorizontalMovement() {
        float offset = movementDirection * Time.deltaTime;
		offset += transform.position.x;
        float actualOffset = Mathf.Clamp(offset, -screenWidth / 2 + Camera.main.transform.position.x, 
                                                   screenWidth / 2 + Camera.main.transform.position.x);
		return actualOffset - transform.position.x;
	}

	private float getVericalMovement() {
        movementSpeed -= slowingFactor * Time.smoothDeltaTime;
		movementSpeed = Mathf.Clamp (movementSpeed, minSoberSpeed, maxFullyDrunkSpeed);
		return movementSpeed * Time.smoothDeltaTime;

    }

	void OnGUI() {
        GUI.Label(new Rect( 450,5, 30,30),"" + movementSpeed);
	}
}
