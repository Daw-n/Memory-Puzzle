using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{


    public Button soundbtn;
    public AudioSource audiosource;
    public int i=1;
    private void Update()
    {
        
        soundbtn = GetComponent<Button>();
        audiosource = GetComponent<AudioSource>();
        soundbtn.onClick.AddListener(() => StopAudio());
    }

    void StopAudio()
    {
        if (i == 0)
        {
            i++;
            audiosource.Play();
            
           
            
        }
        else if(i==1)
        {
            i--;
            audiosource.Stop();
            
        }
    }

}
