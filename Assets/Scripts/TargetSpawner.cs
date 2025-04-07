using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;

    public float generateTime = 3f;

    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            timer = generateTime;

            float xPsoition = Random.Range(-10f, 10f);
            Vector3 newPos = new Vector3(xPsoition, 0, 0);
            Instantiate(targetPrefab, newPos, Quaternion.identity);
        }
    }
}
