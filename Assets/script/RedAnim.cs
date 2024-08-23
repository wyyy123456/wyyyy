using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
public class RedAnim : MonoBehaviour
{
    public SkinnedMeshRenderer skin;
    public VideoPlayer videoPlayer; // 引用 VideoPlayer 组件
    public GameObject videoUI; // 显示视频的UI元素（例如一个包含Raw Image的GameObject）
    bool isPlaying = false;
      private void Start()
    {
        // 确保游戏一开始时视频UI是隐藏的
        videoUI.SetActive(false);
        videoPlayer.Stop(); // 确保视频不播放
    }

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
         // 显示视频UI
        videoUI.SetActive(true);

        // 开始播放 2D 动画
        videoPlayer.Play();

        for (int i = 100; i > 0; i--)
        {
            skin.SetBlendShapeWeight(0, i);
            yield return null;
            
            
        }
        

        yield return null;
    }
}
