using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class card : MonoBehaviour
{
    public bool faceup, locked;
    public static bool coroutineAllowed;
    private card firstinpair, secondinpair;
    private string firstinpairname, secondinpairname;
    public static Queue<card> sequence;
    public static int pairsfound;
    private Text movesText;
    public static int moves=0;
    public static int no = 0;
    public static int temp;
    public static GameObject wintext;
    public static GameObject Clearscreen;
    public static GameObject left;
    public static GameObject right;
    public static GameObject centre;
  public static GameObject blackstar;
    public AudioSource audiosrc;
  
    List<Sprite> crds = new List<Sprite>();
    //    crds=Resources.
    
       
    private void Start()
    {
        
        //Clearscreen = GameObject.Find("Clearscreen");
        //Clearscreen.SetActive(false);
        faceup = false;
        coroutineAllowed = true;
        locked = false;
        audiosrc = GetComponent<AudioSource>();
        sequence = new Queue<card>();
       //  moves = 0;
        movesText = GetComponent<Text>();

        pairsfound = 0;
        if (wintext == null)
        {
            wintext = GameObject.Find("wintext");
            wintext.SetActive(false);
            Clearscreen = GameObject.Find("Clearscreen");
            Clearscreen.SetActive(false);
            left = GameObject.Find("left");
            left.SetActive(false);
            right = GameObject.Find("right");
            right.SetActive(false);
            centre = GameObject.Find("centre");
            centre.SetActive(false);
            blackstar = GameObject.Find("blackstar");
            blackstar.SetActive(false);
        }

        GameObject[] go = GameObject.FindGameObjectsWithTag("Card");
        no = go.Length;
        temp = no;

    }
    private void OnMouseDown()
    {
        if (!locked && coroutineAllowed && (sequence.Count == 0 || sequence.Count == 1))
        {

            StartCoroutine(RotateCard());
           }
        }

    public IEnumerator RotateCard()
    {
        coroutineAllowed = false;
        if (!faceup)
        {
            sequence.Enqueue(this);
            for (float i = 0f; i <= 180f; i += 10)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                
                yield return new WaitForSeconds(0f);
                
            }
        }
        coroutineAllowed = true;//pauses program
        faceup = !faceup;
        if (sequence.Count == 2)
        {
            Checkresults();
        }
    }
    private void Checkresults()
    {

        firstinpair = sequence.Dequeue();
        secondinpair = sequence.Dequeue();
        firstinpairname = firstinpair.name.Substring(0, firstinpair.name.Length - 1);
        secondinpairname = secondinpair.name.Substring(0, secondinpair.name.Length - 1);
        if (firstinpairname == secondinpairname && !(firstinpair.name == secondinpair.name))
        {
            firstinpair.locked = true;
            secondinpair.locked = true;
            pairsfound += 1;
            score.scoreAmount += 1;
            moves++;

        }
        else if (!(firstinpair.name == secondinpair.name))
        {
            firstinpair.StartCoroutine("RotateBack");
            secondinpair.StartCoroutine("RotateBack");
            moves++;
            audiosrc.Play();
        }
        if (pairsfound == no)
        {
            wintext.SetActive(true);
            Clearscreen.SetActive(true);
            blackstar.SetActive(true);
         
            if (moves == score.scoreAmount)
            {
                left.SetActive(true);
                right.SetActive(true);
                centre.SetActive(true);
            }
            else if (moves==score.scoreAmount+1)
            {
                left.SetActive(true);
                centre.SetActive(true);
            }
            else
            {
                left.SetActive(true);
            }

        }
    }
    private void Update()
    {
        movesText.text = "Moves : " + moves;
       
    }
    public IEnumerator RotateBack()
    {
        coroutineAllowed = false;
        yield return new WaitForSeconds(0.2f);
        for (float i = 180f; i >= 0f; i -= 10)
        {
            transform.rotation = Quaternion.Euler(0f, i, 0f);
            yield return new WaitForSeconds(0f);
            sequence.Clear();
        }
        faceup = false;
        coroutineAllowed = true;
    }
}
