using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityChan;

public class AgentController : MonoBehaviour
{
    public Transform target;
    public GameObject button;
    public TextMeshProUGUI score;
    NavMeshAgent agent;
    Animator animator;
    bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        gameOver = agent.remainingDistance < agent.stoppingDistance;
        if (agent.remainingDistance == 0) gameOver = false;
        if (gameOver)
        {
            score.text = "Game Over!!!";
            animator.SetBool("Stopped", gameOver);
            button.SetActive(true);
            target.GetComponent<UnityChanControlScriptWithRgidBody>().enabled = false;
        }
    }
}
