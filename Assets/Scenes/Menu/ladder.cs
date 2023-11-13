using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System.Threading;
using UnityEngine.UI;
public class ladder : MonoBehaviour
{
    // Start is called before the first frame updat
    public AIPath aipath;
 
    public AIDestinationSetter aiset;
    public static ladder ladderx;
   
    public GameObject tree;
    
    void Start()
    {
        ladderx = this;
    }
    public void Update()
    {
        tree.transform.position=transform.GetChild(0).transform.position;
         tree.transform.rotation=transform.GetChild(0).transform.rotation;
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
       
        if(coll.gameObject.CompareTag("Platform"))
        {
            if(!Tree.FirstTree)
            {
               

                Playert.playerts.Wintext.text = "";

                GameManager.Mistake += 1;
                
                GameManager.CanTouch = false;
          
                GetComponent<Rigidbody2D>().angularVelocity = 0;
                GameManager.manager.gameVfx.PlayOneShot(GameManager.manager.LoseSound);
                StartCoroutine(GameManager.manager.NewLevelCreation());
            }
              
        }
    }
    
   
}
