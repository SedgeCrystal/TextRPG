using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TitleDirector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject gd = GameObject.Find("GameDirector");
        if(gd != null)
        {
            Destroy(gd);
        }
        
    }

    public void OnClick_start()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
