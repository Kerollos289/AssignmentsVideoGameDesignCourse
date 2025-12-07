//using UnityEngine;

//public class ZombieAI : MonoBehaviour
//{
//    public Transform player;
//    public float visionRange = 8f;
//    public float attackRange = 1.5f;
//    public float moveSpeed = 2f;

//    private Animator anim;
//    private bool isAttacking = false;

//    void Start()
//    {
//        anim = GetComponent<Animator>();
//    }

//    void Update()
//    {
//        float distance = Vector3.Distance(transform.position, player.position);

//        // Check if player is in front of zombie (zombie FOV)
//        Vector3 dir = (player.position - transform.position).normalized;
//        float dot = Vector3.Dot(transform.forward, dir); // 1 = in front, -1 = behind

//        if (distance < visionRange && dot > 0.6f) // only moves when player is in front
//        {
//            if (distance > attackRange)
//            {
//                WalkTowardsPlayer();
//            }
//            else
//            {
//                AttackPlayer();
//            }
//        }
//        else
//        {
//            Idle();
//        }
//    }

//    void WalkTowardsPlayer()
//    {
//        if (isAttacking) return;

//        anim.SetBool("Walk", true);
//        anim.SetBool("Attack", false);

//        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
//        transform.position += transform.forward * moveSpeed * Time.deltaTime;
//    }

//    void AttackPlayer()
//    {
//        anim.SetBool("Walk", false);
//        anim.SetTrigger("Attack");

//        if (!isAttacking)
//            StartCoroutine(DealDamage());
//    }

//    void Idle()
//    {
//        anim.SetBool("Walk", false);
//    }

//    System.Collections.IEnumerator DealDamage()
//    {
//        isAttacking = true;

//        yield return new WaitForSeconds(0.6f); // attack animation timing

//        float distance = Vector3.Distance(transform.position, player.position);
//        if (distance < attackRange + 0.2f)
//            player.GetComponent<PlayerHealth>().TakeDamage();

//        yield return new WaitForSeconds(1f);
//        isAttacking = false;
//    }
//}



using UnityEngine;
using System.Collections;

public class ZombieAI : MonoBehaviour
{
    public Transform player;
    public float visionRange = 8f;
    public float attackRange = 1.5f;
    public float moveSpeed = 2f;

    private Animator anim;
    private bool isAttacking = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (!player) player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!player) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // Check if player is roughly in front
        Vector3 dir = (player.position - transform.position).normalized;
        float dot = Vector3.Dot(transform.forward, dir); // 1 = in front

        if (distance < visionRange && dot > 0.5f)
        {
            if (distance > attackRange)
                WalkTowardsPlayer();
            else
                AttackPlayer();
        }
        else
            Idle();
    }

    void WalkTowardsPlayer()
    {
        if (isAttacking) return;

        anim.SetBool("Walk", true);
        anim.SetBool("Attack", false);

        // Rotate towards player smoothly
        Vector3 lookPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookPos - transform.position), Time.deltaTime * 5f);

        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    void AttackPlayer()
    {
        anim.SetBool("Walk", false);
        anim.SetTrigger("Attack");

        if (!isAttacking)
            StartCoroutine(DealDamage());
    }

    void Idle()
    {
        anim.SetBool("Walk", false);
    }

    IEnumerator DealDamage()
    {
        isAttacking = true;

        // Wait for attack animation timing
        yield return new WaitForSeconds(0.5f);

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange + 0.2f)
            player.GetComponent<PlayerHealth>().TakeDamage();

        // Cooldown before next attack
        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }
}
