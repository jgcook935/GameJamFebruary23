using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MacDonaldson : MonoBehaviour, ISign
{
    public List<string> text { get; set; } = new List<string>
    {
        "Get outta here kid",
        "...",
        "What do you wanna fight me or something?"
    };

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    ChangeScenesManager.Instance.SetSceneLocation(collision.transform.position);
    //    SceneManager.LoadScene(3);
    //}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
