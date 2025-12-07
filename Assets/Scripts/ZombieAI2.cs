using UnityEngine;

public class ZombieAI2 : MonoBehaviour
{
    public Transform player;
    public float visionRange = 8f;   // radius vision
    public float attackRange = 1.5f;
    public float moveSpeed = 2f;

    public bool cutsceneActive = true; // NEW: zombie stays idle during cutscene

    private Animator anim;
    private bool isAttacking = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // --- Freeze zombie until cutscene ends ---
        if (cutsceneActive)
        {
            Idle();
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        // --- Now vision is radius-based, no need for dot check ---
        if (distance < visionRange)
        {
            if (distance > attackRange)
            {
                WalkTowardsPlayer();
            }
            else
            {
                AttackPlayer();
            }
        }
        else
        {
            Idle();
        }
    }

    void WalkTowardsPlayer()
    {
        if (isAttacking) return;

        anim.SetBool("Walk", true);
        anim.SetBool("Attack", false);

        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
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

    System.Collections.IEnumerator DealDamage()
    {
        isAttacking = true;

        yield return new WaitForSeconds(0.6f);

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < attackRange + 0.2f)
            player.GetComponent<PlayerHealth>().TakeDamage();

        yield return new WaitForSeconds(1f);
        isAttacking = false;
    }
}
