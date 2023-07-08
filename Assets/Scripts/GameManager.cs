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

    // Update is called once per frame
    void Update()
    {
        
    }
}
