using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public GameObject ladderz;
    public static bool FirstTree=false;
     void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("a");
        if(coll.gameObject.CompareTag("Platform"))
        {
            ladderz.GetComponent<Rigidbody2D>().angularVelocity = 0;
            FirstTree = true;
            ladder.ladderx.aiset.target = coll.gameObject.transform.GetChild(0);
            AstarPath.active.Scan();
            ladder.ladderx.aipath.canMove = true;
            
            Playert.playerts.Wintext.text="...";
            GameManager.CanTouch = false;
        
        }
         else if(coll.gameObject.CompareTag("Base") && !FirstTree)
        {
              FirstTree = true;
              GameManager.Mistake += 1;
              Playert.playerts.Wintext.text = "";
              GameManager.manager.gameVfx.PlayOneShot(GameManager.manager.LoseSound);
            StartCoroutine(GameManager.manager.NewLevelCreation());
        }
    }
}
