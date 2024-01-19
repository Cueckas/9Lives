using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(312);
        Invoke("TestIns", 1);

    }

    void FixedUpdate(){
        Debug.Log(8950);
    }

    void TestIns()
    {
        Debug.Log(659048);
        SceneManager.LoadScene(1);
    }
}
