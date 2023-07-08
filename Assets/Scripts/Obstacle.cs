using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Transform prefab;
    public float ContinueTime;
    public int Num;
    private float timer;
    private void Start()
    {
        timer = GameManager.instance.RandomCreate(Num, prefab, transform);
    }
    private void Update()
    {
        if (Time.time >= timer + ContinueTime)
        {
            timer = GameManager.instance.RandomCreate(Num, prefab, transform);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))//Íæ¼Ò´¥ÅöÕÏ°­
        {
            GameManager.instance.GameLose();
        }
    }
}
