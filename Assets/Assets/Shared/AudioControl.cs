﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class AudioControl : MonoBehaviour {
    [SerializeField]AudioClip[] clips;
    [SerializeField] float delayBetweenClips;

    bool canPlay;
    AudioSource source;
	// Use this for initialization
	void Start () {
        source = GetComponent<AudioSource>();
        canPlay = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Play()
    {
        if (!canPlay)
            return;

        GameManager.Instance.Timer.Add(() =>
       {
           canPlay = true;
       }, delayBetweenClips);

        canPlay = false;

        AudioClip clip =clips[ Random.Range(0, clips.Length)];
        source.PlayOneShot(clip);
    }
}
