using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehavior : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 destination;
    public Transform PlayerLocale;
    private Vector3 LastPos;
    private Animator animator;
    private float speed = 0f;
    private bool hit = false;
    private bool dying = false;
    public float AttackDistance;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        LastPos = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = PlayerLocale.position;
        if (GetComponent<Health>().health <= 0)
        {
            agent.destination = transform.position;
        }
    }
    
    private void FixedUpdate()
    {
        speed = Mathf.Lerp(speed, Vector3.Distance(transform.position, LastPos) / Time.deltaTime, 0.75f);
        animator.SetFloat("Speed", speed);
        //Debug.Log(speed);
        LastPos = transform.position;
        animator.SetFloat("Distance", Vector3.Distance(transform.position, agent.destination));
        if (GetComponent<Health>().health <= 0 && dying == false)
        {
            animator.SetTrigger("DieBack");
            dying = true;
        }
        else if (dying == false && Vector3.Distance(transform.position, agent.destination) <= AttackDistance)
        {
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Hit");
        if (other.gameObject.tag == "Weapon")
        {
            if (other.gameObject.GetComponent<Sword>().Attacking == true && hit == false)
            {
                Health health = GetComponent<Health>();
                health.TakeDamage();
                hit = true;
            }
            else if (other.gameObject.GetComponent<Sword>().Attacking == false)
            {
                hit = false;
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.tag == "Weapon" && other.gameObject.GetComponent<Sword>().Attacking == false)
        //{
        //    hit = false;
        //}
    }
}
