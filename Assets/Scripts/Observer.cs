using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public GameObject player;
    public GameEnding gameEnding;
    bool m_IsPlayerInRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        //如果玩家进入灯光范围，用射线模拟视线检测
        if (m_IsPlayerInRange)
        {
            //射线方向为从敌人指向玩家
            Vector3 direction = player.transform.position + Vector3.up - transform.position;
            Ray ray = new Ray(transform.position, direction);   //(射线的起点,方向)
            RaycastHit raycastHit;                              //储存射线的碰撞信息                 

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player.transform)    //如果射线打中的是玩家       
                {
                    gameEnding.CaughtPlayer();                  //玩家被抓
                }
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
            m_IsPlayerInRange = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            m_IsPlayerInRange = false;
    }
}
