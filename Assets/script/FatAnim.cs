using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatAnim : MonoBehaviour
{
    public SkinnedMeshRenderer skin;
    bool isPlaying = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "WaterFat" && !isPlaying)
        {

            StartCoroutine(PlayFatAnimation());
            isPlaying = true;
        }
    }

    IEnumerator PlayFatAnimation()
    {
        for (int i = 100; i > 0; i--)
        {
            skin.SetBlendShapeWeight(0, i);
            yield return null;
            
            
        }
        

        yield return null;
    }
}
