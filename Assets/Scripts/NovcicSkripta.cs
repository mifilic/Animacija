using UnityEngine;
public class Coin : MonoBehaviour
{
    // Granice tvoje platforme prema koordinatama (-8.51 do 6.28)
    public float minX = -8f;
    public float maxX = 6f;

    // Visina platforme je -4, pa se novèiæ stvara iznad nje unutar dosega skoka
    public float platformY = -1f;
    public float maxJumpHeight = 2.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Provjerava je li igraè dotaknuo novèiæ pomoæu taga
        if (collision.CompareTag("Player"))
        {
            Move player = collision.GetComponent<Move>();
            if (player != null)
            {
                player.AddScore();
            }

            // Umjesto uništavanja, premješta novèiæ na novu nasumiènu poziciju iznad platforme
            RespawnCoin();
        }
    }

    public void RespawnCoin()
    {
        float randomX = Random.Range(minX, maxX);

        // Buduæi da je centar na -4, gornji rub/trava je viši. 
        // Postavljamo raspon od -2.5 do -1.5 da novèiæ lebdi toèno iznad trave.
        float randomY = Random.Range(-2f, 0f);

        transform.position = new Vector2(randomX, randomY);
    }
}
