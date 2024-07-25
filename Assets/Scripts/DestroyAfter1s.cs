using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfter1s : MonoBehaviour
{
    public int delay = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyAfter(delay));
    }

    private IEnumerator DestroyAfter(int delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
