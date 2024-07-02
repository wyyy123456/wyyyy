using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverToPrint : MonoBehaviour
{
    public Animator anim;

    public void PlayPrintAnim()
    {
        anim.SetTrigger("Print");
        //play vfx
        //Instantiate(xxx);
        //play sfx
        //xxx.PlayOneShot();
    }
}
