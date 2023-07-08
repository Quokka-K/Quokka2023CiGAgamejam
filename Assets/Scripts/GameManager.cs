using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    [SerializeField] int stepMax; //最大步数
    [DoNotSerialize] public int stepRemain; //剩余步数

    public static GameManager instance;//GM单例
    private void Awake()
    {
        instance = this; 
    }
    void Start()
    {
        stepRemain = stepMax;//初始化步数
    }

    public void GameWin()
    {
        Debug.Log("游戏胜利！");
    }
    public void GameLose()
    {
        Debug.Log("游戏失败！");
    }
    public void ChangeStep(int step)
    {
        //改变剩余步数
        stepRemain += step;
        Debug.Log($"剩余步数：{stepRemain}，最大步数：{stepMax}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
