using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {
    private AudioSource source;
    int sample = 1024;
    float Loudness = 0;
    //get data from microphone into audioclip
    float maxLoud(AudioClip clip)
    {
        float[] soundData = new float[sample];
        //when using null the default mic will be chosen
        int micPosition = Microphone.GetPosition(null) - (sample + 1);
        if (micPosition < 0)
            return 0;
        clip.GetData(soundData, micPosition);
        // peak on the last X amount samples
        for (int i = 0; i < sample; i++)
        {
            float peak = soundData[i] * soundData[i];
            if (Loudness < peak)
            {
                Loudness = peak;
            }
        }
        return Loudness;
    }
    void Start()
    {
        foreach (string device in Microphone.devices) {
            Debug.Log("Name: " + device);
        }
        source = GetComponent<AudioSource>();
        source.clip = Microphone.Start(Microphone.devices[0], true, 10, 44100);
        source.loop = true;
        while (!(Microphone.GetPosition(null) > 0)) ;
        source.Play();
    }
    // Update is called once per frame
    void Update () {
       	// maxLoud(source.clip);
    }
}
