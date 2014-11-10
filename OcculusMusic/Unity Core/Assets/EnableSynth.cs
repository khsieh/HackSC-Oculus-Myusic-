using UnityEngine;
using System.Collections;

public class EnableSynth : MonoBehaviour {

	private OcculusSynth synth;

	// Use this for initialization
	void Start () {
		synth = GetComponent<OcculusSynth>();
		synth.enabled = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftShift)){
			synth.enabled = true;
		}
		else if(Input.GetKeyUp(KeyCode.LeftShift)){
			synth.enabled = false;
		}
	
	}
}
