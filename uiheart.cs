using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiheart : MonoBehaviour
{
    public Image[] heart;
    public Sprite heartfull;
    public Sprite heartempty;
    public int max_health = 5;
    public int current_health = 0;
    void Start()
    {
        current_health = max_health;
    }

    // Update is called once per frame
    void Update()
    {
        if(current_health>max_health)
        {
            current_health=max_health;
        }
        for (int i = 0; i < heart.Length; i++)
        {
            if (i<current_health)
            {
                heart[i].sprite = heartfull;
            }
            else
            {
                heart[i].sprite = heartempty;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "trap")
        {
           current_health -= 1;
        }
    }
}
