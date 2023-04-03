using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PlayerID
{
    Player,
    Enemy
}

public class TurnEndEvent : UnityEvent<PlayerID> { }

public class TurnManager : Singleton<TurnManager>
{
    public List<PlayerID> players;
    public PlayerID currentPlayer;

    public TurnEndEvent onTurnEnd = new TurnEndEvent();

    public void StartTurn(PlayerID playerID)
    {
        currentPlayer = playerID;
        //Debug.Log(currentPlayer + "플레이어의 턴이 시작됩니다.");
    }

    public void EndTurn()
    {
        //Debug.Log(currentPlayer + "의 턴이 종료됩니다.");
        int currentIndex = players.IndexOf(currentPlayer);
        int nextIndex = (currentIndex + 1) % players.Count;
        currentPlayer = players[nextIndex];
        onTurnEnd?.Invoke(currentPlayer);
    }
}
