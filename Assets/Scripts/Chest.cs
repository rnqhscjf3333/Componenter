using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !gameObject.GetComponent<Animator>().GetBool("isOpen"))
        {
            GetComponent<Animator>().SetBool("isOpen", true);
            GameObject.Find("Scroll View").GetComponent<ComponentScroll>().targetPos = 0;
        }
    }
}
