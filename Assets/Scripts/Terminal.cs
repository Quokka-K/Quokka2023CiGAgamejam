using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))//��ҵִ��յ�
        {
            GameManager.instance.GameWin();
        }
    }
}
