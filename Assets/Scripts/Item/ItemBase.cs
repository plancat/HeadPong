using UnityEngine;

public class ItemBase : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.name.CompareTo("Player") == 0)
        {
            Player.GetInstance.health += 50;
            if (Player.GetInstance.health > 100)
            {
                Player.GetInstance.health = 100;
            }

            gameObject.SetActive(false);
        }
    }
}