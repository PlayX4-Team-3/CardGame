using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private CardManager cm;
    private TurnManager tm;

    private void Start()
    {
        cm = CardManager.Instance;
        tm = TurnManager.Instance;

        cm.DeckInstantiate();
        cm.DeckShuffle(cm.copiedPlayerDeck);

        tm.onTurnEnd.AddListener(OnTurnEnd);
        tm.StartTurn(PlayerID.Player);

        cm.Draw();
    }

    public void OnTurnEnd(PlayerID nextPlayer)
    {
        tm.StartTurn(nextPlayer);
        if (nextPlayer == PlayerID.Player)
            cm.Draw();
    }
}
