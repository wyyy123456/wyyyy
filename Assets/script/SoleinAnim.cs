using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoleinAnim : MonoBehaviour
{
    public SkinnedMeshRenderer skin;
    bool isPlaying = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "WaterSolein" && !isPlaying)
        {

            StartCoroutine(PlaySoleinAnimation());
            isPlaying = true;
        }
    }

    IEnumerator PlaySoleinAnimation()
    {
        for (int i = 100; i > 0; i--)
        {
            skin.SetBlendShapeWeight(0, i);
            yield return null;
            
            
        }
        

        yield return null;
    }
}
