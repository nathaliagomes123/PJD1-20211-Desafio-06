using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : Rigidbody2DBase, IPoolableObject
{
    public int Hp { get; protected set; }

    private NavMeshAgent map;

    // Seguir o jogador e campo de visão...
    float SpeedEnemy = 2f;
    float StopDistance = 3f;
    private Transform TargetVision; // Campo de visão.

    /*/ Patrulha
    public Transform currentTarget; // Pivô, ponto zero.
    public Transform point1; // Pivô 1.
    public Transform point2; // Pivô 2. */

    // Patrulha
    public float distance;
    bool isRight = true;
    public Transform pointCheck;

    private void Start()
    {
        Hp = Random.Range(100, 201);

        // Procura o GameObject com a Tag Player, acessando a sua posição.
        TargetVision = GameObject.FindGameObjectWithTag("Player").transform;

        //currentTarget = point1;

        map = GetComponent<NavMeshAgent>();
        map.updateRotation = false;
        map.updateUpAxis = false;
    }

    void Update()
    {
        MoveEnemy();
    }

    public bool ApplyDamage(int damage)
    {
        Hp -= damage;
        if(Hp<=0)  
            Destroy(gameObject);
        return Hp <= 0;
    }

    public void Recycle()
    {
        gameObject.SetActive(false);
    }

    public void TurnOn()
    {
        gameObject.SetActive(true);
        Start();
    }

    protected virtual void MoveEnemy()
    {
        // Só movimenta o inimigo se a distância entre os dois for menor que a escolhida no StopDistance. Basicamente um campo de visão.
        if (Vector2.Distance(transform.position, TargetVision.position) < StopDistance)
        {
            // Movimenta o gameObject até um ponto específico com uma velocidade.
            transform.position = Vector2.MoveTowards(transform.position, TargetVision.position, SpeedEnemy * Time.deltaTime);
            Debug.LogFormat("Está vendo o Player");
        }
        else
        {
            transform.Translate(Vector2.right * SpeedEnemy * Time.deltaTime);
            RaycastHit2D point = Physics2D.Raycast(pointCheck.position, Vector2.down, distance);

            if (point.collider == false)
            {
                if (isRight == true)
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    isRight = false;
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    isRight = true;
                }
            }
           
            /*/
            // movimentação do inimigo até os pivôs.
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, SpeedEnemy * Time.deltaTime);

            // movimentação do inimigo em patrulha, do pivô A ao pivô B, e vise versa.
            if (currentTarget == point1 && transform.position == point1.position)
                currentTarget = point2;
            if (currentTarget == point2 && transform.position == point2.position)
                currentTarget = point1;
            */
        }
    }
}

