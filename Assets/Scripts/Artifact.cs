using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Artifact : MonoBehaviour
{
   public float perRadian = 0.01f;
    public float radian = 0;
    public float radius =0.0025f;

    public GameManager gm;
    float dy;

    void FixedUpdate()
    {
        radian +=perRadian;
        dy = Mathf.Cos(radian)*radius;
        transform.position += new Vector3(0,dy,0); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gm.SetResult();
        ChangeToNextScene();
    }

    public void ChangeToNextScene()
    {
        SceneManager.LoadScene(2);
    }
}
