using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultDirector : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Text resultText = GameObject.Find("ResultText").GetComponent<Text>();
        
        GameObject gd = GameObject.Find("GameDirector");
        if (gd.GetComponent<GameDirector>().isClear)
        {
            resultText.text = "Game Clear!";
        }
        else
        {
            resultText.text = "Game Over";
        }

        Destroy(gd);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
