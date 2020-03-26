using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSprite : MonoBehaviour
{
    public Sprite cardFace;
    public Sprite cardBack;
    private SpriteRenderer spriteRenderer;
    private Selectable selectable;
    private Solitaire solitaire;

    // Start is called before the first frame update
    void Start()
    {
        // Initiate list of cards (deck)
        List<string> deck = Solitaire.GenerateDeck();
        solitaire = FindObjectOfType<Solitaire>();

        // Iterate through cards
        int i = 0;
        foreach (string card in deck)
        {
            // Giving the card a face
            if (this.name == card)
            {
                cardFace = solitaire.cardFaces[i];
                break;
            }
            i++;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
        selectable = GetComponent<Selectable>();
    }

    // Update is called once per frame
    void Update()
    {
        // If faceUp == true, show the card's face
        if (selectable.faceUp == true)
        {
            spriteRenderer.sprite = cardFace;
        }
        // If faceUp == false, show the back of the card
        else
        {
            spriteRenderer.sprite = cardBack;
        }
    }
}
