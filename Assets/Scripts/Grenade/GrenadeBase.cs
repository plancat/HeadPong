using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBase : MonoBehaviour
{
    [SerializeField]
    private GameObject grenadePrefab;
    [SerializeField]
    private GameObject currentGrenade;

    public float throwDelay;
    public float hideDelay;
    public float showDelay;

    public int cur = 3;

    public bool isThrow = false;

    public void Throw()
    {
        if (!isThrow)
        {
            cur -= 1;

            isThrow = true;

            StartCoroutine(GrenadeThrow());
            StartCoroutine(HideGrenadeTimer());
        }
    }

    private void Update()
    {
        if (cur <= 0)
        {
            currentGrenade.gameObject.SetActive(false);
        }
        else
        {
            currentGrenade.gameObject.SetActive(true);
        }

        GameMng.GetInstance.BulletText.text = cur.ToString();
        GameMng.GetInstance.MaxBulletText.text = "3";
    }

    IEnumerator GrenadeThrow()
    {
        yield return new WaitForSeconds(throwDelay);

        GameObject tempGrenade = Instantiate(grenadePrefab);
        tempGrenade.transform.position = currentGrenade.transform.position;
        tempGrenade.transform.GetComponent<Rigidbody>().velocity = Player.GetInstance.playerCamera.transform.forward * 10;
        tempGrenade.transform.localScale = Vector3.one;
        tempGrenade.SetActive(true);
    }

    IEnumerator HideGrenadeTimer()
    {
        yield return new WaitForSeconds(hideDelay);
        currentGrenade.SetActive(false);

        yield return new WaitForSeconds(showDelay);
        currentGrenade.SetActive(true);

        yield return new WaitForSeconds(0.5f);
        isThrow = false;
    }
}
