using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    public Text countText;
    private int count;

    void Start()
    {
        count = 0;
        UpdateCountText();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            count++;
            UpdateCountText();
          
            Destroy(gameObject);
        }
    }

    void UpdateCountText()
    {
    
        countText.text = "Collected: " + count.ToString();
    }
}
