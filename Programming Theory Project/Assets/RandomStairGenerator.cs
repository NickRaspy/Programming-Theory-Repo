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
                GameObject p = Instantiate(platform, newStair.transform);
                p.name = "Platform";
                stairs.Add(newStair);
            }
        }
        else
        {
            int rndChoice;
            if (isMain)rndChoice = Random.Range(1, 5);
            else rndChoice = Random.Range(1, 4);
            GameObject newStair = Instantiate(stair, StairChoice(rndChoice).position, StairChoice(rndChoice).rotation);
            GameObject p = Instantiate(platform, newStair.transform);
            p.name = "Platform";
            stairs.Add(newStair);
        }
    }
    public void StairSpawn()//not main, for testing purpouse 
    {
        if (GameObject.FindGameObjectWithTag("Stair"))
        {
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Stair"))
                Destroy(go);
        }
        int rndAmount = Random.Range(1, 5);
        if (rndAmount > 1)
        {
            int[] stairsAmount = new int[rndAmount];
            for (int i = 0; i < rndAmount; i++)
            {
                int rndChoice = Random.Range(1, 5);
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
                                rndChoice = Random.Range(1, 5);
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
            string check = "";
            for (int i = 0; i < rndAmount; i++)
            {
                check += stairsAmount[i] + " ";
                Instantiate(stair, StairChoice(stairsAmount[i]).position, StairChoice(stairsAmount[i]).rotation);
            }
            Debug.Log(check);
        }
        else
        {
            int rndChoice = Random.Range(1, 5);
            Instantiate(stair, StairChoice(rndChoice).position, StairChoice(rndChoice).rotation);
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
        isAMTButtonClicked = true;
        int maxRooms = Random.Range(20, 31);
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
}
