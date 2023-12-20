using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoungiEtatManager : MonoBehaviour
{
    private PoungiEtatBase etatActuel;

    public PoungiEtatRepos repos = new PoungiEtatRepos();
    public PoungiEtatWave wave = new PoungiEtatWave();

    private int _nbCubesMax ;
    public List<GameObject> TousLesCubes { get; set; }
    public GameObject home { get; set; }
    public GameObject cible { get; set; }
    public NavMeshAgent agent { get; set; }
    public Animator animator { get; set; }
    public Transform goal { get; set; }
    public float range = 30f;
    public GenerateurIle generateurIle { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("TousLesCubes" + TousLesCubes.Count);
        _nbCubesMax = TousLesCubes.Count;
        // Debug.Log(cible);
        // Debug.Log(home.transform.position);
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        ChangerEtat(repos);
        transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));

    }

    public void ChangerEtat(PoungiEtatBase etat)
    {
        etatActuel = etat;
        etatActuel.InitEtat(this);
    }
    // Update is called once per frame
    void Update()
    {

        // Debug.Log(agent.velocity);
        // Debug.Log(Vector3.Normalize(agent.velocity));

        // etatActuel.UpdateEtat(this);

        //fait les animations de déplacement
        animator.SetFloat("Speed", agent.velocity.magnitude);
        if (agent.velocity.magnitude < 0.1f)
        {
            animator.SetBool("IsIdling", true);
            animator.SetBool("IsWalking", false);
        }
        else if (agent.velocity.magnitude > 1f && agent.velocity.magnitude < 5f)
        {
            animator.SetBool("IsIdling", false);
            animator.SetBool("IsWalking", true);
        }

    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        etatActuel.TriggerEnter(this, other);
    }

}