using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chickDianfen : MonoBehaviour

{
    public SkinnedMeshRenderer skin;
    bool isPlaying = false;

private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water" && !isPlaying)
        {
            StartCoroutine(PlayDianFenAnimation());
            isPlaying = true;
        }
    }

IEnumerator PlayDianFenAnimation()
    {
    

        // 播放 3D 动画
        for (int i = 100; i > 0; i--)
        {
            skin.SetBlendShapeWeight(0, i);
            yield return null;
        }

        
            yield return null;
        
    }
}