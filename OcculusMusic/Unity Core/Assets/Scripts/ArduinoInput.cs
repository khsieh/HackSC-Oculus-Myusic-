using UnityEngine;
using System;
using System.Collections;
using System.IO.Ports;

public class ArduinoInput : MonoBehaviour {
	SerialPort port = new SerialPort ("COM4", 9600);
	const int min_value = 300;
	public int currentSet = 0;

	// Use this for initialization
	void Start () {
		port.Open ();
	}
	
	// Update is called once per frame
	void Update () {
		string value = port.ReadLine ();

		var data = Convert.ToInt32 (value);
		currentSet = normalize (data - min_value);
	
	}

	int normalize(int reading){
		if (reading < 0) {
			return 0;			
		} else {
			return mathMap (reading, 0, (1024 - min_value), 0, 1024);
		}

	}

	int mathMap(int x, int in_min, int in_max, int out_min, int out_max){
		return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min; 
	}	
}
