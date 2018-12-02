using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	public static AudioSource source;

	public static Dictionary<string, AudioClip> sfx = new Dictionary<string, AudioClip>();

	void Awake(){
		source = GetComponent<AudioSource>();
		AudioClip[] allClips = Resources.LoadAll("Sounds/", typeof(AudioClip)).Cast<AudioClip>().ToArray();
		foreach(AudioClip audioclip in allClips){
			sfx[audioclip.name] = audioclip;
		}
	}

	public static void PlaySFX(string name, float volume = 1f){
		AudioClip clip = sfx[name];
		if(clip == null) Debug.LogError("Trying to play non-existent sound, "+name);
		else source.PlayOneShot(clip, volume);
	}

	public static void PlaySFXAt(string name, Vector3 pos, float volume = 1f){
		AudioClip clip = sfx[name];
		if(clip == null) Debug.LogError("Trying to play non-existent sound, "+name);
		else AudioSource.PlayClipAtPoint(clip, pos, volume);
	}
}
