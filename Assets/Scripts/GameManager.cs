using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] int stepMax; //�����
    [DoNotSerialize] public int stepRemain; //ʣ�ಽ��

    public static GameManager instance;//GM����
    private void Awake()
    {
        instance = this; 
    }
    void Start()
    {
        stepRemain = stepMax;//��ʼ������
    }

    public void GameWin()
    {
        Debug.Log("��Ϸʤ����");
    }
    public void GameLose()
    {
        Debug.Log("��Ϸʧ�ܣ�");
    }
    public void ChangeStep(int step)
    {
        //�ı�ʣ�ಽ��
        stepRemain += step;
        Debug.Log($"ʣ�ಽ����{stepRemain}���������{stepMax}");
    }
    public float RandomCreate(int n,Transform obj,Transform parent)
    {
        for (int i = 0; i < n; i++)
        {
            Instantiate(obj,parent).position
                = new Vector3(Random.Range(-19, 19), 0, Random.Range(-8, 10));
        }
        return Time.time;
    }
    public Vector3 RandomPoint()
    {
        return new Vector3(Random.Range(-19, 19), 0, Random.Range(-8, 10));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
