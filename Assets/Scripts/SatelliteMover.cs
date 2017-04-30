using UnityEngine;
using System.Collections;

public class SatelliteMover : MonoBehaviour {

	public float speedMin;
	public float speedMax;


	enum SatelliteState { Normal, Dying};

	private SatelliteState _state;

	// Use this for initialization.
	void Start () {
		_state = SatelliteState.Normal;
		float thisSpeed = Random.Range(speedMin, speedMax);
		this.GetComponent<Rigidbody>().velocity = transform.forward * thisSpeed;
	}


	/// <summary>
	/// Check and see if our enemy is in a dying state. We need this because occasionally
	/// momentum drives a "dead" enemy through the end zone.
	/// </summary>
	/// <returns><c>true</c> if this enemy is dying; otherwise, <c>false</c>.</returns>
	public bool IsDying() {
		return (_state == SatelliteState.Dying);
	}


	/// <summary>
	/// Remove the game object after a short moment so we can watch it get knocked around.
	/// </summary>
	public void DieSoon() {
		if (_state == SatelliteState.Normal) {
			// Let's let the enemy get knocked back a bit.
			_state = SatelliteState.Dying;
			GameController gameController = FindObjectOfType<GameController>();
			gameController.GotOne();
      this.GetComponent<Rigidbody>().useGravity = true;
			this.GetComponent<AudioSource>().Play();
			Destroy(gameObject, Random.Range(10.0f, 30.0f));
		}
	}



}

