using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLogic : MonoBehaviour
{
    [SerializeField] string NextLevel;
    [SerializeField] Animator ImageAnimation;


    private void Awake()
    {
        ImageAnimation = GameObject.FindGameObjectWithTag("Image").GetComponent<Animator>();
    }

    public void EndLevel()
    {
        SceneManager.LoadScene(NextLevel);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            ImageAnimation.SetTrigger("close");
        }
    }
}
