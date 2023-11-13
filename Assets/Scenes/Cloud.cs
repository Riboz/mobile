using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
     if(this.transform.position.x==7.4f) transform.position=new Vector2(-3f,this.transform.position.y);
     this.transform.position=Vector2.MoveTowards(this.transform.position,new Vector2(7.4f,this.transform.position.y),0.5f*Time.deltaTime);
    }
}
