using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{


    public float fadeDuration = 1f;                     //淡入时间1S
    public float displayImageDuration = 1f;             //淡入完毕后等待时间1S
    public GameObject player;                           //玩家
    public CanvasGroup exitBackgroundImageCanvasGroup;  //用于调节通关时alpha值的CanvasGroup
    public CanvasGroup caughtBackgroundImageCanvasGroup;  //用于调节被抓时alpha值的CanvasGroup

    private bool m_IsPlayerAtExit;                              //玩家是否到终点
    private bool m_IsPlayerCaught;                              //玩家是否被抓


    float m_Timer;                                      //计时器  

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup,false);
        }
        else if(m_IsPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
            m_IsPlayerAtExit = true;
    }
    void EndLevel(CanvasGroup imgCanvasGroup,bool doRestart)
    {
        m_Timer += Time.deltaTime; //计时器，每帧累加时间

        //alpha值随着百分比改变
        imgCanvasGroup.alpha = m_Timer / fadeDuration;

        //当计时器时间大于总等待时间
        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if(doRestart)
                SceneManager.LoadScene(0);
            else
                Application.Quit();//退出游戏，这个要游戏打包后才会执行，在Unity里不会关闭游戏。
        }
    }
}
