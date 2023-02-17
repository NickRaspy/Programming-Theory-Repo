using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomStairGenerator : MonoBehaviour
{
    public GameObject stair;
    public GameObject currentPlatform;
    public GameObject platform;
    public GameObject player;
    public GameObject spawnpoint;

    private bool isAMTButtonClicked = false;
    private int spawnedStairs = 0;

    public List<GameObject> startStairs = new List<GameObject>();
    private void StairSpawn(List<GameObject> stairs, bool isMain) //main room generator
    {
        int rndAmount;
        if (isMain) rndAmount = Random.Range(1, 5);
        else rndAmount = Random.Range(1, 4);
        spawnedStairs = rndAmount;
        if (rndAmount > 1)
        {
            int[] stairsAmount = new int[rndAmount];
            for (int i = 0; i < rndAmount; i++)
            {
                int rndChoice;
                if (isMain) rndChoice = Random.Range(1, 5);
                else rndChoice = Random.Range(1, 4);
                if (i == 0)
                {
                    stairsAmount[i] = rndChoice;
                }
                else
                {
                    while (true)
                    {
                        bool isFound = true;
                        for (int j = 0; j < i; j++)
                        {
                            if (rndChoice == stairsAmount[j])
                            {
                                if(isMain)rndChoice = Random.Range(1, 5);
                                else rndChoice = Random.Range(1, 4);
                                isFound = false;
                            }
                            else
                            {
                                stairsAmount[i] = rndChoice;
                            }
                        }
                        if (isFound)
                        {
                            break;
                        }
                    }
                }
            }
            for (int i = 0; i < rndAmount; i++)
            {
                GameObject newStair = Instantiate(stair, StairChoice(stairsAmount[i]).position, StairChoice(stairsAmount[i]).rotation);
                Collider[] hcs = Physics.OverlapSphere(newStair.transform.Find("PEnd").position, 1f);
                bool canSpawnPlatform = true;
                foreach (var hc in hcs)
                {
                    if (hc.CompareTag("Platform"))
                    {
                        Debug.Log("Detected");
                        canSpawnPlatform = false;
                        break;
                    }
                }
                if (canSpawnPlatform)
                {
                    GameObject p = Instantiate(platform, newStair.transform);
                    p.name = "Platform";
                    stairs.Add(newStair);
                }
            }
        }
        else
        {
            int rndChoice;
            if (isMain)rndChoice = Random.Range(1, 5);
            else rndChoice = Random.Range(1, 4);
            GameObject newStair = Instantiate(stair, StairChoice(rndChoice).position, StairChoice(rndChoice).rotation);
            Collider[] hcs = Physics.OverlapSphere(newStair.transform.Find("PEnd").position, 1f);
            bool canSpawnPlatform = true;
            foreach (var hc in hcs)
            {
                if (hc.CompareTag("Platform"))
                {
                    Debug.Log("Detected");
                    canSpawnPlatform = false;
                    break;
                }
            }
            if (canSpawnPlatform)
            {
                GameObject p = Instantiate(platform, newStair.transform);
                p.name = "Platform";
                stairs.Add(newStair);
            }
        }
    }
    private Transform StairChoice(int n) //random choice of stairs
    {
        Transform newTransform = transform;
        switch (n)
        {
            case 1:
                newTransform = currentPlatform.transform.Find("PUp").transform;
                break;
            case 2:
                newTransform = currentPlatform.transform.Find("PRight").transform;
                break;
            case 3:
                newTransform = currentPlatform.transform.Find("PLeft").transform;
                break;
            case 4:
                newTransform = currentPlatform.transform.Find("PDown").transform;
                break;
        }
        return newTransform;
    }
    public void BranchMechacnicTest()
    {
        if (GameObject.FindGameObjectWithTag("Stair"))
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Stair"))
                Destroy(go);
            startStairs.Clear();
            currentPlatform = GameObject.Find("StartPlatform");
        }
        isAMTButtonClicked = true;
        int maxRooms = Random.Range(5, 10);
        int sumRooms = 0;
        StairSpawn(startStairs, true); sumRooms += spawnedStairs;
        while(sumRooms < maxRooms)
        {
            List<GameObject> newStairs = new List<GameObject>();
            for(int i = 0; i < startStairs.Count; i++)
            {
                spawnedStairs = 0;
                currentPlatform = startStairs[i].transform.Find("Platform").gameObject;
                StairSpawn(newStairs, false);
                sumRooms += spawnedStairs;
            }
            startStairs.Clear();
            startStairs = newStairs;
        }
    }
    public IEnumerator CDTrick()
    {
        yield return new WaitForSeconds(0.5f);
    }
    public void CameraSet()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Transform t = GameObject.Find("Main Camera").transform;
            t.position = GameObject.FindGameObjectWithTag("Player").transform.position;
            t.rotation = GameObject.FindGameObjectWithTag("Player").transform.rotation;
            t.SetParent(GameObject.FindGameObjectWithTag("Player").transform);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("No Player Found!");
        }
    }
    public void PlayerSpawn()
    {
        Instantiate(player, spawnpoint.transform.position, player.transform.rotation);
    }
    public void CleanAll()
    {
        if (GameObject.FindGameObjectWithTag("Stair"))
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Stair"))
                Destroy(go);
            startStairs.Clear();
            currentPlatform = GameObject.Find("StartPlatform");
        }
    }
}