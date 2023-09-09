using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using UnityChan;


public class Point : MonoBehaviour
{
    public GameObject button;
    public TextMeshProUGUI score;
    float rotateSpeed = 90.0f;
    GameObject enemy;
    NavMeshAgent agent;
    Animator animator;
    bool gameover;

    // Start is called before the first frame update
    void Start()
    {
        button.SetActive(false);
        enemy = GameObject.Find("Enemy");
        agent = enemy.GetComponent<NavMeshAgent>();
        animator = enemy.GetComponent<Animator>();
        //Debug.Log(animator);
        gameover = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * Time.deltaTime * rotateSpeed);
        //Debug.Log(gameover);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<AddScore>().currentScore++;
            score.text = "Score: " + other.GetComponent<AddScore>().currentScore.ToString();
            if (!gameover) Destroy(gameObject);
            
            if (other.GetComponent<AddScore>().currentScore == other.GetComponent<AddScore>().fullScore)
            {
                score.text += "  Conguratulations!!!";
                button.SetActive(true);
                //agent.isStopped = true;
                gameover = true;
                animator.SetBool("Stopped", gameover);
                GameObject.FindWithTag("MainCamera").GetComponent<ThirdPersonCamera>().over = true;
                GameObject.FindWithTag("MainCamera").SendMessage("setCameraPositionFrontView");
                other.GetComponent<Animator>().SetBool("Win", true);
                other.GetComponent<UnityChanControlScriptWithRgidBody>().enabled = false;
                Destroy(gameObject);
            } 
        }
    }
}
