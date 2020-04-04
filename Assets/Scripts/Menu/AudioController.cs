using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

    float musicVolume;

    void Awake()
    {
        // PlayerPrefs.SetFloat("MusicVolume", 0.5f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        AudioListener.volume = musicVolume;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        
    }
}
