using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text countText;

    private int collectibleCount = 0; 
        void Start()
    {
       
        UpdateCount();
    }

       public void UpdateCount()
    {
       
        countText.text = "Collected: " + collectibleCount.ToString();
    }

    
    public void CollectObject()
    {
        
        collectibleCount++;
       
        UpdateCount();
    }
}