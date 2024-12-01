using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class laserBeam : MonoBehaviour
{
    private void Update()
    {
        sadsa();
    }

    private IEnumerator sadsa()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
