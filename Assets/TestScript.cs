using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour {
	public Text text;
    private AudioSource source;
    int sample = 1024;
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
		float result=0;
        for (int i = 0; i < sample; i++)
        {
            result += soundData[i] * soundData[i];
        }
		text.text=result.ToString();
        return result;
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
       	maxLoud(source.clip);
    }
}
