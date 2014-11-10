using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Throw : MonoBehaviour {

	float num = 0;
	public GameObject myo = null;
	public GameObject sphere;
	public float throwSpeed = 1000;
	private bool beenThrown = false;
	public GameObject spawnLocation;

	List<GameObject> musicNotes;
	public int pooledAmount = 5;


	// Use this for initialization
	void Start () {
		musicNotes = new List<GameObject>();

		for(int i = 0; i < pooledAmount; i++){
			GameObject obj = (GameObject)Instantiate(sphere);
			obj.SetActive(false);
			musicNotes.Add(obj);
		}

		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();
		num = thalmicMyo.gyroscope.y;
		beenThrown = false;
	}
	
	// Update is called once per frame
	void Update () {
		ThalmicMyo thalmicMyo = myo.GetComponent<ThalmicMyo> ();

		var gyro = thalmicMyo.gyroscope;
		var calc = Mathf.Abs (gyro.y - num);
		//print (calc);

		if (!beenThrown && calc > 25) {
			print ("throw");

			for(int i = 0; i < musicNotes.Count; i++){
				if(!musicNotes[i].activeInHierarchy){
					musicNotes[i].transform.position = transform.position;
					musicNotes[i].transform.rotation = transform.rotation;
					musicNotes[i].rigidbody.velocity = new Vector3(0, 0, 10);
					musicNotes[i].SetActive(true);
					musicNotes[i].GetComponent<AudioSource>().enabled = false;
					musicNotes[i].GetComponent<AudioSource>().enabled = true;
					beenThrown = true;
					StartCoroutine(pauseThrows(1.0f));

					break;
				}
			}
			//GameObject thrown = Instantiate(sphere, spawnLocation.transform.position, Quaternion.identity) as GameObject;
			//thrown.rigidbody.AddForce(transform.forward * throwSpeed);

		}

//		print (calc);
		if(Input.GetKeyDown(KeyCode.B)){
			for(int i = 0; i < pooledAmount; i++){
				musicNotes[i].SetActive(false);
			}

		}

		if (Input.GetKeyDown (KeyCode.K)) {
			//Instantiate(sphere, new Vector3(0, 1, 0), Quaternion.identity);
			GameObject thrown = Instantiate(sphere, spawnLocation.transform.position, Quaternion.identity) as GameObject;
			thrown.rigidbody.AddForce(transform.forward * throwSpeed);
		}
		num = gyro.y;
	
	}

	IEnumerator pauseThrows(float seconds){
		yield return new WaitForSeconds(seconds);
		beenThrown = false;
	}
}
