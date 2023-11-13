using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Playert : MonoBehaviour
{
    public Transform playertransform;
    public string[] eX;
    public Text Wintext;
    public static Playert playerts;
public void Start()
{
playerts=this;
}
    void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("a");
        if(coll.gameObject.CompareTag("Platform"))
        {
          
            StartCoroutine(ReBackToTransform());
             ladder.ladderx.aipath.canMove = false;
        
             ladder.ladderx.aiset.target = null;
        }
    }
    public IEnumerator ReBackToTransform()
    {
         GameManager.manager.gameVfx.PlayOneShot(GameManager.manager.WinSound);
         Wintext.text = eX[Random.Range(0,eX.Length)];
        yield return new WaitForSeconds(1f);
        
        StartCoroutine(GameManager.manager.NewLevelCreation());
        yield return new WaitForSeconds(0.25f);
        
         
        GameManager.Score += 1;
        PlayerPrefs.SetInt("scorePoint",GameManager.Score);
          Playert.playerts.Wintext.text = "";
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        this.transform.position = playertransform.position;
        
    }
    public IEnumerator ReBackToTransformNowin()
    {
        
        GameManager.manager.gameVfx.PlayOneShot(GameManager.manager.LoseSound);
        Playert.playerts.Wintext.text = "";
        yield return new WaitForSeconds(0.15f);
        this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        this.transform.position = playertransform.position;
    }
    
}
