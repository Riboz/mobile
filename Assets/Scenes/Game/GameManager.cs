using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
public class GameManager : MonoBehaviour
{
    public AudioSource gameVfx;
    public AudioClip treeGrowth, treeFall, Collision, WinSound, LoseSound;
    public Image[] startImages, health;
    public Sprite[] healthSprite, Weather;
    public Text scoreText;
    public static GameManager manager;
    public static bool CanTouch;
    public static int Mistake,Score;
    bool finishCreate,TouchControl;
    public GameObject touchImage,SpawningPlatform,Ladder,BeforePlat,WeatherImage,watchadPanel;
    private float selectedposX, selectedposY,ladder_Y;
   
    RaycastHit2D ray;
    public LayerMask  a;
    // 12 circle boyutu her level değişiminde 12 yap 
    // Start is called before the first frame update
    void Start()
    {
       manager = this;

       Mistake = 0;
       Score = PlayerPrefs.GetInt("scorePoint");

       CanTouch = false;

       StartCoroutine(NewLevelCreation());
        // ilklevel yükleme methodunu çalıştır

    }
   
    IEnumerator LengthMethodOfLadder()
    {

        while(TouchControl)
        {
        Ladder.transform.localScale += new Vector3(0, ladder_Y, 0);

        ladder_Y += 0.0025f;

        
         
        
        
        yield return new WaitForSeconds(0.025f);
        }
       

    }
    public void OnDown()
     {
        if(CanTouch)
        {
        // clip başka loopta 
        gameVfx.PlayOneShot(treeGrowth);
        gameVfx.loop = true;
        Debug.Log("tutuyor");
        TouchControl = true;
        CanTouch=false;
        StartCoroutine(LengthMethodOfLadder());
        touchImage.gameObject.SetActive(false);
        }
        
     }
     public void OnUp()
       {
        Debug.Log("birakti");
        // ağaç kırılma sesi
         
        
       if(TouchControl) 
       {
        Ladder.GetComponent<Rigidbody2D>().angularVelocity=-75;
        TouchControl = false;
         gameVfx.Stop();
         gameVfx.loop = false;
         gameVfx.PlayOneShot(treeFall);
        
        StopCoroutine(LengthMethodOfLadder());
       }

       }
    float GetRandomX (float min, float max)
{
    float PosX = UnityEngine.Random.Range(min, max);
    while (PosX == selectedposX && Mathf.Abs(PosX-selectedposX) <= 0.2f) PosX = UnityEngine.Random.Range (min, max);
        
    selectedposX = PosX;
    return PosX;
}
    float GetRandomY (float min, float max)
{
    float PosY = UnityEngine.Random.Range(min, max);
    while (PosY == selectedposX && Mathf.Abs(PosY-selectedposY) <= 0.2f) PosY = UnityEngine.Random.Range (min, max);
        
    selectedposY = PosY;
    return PosY;
}

    public IEnumerator NewLevelCreation()
    {

        
       switch(Mistake)
       {

         case 0: for(int i=0;i<health.Length;i++){health[i].sprite = healthSprite[0];} 
         break;
         
         case 1: health[2].sprite = healthSprite[1];
         break;
         
         case 2:  health[1].sprite = healthSprite[1];
         break;

         case 3:  health[0].sprite = healthSprite[1];   watchadPanel.SetActive(true);
         break;
       
       }
       
        for(int i = 0; i < startImages.Length; i++)
        {
            startImages[i].transform.DOScale(12,0.25f).SetEase(Ease.InCubic);
        }
        yield return new WaitForSeconds(0.5f);
        
        ladder.ladderx.aipath.canMove = false;
        
        ladder.ladderx.aiset.target = null;

         Playert.playerts.Wintext.text = "";
         
        GetRandomX(-0.2f,1.6f);

        GetRandomY(-3f,-0.8f);
       
        Destroy(BeforePlat);

        BeforePlat = Instantiate(SpawningPlatform,new Vector2(selectedposX, selectedposY),quaternion.identity);

        for(int i = 0; i < startImages.Length; i++)
        {
            startImages[i].transform.DOScale(0,0.25f).SetEase(Ease.InCubic);
        }
        
        Ladder.transform.localScale = new Vector3(0.3f,0.6f,1);

        Ladder.GetComponent<Rigidbody2D>().angularVelocity = 0;

        Ladder.GetComponent<Rigidbody2D>().rotation = 0;

        WeatherImage.GetComponent<SpriteRenderer>().sprite = Weather[UnityEngine.Random.Range(0,3)];

        touchImage.gameObject.SetActive(true);

        Playert.playerts.Wintext.text = "";

        Playert.playerts.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        Playert.playerts.transform.position = Playert.playerts.playertransform.position;

        
        scoreText.text = ""+Score;
        yield return new WaitForSeconds(0.5f);
        ladder_Y = 0f;

        Tree.FirstTree = false;

        

        CanTouch = true;
        

    }
    // Update is called once per frame
    public void ReBackToMenu()
    {
        SceneManager.LoadScene(0);

        Mistake = 0;

        Score = 0;

    }
    public void Restart()
    {
        StartCoroutine(NewLevelCreation());
        
    }
    void FixedUpdate()
    {    
        
    }
}
