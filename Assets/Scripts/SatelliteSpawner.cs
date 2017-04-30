using UnityEngine;
using System.Collections;

/// <summary>
/// Satellite spawner. Creates a new enemy once every few seconds, assuming we're not in a game over state.
/// </summary>
public class SatelliteSpawner : MonoBehaviour {

	public float minZSpawn = -75; // should be -75 to 75
	public float maxZSpawn = 75;
	public float minYSpawn = 25; // should be 25-50
	public float maxYSpawn = 50;
	public float minXSpawn = -75; // should be -75 to 75
	public float maxXSpawn = 75;
	public float launchRangeMinTime;
	public float launchRangeMaxTime;
	public GameObject enemyPrefab;

	private float _nextLaunchTime;
	private GameController _gameController;

	void Start () {
		SetNextLaunch();
		_gameController = this.GetComponent<GameController>();
	}

	void SetNextLaunch() {
		float launchInterval = Random.Range(launchRangeMinTime, launchRangeMaxTime);
		_nextLaunchTime = Time.time + launchInterval;
	}

	void Update () {
		if (Time.time > _nextLaunchTime && !_gameController.isGameOver) {
      Spawn();
			SetNextLaunch();
		}
	}

	void Spawn () {
    Vector3 launchPosition;
	  Quaternion launchRotation;

    // directions, +x, -x, +z, -z
    switch (Random.Range(0, 4))
    {
      case 0: // +x
        launchRotation = Quaternion.Euler(new Vector3(0.0f, Random.Range(45.0f,135.0f), 0.0f));
        launchPosition = new Vector3(minXSpawn, Random.Range(minYSpawn, maxYSpawn), Random.Range(minZSpawn, maxZSpawn));
        break;
      case 1: // -x
        launchRotation = Quaternion.Euler(new Vector3(0.0f, Random.Range(-45.0f,-135.0f), 0.0f));
        launchPosition = new Vector3(maxXSpawn, Random.Range(minYSpawn, maxYSpawn), Random.Range(minZSpawn, maxZSpawn));
        break;
      case 2: // +z
        launchRotation = Quaternion.Euler(new Vector3(0.0f, Random.Range(-45.0f,45.0f), 0.0f));
        launchPosition = new Vector3(Random.Range(minXSpawn, maxXSpawn), Random.Range(minYSpawn, maxYSpawn), minZSpawn);
        break;
      default: // -z
        launchRotation = Quaternion.Euler(new Vector3(0.0f, Random.Range(135.0f,225.0f), 0.0f));
        launchPosition = new Vector3(Random.Range(minXSpawn, maxXSpawn), Random.Range(minYSpawn, maxYSpawn), maxZSpawn);
        break;
    }

    Instantiate(enemyPrefab, launchPosition, launchRotation);

  }


}
