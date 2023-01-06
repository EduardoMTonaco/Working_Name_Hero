using System.Collections;
using System.Collections.Generic;
using Assets;
using Unity.VisualScripting;
using UnityEngine;

public class CreateMinion : MonoBehaviour
{
    public MovementAgente Minion;
    private float contadorTempo;
    public float createTimeWait = 10;
    public int HeightLista = 10;
    public List<GameObject> AgenteList = new List<GameObject>();
    private CharactereList CharList;

    // Start is called before the first frame update
    void Awake()
    {
        
        contadorTempo = 45;
        PopulateList();


    }

    // Update is called once per frame
    void Update()
    {
        contadorTempo += Time.deltaTime;
        if (contadorTempo >= createTimeWait)
        {
            CreateMinionOnMap();
            contadorTempo = 0;
        }

    }
    private void CreateMinionOnMap()
    {
        foreach(GameObject obj in AgenteList) 
        {
            if(obj.gameObject.activeSelf == false)
            {
                obj.gameObject.SetActive(true);
                obj.transform.position = this.transform.position;
                obj.GetComponent<MovementAgente>().Revive();
                return;
            }
        
        }
    }

    private void PopulateList()
    {
        for(int i = 0; i < HeightLista; i++) 
        {
            var a = Instantiate(Minion, transform.position, transform.rotation);
            a.gameObject.SetActive(false);
            AgenteList.Add(a.gameObject);
        }
    }
}
