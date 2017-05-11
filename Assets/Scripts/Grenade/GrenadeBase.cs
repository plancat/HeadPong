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

    bool isThrow = false;

    public void Throw()
    {
        if (!isThrow)
        {
            isThrow = true;

            StartCoroutine(GrenadeThrow());
            StartCoroutine(HideGrenadeTimer());
        }
    }

    IEnumerator GrenadeThrow()
    {
        yield return new WaitForSeconds(throwDelay);
        isThrow = false;

        GameObject tempGrenade = Instantiate(grenadePrefab,
            currentGrenade.transform.position,
            currentGrenade.transform.rotation);

        tempGrenade.transform.localScale = Vector3.one;
        tempGrenade.SetActive(true);
    }

    IEnumerator HideGrenadeTimer()
    {
        yield return new WaitForSeconds(hideDelay);
        currentGrenade.SetActive(false);

        yield return new WaitForSeconds(showDelay);
        currentGrenade.SetActive(true);
    }
}
