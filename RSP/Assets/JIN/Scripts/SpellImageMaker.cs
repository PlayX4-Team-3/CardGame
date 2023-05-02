using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SpellImageMaker : Singleton<SpellImageMaker>
{
    private Sprite[] sprites;
    public List<GameObject> spells;

    void Start()
    {
        sprites = Resources.LoadAll<Sprite>("Spells");

        for (int i = 0; i < sprites.Length; i++)
        {
            GameObject go = new GameObject(sprites[i].name);
            go.transform.SetParent(this.transform);

            go.AddComponent<SpriteRenderer>().sprite = sprites[i];
            go.GetComponent<SpriteRenderer>().sortingOrder = 10;

            spells.Add(go);

            go.SetActive(false);
        }
    }

    public void SetSpell(GameObject target, GameObject spell)
    {
        spell.SetActive(true);

        spell.transform.SetParent(target.gameObject.transform);
        spell.transform.position = target.transform.position;
        spell.transform.localScale = Vector3.zero;
    }
}
