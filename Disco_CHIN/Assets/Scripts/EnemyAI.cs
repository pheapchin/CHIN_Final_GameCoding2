using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.IO;
using Unity.VisualScripting;

public class EnemyAI : MonoBehaviour
{
    //defines diff states and switches between them
    public enum EnemyState {Idle, Patrol, Chase, Attack, Death}
    public EnemyState currentState;

    private Transform player;
    private GameObject bulletClone;
    //public GameObject bulletPrefab;
    private NavMeshAgent agent;

    //patrol settings
    public Transform[] patrolPoints;
    private int currentPatrolIndex;

    //AI settings
    [Header("AI Settings")]
    public string enemyType;
    public int health;
    public float speed;
    public float detectionRange;
    public float attackRange;
    public float attackCooldown;

    float lastAttackTime;
    int collisionCount = 0;

    public static bool dead;

    [Header("Projectile Settings")]
    public Rigidbody projectile;
    public float projSpeed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //load enemy data from json
        LoadEnemyData(enemyType);
        //apply loaded stats
        agent.speed = speed;
        lastAttackTime = -attackCooldown;

        currentState = EnemyState.Patrol;
        MoveToNextPatrolPoint();

        //if(player == null)
        //{
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            Debug.Log(player.position);
            if(player!= null)
            {
                Debug.Log("player found in scene");
            }
            else
            {
                Debug.Log("no found player");
            }
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"Enemy State: {currentState} | Distance to Player: {Vector3.Distance(transform.position, player.position)} | Has Path: {agent.hasPath}");
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        //need to get the clone from throwables-reference weapon pickup script

        //find projectile gameoject to get the clone's position for the enemy to hunt the clone when thrown PLEASEEEEEEEEEEEEEEEEEEEEEEEEE
        //yourscript.scriptvar.clonevar
        //bulletPrefab = Throwables.Instance.objectToThrow;
        //it is not grabbing the right projectile.
        
        //bulletClone = Throwables.Instance.projectile; //GameObject.FindGameObjectWithTag("Bullet").transform;
        //float distanceToBullet = Vector3.Distance(transform.position, bulletClone.transform.position);


        //switch statement like multiple choice decision maker
        //check a variable and decide what code to run based on var value

        //determining what behavior to perform based off its current state
        switch(currentState)
        {
            case EnemyState.Idle:
                IdleState();
                //break makes sure program doesnt check other cases once match is found
                break;

            //move between waypoints if player is detected it switches to chase
            case EnemyState.Patrol:
                PatrolState();
                //if enemy within detection will switch to chase
                if (distanceToPlayer <= detectionRange) ChangeState(EnemyState.Chase);
                break;

            //move towards player if close enough switches to attack
            //if player escapes switches back to patrol
            case EnemyState.Chase:
                ChaseState();
                if (distanceToPlayer <= attackRange) ChangeState(EnemyState.Attack);
                else if (distanceToPlayer >= detectionRange) ChangeState(EnemyState.Patrol);

                break;

            //attacks player if player moves away switches back to chase
            case EnemyState.Attack:
                AttackState();
                if(distanceToPlayer > attackRange) ChangeState(EnemyState.Chase);

                break;
        }
    }

    void ChangeState(EnemyState newState)
    {
        //updates enemies current state
        currentState = newState;
    }

    void IdleState()
    {
        //can add animation
        //sound
    }

    void PatrolState()
    {
        if(!agent.enabled || !agent.isOnNavMesh) return;
        //enemy follows path to target, waits until it reaches the patrol point, once it reaches it it moves to next location
        //path pending is true if unity is still calculating the path
        //if false, path has been fully calculated and enemy is moving
        //if enemy is close enough to patrol point, .5 it moves to next one
        if(!agent.pathPending && agent.remainingDistance < .5f)
        {
            MoveToNextPatrolPoint();
        }
    }

    void ChaseState()
    {
        if (!agent.enabled || !agent.isOnNavMesh) return;

        agent.SetDestination(player.position);
        //check this vv
        //agent.SetDestination(bulletClone.transform.position);
        Debug.Log("chase called");
    }

    void AttackState()
    {
        //indicate attacking
        if(Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            StartCoroutine(SpawnBullets());
            Debug.Log("enemy attacked");
            //logic to alert the player about being found, staying in the radius for a certain amount of time will reset the player's game.
            //logic to damage player health on another script
            DealDamage.SendDamage(1);
        }
    }

        private void MoveToNextPatrolPoint()
        {
            //if (patrolPoints.Length == 0) return;
            //set destination to next patrol point
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
            //update current index and wrap
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            collisionCount++;
            health -= 1;

            Debug.Log("bullet hit");

            if(health == 0)
            {
                agent.enabled = false;
                //ChangeState(EnemyState.Death)
                Destroy(gameObject);   
                dead = true;
            }
        }

        if (other.gameObject.CompareTag("Melee"))
        {
            collisionCount++;
            health -= 1;

            Debug.Log("melee hit");

            if (health == 0)
            {
                agent.enabled = false;
                //ChangeState(EnemyState.Death)
                Destroy(gameObject);
                dead = true;
            }
        }

        if (other.gameObject.CompareTag("DOT"))
        {
            collisionCount++;
            if(GetComponent<StatusManager>() != null)
            {
                GetComponent<StatusManager>().ApplyDOT(4);
                Debug.Log("dot hit");
            }
            //health -= 2;

            //Debug.Log("damaged");

            if (health == 0)
            {
                agent.enabled = false;
                //ChangeState(EnemyState.Death)
                Destroy(gameObject);
                dead = true;
            }
        }

        if (other.gameObject.CompareTag("Slow"))
        {
            collisionCount++;
            if (GetComponent<StatusManager>() != null)
            {
                GetComponent<StatusManager>().ApplySlow(4);
                Debug.Log("slowed");
            }
            //health -= 2;

        }
    }

    IEnumerator SpawnBullets()
    {
        Debug.Log("shooting");
        yield return new WaitForSeconds(3f);

        Rigidbody p = Instantiate(projectile, transform.position, transform.rotation);
        p.velocity = transform.forward * speed;

        StartCoroutine(SpawnBullets());
    }

    private void LoadEnemyData(string enemyName)
    {
        string path = Application.dataPath + "/Data/enemiesText.json";
        //string path = Path.Combine(Application.streamingAssetsPath, "Data/EnemiesText.json");
        if (File.Exists(path))
        {
            //read json file as tezxt and store as a string
            string json = File.ReadAllText(path);
            //convert json into c# objects and store results
            EnemyDataBase enemyStats = JsonUtility.FromJson<EnemyDataBase>(json);
            print(enemyStats.enemiesList.Count);
            //find the correct enemy in json
            //loop through all enemies
            foreach(EnemyStats enemy in enemyStats.enemiesList)
            {
                if(enemy.name == enemyName)
                {
                    health = enemy.health;
                    speed = enemy.speed;
                    detectionRange = enemy.detectionRange;
                    attackRange = enemy.attackRange;
                    attackCooldown = enemy.attackCooldown;
                    Debug.Log($"Loaded enemy: {enemy.name}");
                    return;
                }
            }
        }
        else
        {
            Debug.Log("enemy json file not found");
        }
    }

    //can add rangedattack state
    //can add if distance is greater than attack distance fire projectiles
    //stealth IGNORES THE PLAYER IF THE PLAYER CROUCHES BEHIND COVER
}
