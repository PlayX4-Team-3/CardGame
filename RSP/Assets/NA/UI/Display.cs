using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AllCharacter;
using ObserverPattern;

public class Display : MonoBehaviour
{
    /// <summary>
    /// 게임 데이터 관리
    /// </summary>
    [SerializeField]
    private GameData data;

    /// <summary>
    /// 플레이어 데이터
    /// </summary>
    [SerializeField]
    private Player player;
    /// <summary>
    /// 적 데이터
    /// </summary>
    [SerializeField]
    private Enemy enemy;

    private void Start()
    {
        this.player.DataInit(this.data);
        this.enemy.DataInit(this.data);

        //옵저버의 등록
        this.data.RegisterObserver(this.player);
        this.data.RegisterObserver(this.enemy);    
    }

    private void Update()
    {
        //옵저버들의 초기화
        this.data.UpdateHpText(this.player.Hp, this.enemy.Hp);
    }

}
