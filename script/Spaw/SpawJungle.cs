using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawJungle : MonoBehaviour
{
    public JungleAgentMovement JungleMinion;
    public float TimeToSpaw = 30;
    [HideInInspector]
    public bool ThereIsMinion;
    private float spawTimeCount = 30;

    public void Start()
    {
        ThereIsMinion = false;
    }
    // Update is called once per frame
    void Update()
    {
        SpawTime();
    }
    public void SpawJungler()
    {
        if(!ThereIsMinion)
        {
            JungleAgentMovement jungle = Instantiate(JungleMinion, transform.position, transform.rotation);
            jungle.Spaw = this;
            ThereIsMinion = true;
        }
    }
    public void SpawTime()
    {
        if(!ThereIsMinion)
        {
            spawTimeCount += Time.deltaTime;
        }
        if(spawTimeCount > TimeToSpaw)
        {
            SpawJungler();
            spawTimeCount = 0;
        }
    }
}
