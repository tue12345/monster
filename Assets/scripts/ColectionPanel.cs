using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColectionPanel : MonoBehaviour
{
    [SerializeField] GameObject colectionPrefap;
    [SerializeField] Transform parent;
    [SerializeField] List<GameObject> Box = new List<GameObject>();
    [SerializeField] RectTransform content;

    ColectionBox box;
    int colectionbox = 0, boxnumber = 0;
    GameObject go;
    // Start is called before the first frame update
    void Start()
    {

        foreach (int i in PlayerData.current.Head)
        {
            colectionbox++;
        }
        for (int i = 0; i < colectionbox; i++)
        {
            go = Instantiate(colectionPrefap, parent);
            Box.Add(go);

            //go.transform.SetParent(parent);
        }
        foreach (GameObject i in Box)
        {
            box = i.GetComponent<ColectionBox>();
            box.BoxNumber1 = boxnumber;
            boxnumber++;
        }
        if (colectionbox % 2 == 0)
        {
            content.sizeDelta = new Vector2(1378.9f, ((colectionbox / 2) + 1) * 750);
            if (content.sizeDelta.y < 1815)
            {

                content.sizeDelta = new Vector2(1378.9f, 1815);
            }
            
        }
        else
        {
            content.sizeDelta = new Vector2(1378.9f, (((colectionbox / 2)+2) * 750));
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
