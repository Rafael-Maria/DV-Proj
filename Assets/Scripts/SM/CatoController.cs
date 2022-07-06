using UnityEngine;
using UnityEngine.EventSystems;

public class CatoController : MonoBehaviour
{
    private ShootingMinigame sm;
    [SerializeField] private int type;
    [SerializeField] private int score;
    [SerializeField] private Vector3 speed;
    [SerializeField] private Vector3 direction;
    private GameObject parent;
    private void Start()
    {
        sm = FindObjectOfType<ShootingMinigame>();
        parent = transform.parent.gameObject;
        randomizeSpeed();
    }

    void OnMouseDown()
    {
        if(sm != null) {
            if (sm.GetBullets() > 0)
            {
                if (parent != null)
                {
                    Destroy(parent);
                }

                sm.AddScore(score);
                sm.SpawnEnemy(type);
            }
        }
    }

    private void Update()
    {
        if(parent.transform.position.x > 80) //Passou o lado direito da camera
        {
            parent.transform.position = new Vector3(-75, Random.Range(-13, -19), parent.transform.position.z);
            randomizeSpeed();
        } else if(parent.transform.position.x < -80) //Passou o lado esquerdo da camera
        {
            parent.transform.position = new Vector3(75, Random.Range(-13, -19), parent.transform.position.z);
            randomizeSpeed();
        }

        Vector3 movement = new Vector3(speed.x * direction.x, speed.y * direction.y, 0);
        movement *= Time.deltaTime;
        parent.transform.Translate(movement);
    }

    private void randomizeSpeed()
    {
        speed = new Vector3(Random.Range(12, 25), 0, 0);
    }
}
