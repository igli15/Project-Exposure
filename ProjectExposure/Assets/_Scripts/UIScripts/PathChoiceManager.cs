using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PathChoiceManager : MonoBehaviour {
    public GameObject button;
    public static GameObject instance;
    public void Start()
    {
        instance = this.gameObject;
    }

    public void GenerateButtons(List<Path> paths)
    {
        if (paths.Count == 0) return;
        int i = 0;
        foreach (Path path in paths)
        {
            GameObject newButton = GameObject.Instantiate(button, transform.position, transform.rotation, transform);
            newButton.transform.position += new Vector3(150*i,0, 0);
            newButton.GetComponent<Button>().onClick.AddListener(delegate { OnClick(path); });
            i++;
        }

    }

    void OnClick(Path path) {
        Debug.Log("NEW PATH IS " + path);
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        RailMovement.instance.SetPoint(path.GetFirstPoint());
        RailMovement.instance.StartMovement();
    }
}
