using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wall : MonoBehaviour
{

    public List<Sprite> listaDamagedSprites;
    public int hp = 4;

    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DamagedWall(int loss)
    {
        IntRange randomSprite = new IntRange(0, listaDamagedSprites.Count -1);

        int random = randomSprite.Random;
        Debug.Log("Cantidad sprites: " + listaDamagedSprites.Count + ", NumRandom:  " + random);
        spriteRenderer.sprite = (Sprite)listaDamagedSprites[random];
        hp -= loss;
        if (hp <= 0)
            gameObject.SetActive(false);
    }
}
