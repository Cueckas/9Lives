using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Emerge : MonoBehaviour
{
    public Texture img; // ºÚÉ«Í¼Æ¬
    public float speed; // µ­³öËÙ¶È

    private static string nextScene;
    private static bool fadeOut = false;
    private bool fadeIn = false;
    private float alpha = 0f;

    void Update()
    {
        if (fadeOut)
        {
            alpha += speed * Time.deltaTime;
            if (alpha >= 1)
            {
                SceneManager.LoadScene(nextScene); // ³¡¾°ÇÐ»»
                fadeIn = true;
                fadeOut = false;
            }
        }

        if (fadeIn)
        {
            alpha -= speed * Time.deltaTime;
            if (alpha <= 0)
            {
                fadeIn = false;
            }
        }
    }

    void OnGUI()
    {
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), img); // »æÖÆºÚÉ«Í¼Æ¬
    }

    public static void LoadScene(string scene)
    {
        nextScene = scene;
        fadeOut = true;
    }
}
