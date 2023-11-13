using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening.Plugins.Options;

public class MenuController : MonoBehaviour
{
    public AudioSource music;
    public GameObject[] Circle;
    bool soundAct=true;
    public Sprite sound,nsound;
    public UnityEngine.UI.Image Option;
    public UnityEngine.UI.Image[] Scenecircle;
    public Button[] Thebuttons;
    // Start is called before the first frame update
    public IEnumerator theCircle()
    {
        for(int i=0;i<Circle.Length;i++)
        {
        Circle[i].GetComponent<Transform>().DOScale(0.5f,2).SetLoops(-1,LoopType.Yoyo).SetRelative().SetEase(Ease.Linear);
      
        yield return new WaitForSeconds(Random.Range(0,2));
       
        }

    }
    public IEnumerator theButton()
    {
        for(int i=0;i<Thebuttons.Length;i++)
        {
       Thebuttons[i].GetComponent<Transform>().DOScale(0.5f,1).SetLoops(-1,LoopType.Yoyo).SetRelative().SetEase(Ease.Linear);

        }
        yield return new WaitForSeconds(1f);
    }
    void Start()
    {
        Application.targetFrameRate = 90;
        StartCoroutine(theButton());

        StartCoroutine(theCircle());
    }
    private IEnumerator ChangeScene()
    {
    
        for(int i=0;i<Scenecircle.Length;i++)
        {
            Scenecircle[i].transform.DOScale(12,0.5f).SetEase(Ease.InCubic);
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
   public void StartGame()
   {
    StartCoroutine(ChangeScene());
   
   }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void OptionGame()
    {
        Option.transform.DOLocalMoveY(0,0.3f).SetEase(Ease.InCubic);
    }
    public void RebackFromOptions()
    {
        Option.transform.DOLocalMoveY(-4500,0.3f);
    }
    public void Sound()
    {
        soundAct = !soundAct;

       if(soundAct) Thebuttons[3].GetComponent<UnityEngine.UI.Image>().sprite = sound;
       else
       {
         Thebuttons[3].GetComponent<UnityEngine.UI.Image>().sprite = nsound;
       }
       music.mute = soundAct;
    }

}
