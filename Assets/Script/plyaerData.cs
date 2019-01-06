using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plyaerData : MonoBehaviour {
    private Dictionary<string, SkinnedMeshRenderer> playerData = new Dictionary<string, SkinnedMeshRenderer>();
    Transform[] playerBips;
    List<Transform> bones = new List<Transform>();
    public List<GameObject> hatList = new List<GameObject>();
    public List<GameObject> handList = new List<GameObject>();
    // Use this for initialization
    void Start()
    {
        playerBips = gameObject.GetComponentsInChildren<Transform>();
        SkinnedMeshRenderer[] _playerData = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (var item in _playerData)
        {
            string name = item.name;
            if (!playerData.ContainsKey(name))
            {
                playerData.Add(name, item.GetComponent<SkinnedMeshRenderer>());
            }
        }
    }

    void ChangeMesh(string partName, GameObject equips)
    {
        SkinnedMeshRenderer partSMR = playerData[partName];//原始部位
        SkinnedMeshRenderer equipsSMR = equips.GetComponentInChildren<SkinnedMeshRenderer>();//新装备
        bones.Clear();
        foreach (var equipsBone in equipsSMR.bones)
        {
            print(equipsBone.name + "-----------" );
            foreach (var playerBone in playerBips)
            {
                if (equipsBone.name == playerBone.name)
                {
                    bones.Add(playerBone);
                    break;
                }
            }
        }
        partSMR.bones = bones.ToArray();
        partSMR.sharedMaterials = equipsSMR.sharedMaterials;
        partSMR.sharedMesh = equipsSMR.sharedMesh;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GameObject newHand = handList[Random.Range(0, handList.Count)];
            GameObject newHat = hatList[Random.Range(0, hatList.Count)];
            ChangeMesh("hand", newHand);
            ChangeMesh("hat", newHat);
        }
    }
}
