using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singletan<GameManager>
{
    private CardManager dm;

    private bool isPlayerTurn = false;


    private void Awake()
    {
        dm = CardManager.Instance;

        dm.DeckInstantiate();
        dm.DeckShuffle(dm.copiedPlayerDeck);

        isPlayerTurn = true;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            dm.Draw();
            TurnEnd();
        }
    }

    public void TurnEnd()
    {
        isPlayerTurn = !isPlayerTurn;
    }
}
