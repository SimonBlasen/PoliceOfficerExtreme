using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioManager : MonoBehaviour
{

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip[] radioSongs;
    [SerializeField]
    private int sender;
    [SerializeField]
    private string[] radioNames;
    [SerializeField]
    private TMPro.TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        sender = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(sender < 3)
            {
                sender += 1;
            }
            else
            {
                sender = 0;
            }

            text.text = radioNames[sender];
            if(sender == 0)
            {
                audioSource.Stop();
            }
            else
            {
                audioSource.clip = radioSongs[sender-1];
                audioSource.Play();
            }

        }
    }
}
