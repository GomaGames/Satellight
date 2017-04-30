using UnityEngine;
using System.Collections;

public class RocketController : MonoBehaviour {

	float starSpeed = 9.0f;
	float rotationSpeed = 8.0f;
	public Vector3 target;

	void Start () {
		this.GetComponent<Rigidbody>().velocity = target * starSpeed;
		this.GetComponent<Rigidbody>().angularVelocity = this.transform.forward * rotationSpeed;
  }

	void OnCollisionEnter (Collision collision) {
		if (collision.collider.tag == "Satellite") {
			collision.collider.GetComponent<SatelliteMover>().DieSoon();
			Destroy(gameObject, 0.9f);
		}
	}

}
