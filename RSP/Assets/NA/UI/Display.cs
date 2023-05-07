using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AllCharacter;
using ObserverPattern;

public class Display : MonoBehaviour
{
    /// <summary>
    /// ���� ������ ����
    /// </summary>
    [SerializeField]
    private GameData data;

    /// <summary>
    /// �÷��̾� ������
    /// </summary>
    [SerializeField]
    private Player player;

    /// <summary>
    /// �� ������
    /// </summary>
    [SerializeField]
    private Enemy enemy;

    private void Start()
    {
        enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();

        this.player.DataInit(this.data);
        this.enemy.DataInit(this.data);

        //�������� ���
        this.data.RegisterObserver(this.player);
        this.data.RegisterObserver(this.enemy);    
    }

    public void UpdateCharacterState()
    {
        //���������� �ʱ�ȭ
        this.data.UpdateDisplay(this.player.Hp, this.enemy.Hp, this.player.Defense_Figures, this.enemy.Defense_Figures);
    }

}
