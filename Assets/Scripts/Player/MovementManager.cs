using UnityEngine;
using System.Collections;

public class MovementManager : MonoBehaviour {
	public float startSpeed = 3f;
	public float minSoberSpeed = 2f;
	public float maxFullyDrunkSpeed = 10f;
	public float slowingFactor = 0.5f;
	public float boosterSpeed = 5f;
	public float fuckerSpeed = -3f;
	public float rotationSpeed = 20f;
	public float randomTilt = 0.5f;
	public float maxRotationDiference = 20f;
	public float screenWidth = .1f;

	public Transform model;
	public Transform rotationPoint;
	public Transform fuckerParticlePrefab;
	public Transform boosterParticlePrefab;

	private float minAngleRotation; 
	private float maxAngleRotation;
	private bool drunkCoroutineStarted = false;
	private float drunkRotation;
	
    private bool moving = false;
	private float movementDirection;
	private float movementSpeed;
    public float Speed { get { return movementSpeed; } }

	private Animator animationController;
	private CameraAnimationController cameraAnimController;

	void Start() {
		cameraAnimController = Camera.main.GetComponent<CameraAnimationController> ();
		animationController = model.gameObject.GetComponent<Animator> ();
		minAngleRotation = maxRotationDiference; 
		maxAngleRotation = 360f - maxRotationDiference;
		movementSpeed = startSpeed;
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Booster")) {
			movementSpeed += boosterSpeed;
			cameraAnimController.playerCollectedBooster();
			animationController.SetBool ("BoosterHit", true);
			GameObject instance = CFX_SpawnSystem.GetNextObject(boosterParticlePrefab.gameObject);
			instance.transform.position = transform.position;
		}

		if (other.CompareTag ("Fucker")) {
			movementSpeed += fuckerSpeed;
			animationController.SetBool ("FuckerHit", true);
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
		if (currentRotationAngle < minAngleRotation ||
		    currentRotationAngle > maxAngleRotation ||
		    (currentRotationAngle > maxAngleRotation-10 && rotationOffset > 0) ||
		    (currentRotationAngle < minAngleRotation+10 && rotationOffset < 0)) 
		{

            if (moving)
            {
				Debug.Log(currentRotationAngle);
				Quaternion UProtation = Quaternion.LookRotation(Vector3.forward);
				transform.rotation = Quaternion.Slerp(transform.rotation, UProtation, movementDirection * rotationOffset);
			} else {
				transform.RotateAround (rotationPoint.position, Vector3.forward, rotationOffset );
			}
		}

		animationController.SetBool ("FuckerHit", false);
		animationController.SetBool ("BoosterHit", false);
		animationController.SetBool ("PlayerInput", moving);
		animationController.SetFloat ("drunkRotation", rotationOffset);

		float movementBonus = Mathf.Sign (horizontalOffset) * Mathf.Abs(verticalOffset);
		horizontalOffset = moving ? horizontalOffset + movementBonus : horizontalOffset;
		transform.Translate (new Vector3 (horizontalOffset, verticalOffset, 0), Space.World);
	}

	private void getInput() {
        float speed = Input.GetAxis("Horizontal");
		moving = Mathf.Abs(speed) > 0;
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
