using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class DianFenAnim : MonoBehaviour
{
    public SkinnedMeshRenderer skin;
    public VideoPlayer videoPlayer; // 引用 VideoPlayer 组件
    public GameObject videoUI; // 显示视频的UI元素（例如一个包含Raw Image的GameObject）
    public AudioSource audioSource; // 用于播放音效
    public AudioClip soundEffect; // 要播放的音效
    bool isPlaying = false;

    private void Start()
    {
        // 确保游戏一开始时视频UI是隐藏的
        videoUI.SetActive(false);
        videoPlayer.Stop(); // 确保视频不播放
    }

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
        // 显示视频UI
        videoUI.SetActive(true);

        // 开始播放 2D 动画
        videoPlayer.Play();

        // 播放 3D 动画
        for (int i = 100; i > 0; i--)
        {
            skin.SetBlendShapeWeight(0, i);
            yield return null;
        }

        // 等待2秒钟后播放音效
        yield return new WaitForSeconds(2.0f);

        // 播放音效
        if (audioSource != null && soundEffect != null)
        {
            audioSource.PlayOneShot(soundEffect);
        }

        // 隐藏视频UI（如果需要）
        // 可以根据视频长度调整延迟时间
        yield return new WaitForSeconds((float)videoPlayer.length - 2.0f);

        // 隐藏视频UI
        videoUI.SetActive(false);
    }
}
