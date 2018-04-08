using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerPart>() != null)
        {
            Destroy(gameObject);
            GameController.Instance.TreasureCollected();
        }
    }
}
