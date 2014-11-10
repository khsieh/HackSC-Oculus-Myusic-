using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CSharpSynth.Effects;
using CSharpSynth.Sequencer;
using CSharpSynth.Synthesis;
using CSharpSynth.Midi;

[RequireComponent (typeof(AudioSource))]
public class OcculusSynth : MonoBehaviour
{
	//Public
	//Check the Midi's file folder for different songs
	public string midiFilePath = "Midis/Groove.mid";
	//Try also: "FM Bank/fm" or "Analog Bank/analog" for some different sounds
	public string bankFilePath = "GM Bank/gm";
	public int bufferSize = 1024;
	public int midiNote = 60;
	public int midiNoteVolume = 100;
	public int midiInstrument = 75;
	//Private 
	private float[] sampleBuffer;
	private float gain = 1f;
	private MidiSequencer midiSequencer;
	private StreamSynthesizer midiStreamSynthesizer;
	
	private float sliderValue = 1.0f;
	private float maxSliderValue = 127.0f;
	
	public ArduinoInput sample;
	
	const int numNotes = 6;
	bool[] samOn = new bool[numNotes];
	private bool recentlyPlayed;
	// Awake is called when the script instance
	// is being loaded.
	void Awake ()
	{
		sample = GameObject.Find("Arduino Input").GetComponent<ArduinoInput>();
		midiStreamSynthesizer = new StreamSynthesizer (44100, 2, bufferSize, 40);
		sampleBuffer = new float[midiStreamSynthesizer.BufferSize];		
		
		midiStreamSynthesizer.LoadBank (bankFilePath);
		
		midiSequencer = new MidiSequencer (midiStreamSynthesizer);
		//midiSequencer.LoadMidi (midiFilePath, false);
		//These will be fired by the midiSequencer when a song plays. Check the console for messages
		midiSequencer.NoteOnEvent += new MidiSequencer.NoteOnEventHandler (MidiNoteOnHandler);
		midiSequencer.NoteOffEvent += new MidiSequencer.NoteOffEventHandler (MidiNoteOffHandler);
		for (int i  = 0; i < numNotes; ++i) {
			samOn[i] = false;
		}

		recentlyPlayed = false;
	}
	
	// Start is called just before any of the
	// Update methods is called the first time.
	void Start ()
	{
		recentlyPlayed = false;
		
	}
	// Update is called every frame, if the
	// MonoBehaviour is enabled.
	void Update ()
	{
		var sam = (sample.currentSet % (numNotes - 1));
		
		//print (sam);
		//midiStreamSynthesizer.NoteOn (1, midiNote, midiNoteVolume, midiInstrument)
		
		//Demo of direct note output

		//midiStreamSynthesizer.NoteOn (1, midiNote, midiNoteVolume, midiInstrument);

		if(!recentlyPlayed){
			print (sam);
			recentlyPlayed = true;

			if (samOn[0] && sam == 1) {
				midiStreamSynthesizer.NoteOn (1, midiNote, midiNoteVolume, midiInstrument);
				midiStreamSynthesizer.NoteOn (1, midiNote + 2, midiNoteVolume, midiInstrument);
				midiStreamSynthesizer.NoteOn (1, midiNote + 4, midiNoteVolume, midiInstrument);
				samOn[0] = !samOn[0];
				StartCoroutine(delayMusic(1/8f));
			}

			else if (!samOn[0] && sam == 1) {
				midiStreamSynthesizer.NoteOff (1, midiNote);
				midiStreamSynthesizer.NoteOff (1, midiNote + 2);
				midiStreamSynthesizer.NoteOff (1, midiNote + 4);
				samOn[0] = !samOn[0];
			}
			else if (samOn[1] && sam == 2) {
				midiStreamSynthesizer.NoteOn (1, midiNote + 1, midiNoteVolume, midiInstrument);
				midiStreamSynthesizer.NoteOn (1, midiNote + 3, midiNoteVolume, midiInstrument);
				midiStreamSynthesizer.NoteOn (1, midiNote + 5, midiNoteVolume, midiInstrument);
				samOn[1] = !samOn[1];
				StartCoroutine(delayMusic(1/4f));
			}
			else if (!samOn[1] && sam == 2) {
				midiStreamSynthesizer.NoteOff (1, midiNote + 1);
				midiStreamSynthesizer.NoteOff (1, midiNote + 3);
				midiStreamSynthesizer.NoteOff (1, midiNote + 5);
	            samOn[1] = !samOn[1];
	        }
	        
			else if (samOn[2] && sam == 3) {
				midiStreamSynthesizer.NoteOn (1, midiNote + 2, midiNoteVolume, midiInstrument);
				midiStreamSynthesizer.NoteOn (1, midiNote + 4, midiNoteVolume, midiInstrument);
				midiStreamSynthesizer.NoteOn (1, midiNote + 6, midiNoteVolume, midiInstrument);
				samOn[2] = !samOn[2];
				StartCoroutine(delayMusic(1/8f));
			}
			else if (!samOn[2] && sam == 3) {
				midiStreamSynthesizer.NoteOff (1, midiNote + 2);
				midiStreamSynthesizer.NoteOff (1, midiNote + 4);
				midiStreamSynthesizer.NoteOff (1, midiNote + 6);
				samOn[2] = !samOn[2];
			}
			else if (samOn[3] && sam == 4) {
				midiStreamSynthesizer.NoteOn (1, midiNote + 3, midiNoteVolume, midiInstrument);
				midiStreamSynthesizer.NoteOn (1, midiNote + 5, midiNoteVolume, midiInstrument);
				midiStreamSynthesizer.NoteOn (1, midiNote + 7, midiNoteVolume, midiInstrument);
				samOn[3] = !samOn[3];
				StartCoroutine(delayMusic(1/3f));
			}
			else if (!samOn[3] && sam == 4) {
				midiStreamSynthesizer.NoteOff (1, midiNote + 3);
				midiStreamSynthesizer.NoteOff (1, midiNote + 5);
				midiStreamSynthesizer.NoteOff (1, midiNote + 7);
				samOn[3] = !samOn[3];
			}

			else if (samOn[4] && sam == 5) {
				midiStreamSynthesizer.NoteOn (1, midiNote + 4, midiNoteVolume, midiInstrument);
				midiStreamSynthesizer.NoteOn (1, midiNote + 6, midiNoteVolume, midiInstrument);
				midiStreamSynthesizer.NoteOn (1, midiNote + 8, midiNoteVolume, midiInstrument);
				samOn[4] = !samOn[4];
				StartCoroutine(delayMusic(1/6f));
			}
			else if (!samOn[4] && sam == 5) {
				midiStreamSynthesizer.NoteOff (1, midiNote + 4);
				midiStreamSynthesizer.NoteOff (1, midiNote + 6);
				midiStreamSynthesizer.NoteOff (1, midiNote + 8);
				samOn[4] = !samOn[4];
			}

			StartCoroutine(delayMusic(1/8f));

		}
	}

	IEnumerator delayMusic(float seconds){
		yield return new WaitForSeconds(seconds);
		recentlyPlayed = false;

	}
	
	// OnGUI is called for rendering and handling
	// GUI events.
	void OnGUI ()
	{
		// Make a background box
		GUILayout.BeginArea (new Rect (Screen.width / 2 - 75, Screen.height / 2 - 50, 150, 300));
		
		
		if (GUILayout.Button ("Play Song")) {
			midiSequencer.Play ();
		}
		if (GUILayout.Button ("Stop Song")) {
			midiSequencer.Stop (true);
		}		
		GUILayout.Label("Play keys AWSEDFTGYHJK");
		
		GUILayout.Box("Instrument: " + Mathf.Round(midiInstrument));
		midiInstrument = (int)GUILayout.HorizontalSlider (midiInstrument, 0.0f, maxSliderValue);
		GUILayout.Box("Volume: " + Mathf.Round(midiNoteVolume));
		midiNoteVolume = (int)GUILayout.HorizontalSlider (midiNoteVolume, 0.0f, maxSliderValue);
		// End the Groups and Area	
		GUILayout.EndArea ();
		
		Event e = Event.current;
		if (e.isKey)
			Debug.Log("Detected key code: " + e.keyCode);		
	}
	
	// This function is called when the object
	// becomes enabled and active.
	void OnEnable ()
	{
		
	}
	
	// This function is called when the behaviour
	// becomes disabled () or inactive.
	void OnDisable ()
	{
		
	}
	
	// Reset to default values.
	void Reset ()
	{
		
	}
	
	// See http://unity3d.com/support/documentation/ScriptReference/MonoBehaviour.OnAudioFilterRead.html for reference code
	//	If OnAudioFilterRead is implemented, Unity will insert a custom filter into the audio DSP chain.
	//
	//	The filter is inserted in the same order as the MonoBehaviour script is shown in the inspector. 	
	//	OnAudioFilterRead is called everytime a chunk of audio is routed thru the filter (this happens frequently, every ~20ms depending on the samplerate and platform). 
	//	The audio data is an array of floats ranging from [-1.0f;1.0f] and contains audio from the previous filter in the chain or the AudioClip on the AudioSource. 
	//	If this is the first filter in the chain and a clip isn't attached to the audio source this filter will be 'played'. 
	//	That way you can use the filter as the audio clip, procedurally generating audio.
	//
	//	If OnAudioFilterRead is implemented a VU meter will show up in the inspector showing the outgoing samples level. 
	//	The process time of the filter is also measured and the spent milliseconds will show up next to the VU Meter 
	//	(it turns red if the filter is taking up too much time, so the mixer will starv audio data). 
	//	Also note, that OnAudioFilterRead is called on a different thread from the main thread (namely the audio thread) 
	//	so calling into many Unity functions from this function is not allowed ( a warning will show up ). 	
	private void OnAudioFilterRead (float[] data, int channels)
	{
		
		//This uses the Unity specific float method we added to get the buffer
		midiStreamSynthesizer.GetNext (sampleBuffer);
		
		for (int i = 0; i < data.Length; i++) {
			data [i] = sampleBuffer [i] * gain;
		}
	}
	
	public void MidiNoteOnHandler (int channel, int note, int velocity)
	{
		Debug.Log ("NoteOn: " + note.ToString () + " Velocity: " + velocity.ToString ());
	}
	
	public void MidiNoteOffHandler (int channel, int note)
	{
		Debug.Log ("NoteOff: " + note.ToString ());
	}
	
	
}
