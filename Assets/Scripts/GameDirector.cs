using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public enum RewardType
    {
        Health,
        Heal,
        Damage,
        Armor,
        Speed,
        DamagePenetration,
    }

    public struct RoomDefinition
    {
        public int DoorCount;
        public int RoomType;
        public RewardType reward;
        public int RewardScalar;
        public int EnemyPoints;
        public int EnemyCountLimit;
        public GameObject RoomPrefab;
        public Array DoorOptions;
    }

    public struct DoorDefinition
    {
        public RewardType DoorType;
        public GameObject DoorPrefab;
    }

    //Room and Enemy Definitions
    public int RoomClearCount = 0;
    public int EnemyBudget = 0;

    //Player and Room Locations
    public GameObject RoomZeroLocation;
    public GameObject Player;

    //Room and Door Definitions
    public RoomDefinition room;
    public RoomDefinition NewRoom;
    public List<GameObject> RoomList = new List<GameObject>();
    public List<DoorDefinition> DoorList = new List<DoorDefinition>();

    //currency
    public int OldCoin;
    public int BloodCoin;
    public int SoulCoin;
    public int GoldCoin;


    //Teleport to start room
    void RoomZero()
    {
        Player.transform.position = RoomZeroLocation.transform.position;
        GenerateRoom();
    }

    //On room exit, generate new room
    void GenerateRoom()
    {
        //on collision with door, wipe old room and generate new room



    }

    //On room clear, generate reward
    void RoomClear()
    {
        RoomClearCount++;
        DispenseReward();
        DoorGeneration();
    }

    void DispenseReward()
    {
        ////Dispense reward based on room reward type
        //switch (room.reward)
        //{
        //    case RewardType.Health:
        //        Player.GetComponent<PlayerController>().Health += room.RewardScalar;
        //        break;
        //    case RewardType.Heal:
        //        Player.GetComponent<PlayerController>().Heal();
        //        break;
        //    case RewardType.Damage:
        //        Player.GetComponent<PlayerController>().StatIncrease();
        //        break;
        //    case RewardType.Armor:
        //        Player.GetComponent<PlayerController>().StatIncrease();
        //        break;
        //    case RewardType.Speed:
        //        Player.GetComponent<PlayerController>().StatIncrease();
        //        break;
        //    case RewardType.DamagePenetration:
        //        Player.GetComponent<PlayerController>().StatIncrease();
        //        break;
        //}

        //Dispense currency based on room clear count
        if (RoomClearCount > 5)
        {
            //Dispense OldCoin
        }
        else if (RoomClearCount > 10)
        {
            //Dispense BloodCoin
        }
        else if (RoomClearCount > 15)
        {
            //Dispense SoulCoin
        }
        else if (RoomClearCount > 20)
        {
            //Dispense GoldCoin
        }
    }

    void DoorGeneration()
    {
        if(RoomClearCount < 10)
        {
            room.DoorCount = 1;
        }
        else if (RoomClearCount < 20)
        {
            room.DoorCount = 2;
        }
        else if (RoomClearCount < 30)
        {
            room.DoorCount = 3;
        }
        else if (RoomClearCount < 40)
        {
            room.DoorCount = 4;
        }

        //for each door, generate a room
        for (int i = 0; i < room.DoorCount; i++)
        {
           //add door to door list
           DoorList.Add(new DoorDefinition());
            //set door type to random reward type
            //DoorList[i].DoorType = (RewardType)UnityEngine.Random.Range(0, 5);
        }
        //for each door activate door
        for (int i = 0; i < room.DoorCount; i++)
        {
            //activate door
        }
    }

    //On room fail, respawn player in hub
    void RoomFail()
    {
    }
}
