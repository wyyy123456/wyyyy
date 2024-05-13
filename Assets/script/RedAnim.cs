using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedAnim : MonoBehaviour
{
    public SkinnedMeshRenderer skin;
    bool isPlaying = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "WaterRed" && !isPlaying)
        {

            StartCoroutine(PlayRedAnimation());
            isPlaying = true;
        }
    }

    IEnumerator PlayRedAnimation()
    {
        for (int i = 100; i > 0; i--)
        {
            skin.SetBlendShapeWeight(0, i);
            yield return null;
            
            
        }
        

        yield return null;
    }
}
