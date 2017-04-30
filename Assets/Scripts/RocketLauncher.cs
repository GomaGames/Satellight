using UnityEngine;
using System.Collections;

public class RocketLauncher : MonoBehaviour {

	public GameObject rocket;
	public RocketController rocketController;
	public AudioSource whooshSound;

	private GameController _gameController;
	private Vector3 _shooterOffset;
  private Vector3 _vrShooterOffset;

	void Start () {
		_gameController = this.GetComponent<GameController>();
		_shooterOffset = new Vector3(0.0f, 0.8f, 1.0f);
  	_vrShooterOffset = new Vector3(0.0f, -0.4f, 1.0f);
	}

	void Update() {
    if (Cardboard.SDK.VRModeEnabled && Cardboard.SDK.Triggered && !_gameController.isGameOver) {

      GameObject vrLauncher = Cardboard.SDK.GetComponentInChildren<CardboardHead>().gameObject;
      LaunchRocketFrom(vrLauncher, _vrShooterOffset);

    } else if (!Cardboard.SDK.VRModeEnabled && Input.GetButtonDown("Fire1") && !_gameController.isGameOver) {

      // This is the same code as before
      Vector3 mouseLoc = Input.mousePosition;
      Vector3 worldMouseLoc = Camera.main.ScreenToWorldPoint(mouseLoc);
      worldMouseLoc.x = rocket.transform.position.x;
      worldMouseLoc.y = rocket.transform.position.y;
      worldMouseLoc.z = rocket.transform.position.z;
      rocket.transform.LookAt(worldMouseLoc);
      LaunchRocketFrom(rocket, _shooterOffset);

    }
  }

	void LaunchRocketFrom(GameObject origin, Vector3 shooterOffset) {

		// This will launch a rocket slightly in front of our origin object.
		// We also have to rotate our model 90 degrees in the x-coordinate.
		Vector3 rocketRotation = origin.transform.rotation.eulerAngles;
		/* rocketRotation.z -= 90.0f; */
		/* rocketRotation.x += 90.0f; */
		/* rocketRotation.y -= 90.0f; */

		Vector3 transformedOffset = origin.transform.rotation * shooterOffset;
		RocketController rocket = ( RocketController )Instantiate(rocketController, origin.transform.position + transformedOffset, Quaternion.Euler(rocketRotation));

    // direction shooting rocket towards
    rocket.target = origin.transform.forward;

		// Play a sound effect!
		whooshSound.Play();

	}

}
