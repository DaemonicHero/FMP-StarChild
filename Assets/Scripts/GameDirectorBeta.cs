using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class GameDirectorBeta : MonoBehaviour
{
    //Room Variables
    public int roomCount = 0; //How many rooms have been cleared
    public GameObject doorOne;
    public GameObject doorTwo;
    public GameObject doorThree;
    public GameObject doorFour;
    //spawn location for enemy
    public GameObject enemySpawnLocation;
    public GameObject enemySpawnLocation2;
    public GameObject enemySpawnLocation3;
    public GameObject enemySpawnLocation4;
    //spawn location for loot
    public GameObject loot;
    //player hub
    public GameObject playerHub;


    [System.Serializable]
    public struct Room
    {
        public GameObject door1;
        public GameObject door2;
        public GameObject door3;
        public GameObject door4;
        public GameObject loot;
        public GameObject playerSpawn;
        public GameObject enemySpawn1;
        public GameObject enemySpawn2;
        public GameObject enemySpawn3;
        public GameObject enemySpawn4;
    }
    public List<Room> rooms = new List<Room>();
    public Room currentRoom;
    //room zero is the starting room. It is always the same.
    public Room roomZero;

    public LootType lootType;

    public int enemyCount; //How many enemies are currently in the room
    public int enemyPoints; //How many enemies can be spawned
    public int enemyCountLimit; //How many enemies can be in the room at once
    public GameObject enemyPrefab;
    public bool spawnCooldowns = false;

    public GameObject player;
    bool roomCleared = false;
    public bool runInProgress = false;
    public bool runFinished = false;
    public bool transitionInProgress = false;

    //Scalar Multipliers
    //enemy limit multiplier 2/3rds of room count
    public float enemyLimitMultiplier = 1;
    //enemy stats multiplier +.2 per room
    public float enemyStatsMultiplier = 1;
    //rewards multiplier +.1 per room
    public float rewardsMultiplier = 1;


    public enum LootType
    {
        Health,
        Armor,
        Damage,
        Speed,
        DamagePenetration,
        Healing
    }

    public void LootPickup()
    {
        if (lootType == LootType.Health)
        {
            //player max health increase
            player.GetComponent<PlayerController>().maxHealth += (1 * rewardsMultiplier);

        }
        else if (lootType == LootType.Armor)
        {
            //player armor increase
            player.GetComponent<PlayerController>().armor -= (0.1f);
            //player armor cannot be less than 0.1
            if (player.GetComponent<PlayerController>().armor < 0.1f)
            {
                player.GetComponent<PlayerController>().armor = 0.1f;
            }
        }
        else if (lootType == LootType.Damage)
        {
            //player damage increase
            player.GetComponent<PlayerController>().damage += (1 * rewardsMultiplier);
        }
        else if (lootType == LootType.Speed)
        {
            //player speed increase
            player.GetComponent<PlayerController>().speed += (0.1f * rewardsMultiplier);
        }
        else if (lootType == LootType.DamagePenetration)
        {
            //player damage penetration increase
            player.GetComponent<PlayerController>().damagePenetration += (0.1f * rewardsMultiplier);
        }
        else if (lootType == LootType.Healing)
        {
            //player heal
            player.GetComponent<PlayerController>().Heal(5* rewardsMultiplier);
            //health can't be more than max health
            if (player.GetComponent<PlayerController>().health > player.GetComponent<PlayerController>().maxHealth)
            {
                player.GetComponent<PlayerController>().health = player.GetComponent<PlayerController>().maxHealth;
            }
        }
        loot.SetActive(false);
    }

    public void RoomClear()
    {
            roomCount++;
           
            //spawn doors
            int DoorNumber = Random.Range(0, 4);
            if (DoorNumber == 0)
            {
                doorOne.SetActive(true);
                doorOne.GetComponent<Door>().DoorSet();
            }
            else if (DoorNumber == 1)
            {
                doorOne.SetActive(true);
                doorOne.GetComponent<Door>().DoorSet();
                doorTwo.SetActive(true);
                doorTwo.GetComponent<Door>().DoorSet();
            }
            else if (DoorNumber == 2)
            {
                doorOne.SetActive(true);
                doorOne.GetComponent<Door>().DoorSet();
                doorTwo.SetActive(true);
                doorTwo.GetComponent<Door>().DoorSet();
                doorThree.SetActive(true);
                doorThree.GetComponent<Door>().DoorSet();
            }
            else if (DoorNumber == 3)
            {
                doorOne.SetActive(true);
                doorOne.GetComponent<Door>().DoorSet();
                doorTwo.SetActive(true);
                doorTwo.GetComponent<Door>().DoorSet();
                doorThree.SetActive(true);
                doorThree.GetComponent<Door>().DoorSet();
                doorFour.SetActive(true);
                doorFour.GetComponent<Door>().DoorSet();
            }
            //spawn loot
            loot.SetActive(true);
            roomCleared = true;
        
    }

    public void EnemySpawn()
    {
        if (enemyCount < enemyCountLimit)
        {
            //spawn enemy
            enemyCount++;
            enemyPoints--;
            //spawn enemy at random location
            int EnemySpawn = Random.Range(0, 4);
            if (EnemySpawn == 0 && enemyPrefab != null)
            {
                GameObject enemyInstance = Instantiate(enemyPrefab, enemySpawnLocation.transform.position, Quaternion.identity);
                EnemyAI enemyAI = enemyInstance.GetComponent<EnemyAI>();
                if (enemyAI != null)
                {
                    enemyAI.GameDirector = this.gameObject;
                    enemyAI.player = player.transform;
                    enemyAI.playerBody = player;
                    // Set the enemy's max health, current health, and damage based on the enemyStatsMultiplier
                    enemyAI.maxHealth = 10 * enemyStatsMultiplier;
                    enemyAI.currentHealth = 10 * enemyStatsMultiplier;
                    enemyAI.damage = 1 * enemyStatsMultiplier;
                }
            }
            // Repeat the above null-check pattern for other EnemySpawn cases

            if (EnemySpawn == 1 && enemyPrefab != null)
            {
                GameObject enemyInstance = Instantiate(enemyPrefab, enemySpawnLocation2.transform.position, Quaternion.identity);
                EnemyAI enemyAI = enemyInstance.GetComponent<EnemyAI>();
                if (enemyAI != null)
                {
                    enemyAI.GameDirector = this.gameObject;
                    enemyAI.player = player.transform;
                    enemyAI.playerBody = player;
                    enemyAI.maxHealth = 10 * enemyStatsMultiplier;
                    enemyAI.currentHealth = 10 * enemyStatsMultiplier;
                    enemyAI.damage = 1 * enemyStatsMultiplier;
                }
            }
            // Repeat the above null-check pattern for other EnemySpawn cases
            if (EnemySpawn == 2 && enemyPrefab != null)
            {
                GameObject enemyInstance = Instantiate(enemyPrefab, enemySpawnLocation3.transform.position, Quaternion.identity);
                EnemyAI enemyAI = enemyInstance.GetComponent<EnemyAI>();
                if (enemyAI != null)
                {
                    enemyAI.GameDirector = this.gameObject;
                    enemyAI.player = player.transform;
                    enemyAI.playerBody = player;
                    enemyAI.maxHealth = 10 * enemyStatsMultiplier;
                    enemyAI.currentHealth = 10 * enemyStatsMultiplier;
                    enemyAI.damage = 1 * enemyStatsMultiplier;
                }
            }
            // Repeat the above null-check pattern for other EnemySpawn cases
            if (EnemySpawn == 3 && enemyPrefab != null)
            {
                GameObject enemyInstance = Instantiate(enemyPrefab, enemySpawnLocation4.transform.position, Quaternion.identity);
                EnemyAI enemyAI = enemyInstance.GetComponent<EnemyAI>();
                if (enemyAI != null)
                {
                    enemyAI.GameDirector = this.gameObject;
                    enemyAI.player = player.transform;
                    enemyAI.playerBody = player;
                    enemyAI.maxHealth = 10 * enemyStatsMultiplier;
                    enemyAI.currentHealth = 10 * enemyStatsMultiplier;
                    enemyAI.damage = 1 * enemyStatsMultiplier;
                }
            }
            // Repeat the above null-check pattern for other EnemySpawn cases

        }

    }

    public void EnemyDeath()
    {
        if (enemyCount > 0)
        {
            //enemy death
            enemyCount--;
        }
    }

    public IEnumerator SpawnCooldown()
    {
        yield return new WaitForSeconds(1);
        spawnCooldowns = false;
    }

    //hide doors
    public void DoorHide()
    {
        doorOne.SetActive(false);
        doorTwo.SetActive(false);
        doorThree.SetActive(false);
        doorFour.SetActive(false);
        roomCleared = false;
        NextRoom();
    }

    void NextRoom()
    {
        //random next room
        int NextRoom = Random.Range(1, rooms.Count);
        //set current room to next room
        currentRoom = rooms[NextRoom];
        //set enemy spawn locations to next room enemy spawn locations
        enemySpawnLocation = currentRoom.enemySpawn1;
        enemySpawnLocation2 = currentRoom.enemySpawn2;
        enemySpawnLocation3 = currentRoom.enemySpawn3;
        enemySpawnLocation4 = currentRoom.enemySpawn4;
        //set loot location to next room loot location
        loot = currentRoom.loot;
        //set door locations to next room door locations
        doorOne = currentRoom.door1;
        doorTwo = currentRoom.door2;
        doorThree = currentRoom.door3;
        doorFour = currentRoom.door4;
        //set player spawn location to next room player spawn location
        player.transform.position = currentRoom.playerSpawn.transform.position;
        enemyLimitMultiplier = (roomCount/3) *2;
        //round enemy limit multiplier to nearest whole number
        enemyCountLimit = Mathf.RoundToInt(enemyLimitMultiplier);
        if (enemyCountLimit < 5)
        {
            enemyCountLimit = 5;
        }
        enemyStatsMultiplier += 0.2f;
        rewardsMultiplier += 0.1f;
        enemyPoints = roomCount;
        //if enemy points are less than 5, set enemy points to 5
        if (enemyPoints < 5)
        {
            enemyPoints = 5;
        }
        lootType = (LootType)Random.Range(0, 6);
        transitionInProgress = false;
        //start spawn cooldown
    }

    public void Reset()
    {
        //reset room count
        roomCount = 0;
        //reset enemy count
        enemyCount = 0;
        //reset enemy points
        enemyPoints = 5;
        //reset enemy limit multiplier
        enemyLimitMultiplier = 1;
        //reset enemy stats multiplier
        enemyStatsMultiplier = 1;
        //reset rewards multiplier
        rewardsMultiplier = 1;
        //reset run in progress
        runInProgress = false;
        //reset run finished
        runFinished = false;
        //reset transition in progress
        transitionInProgress = false;
        //reset room cleared
        roomCleared = false;
        //reset current room to room zero
        currentRoom = roomZero;
        //set enemy spawn locations to room zero enemy spawn locations
        enemySpawnLocation = currentRoom.enemySpawn1;
        enemySpawnLocation2 = currentRoom.enemySpawn2;
        enemySpawnLocation3 = currentRoom.enemySpawn3;
        enemySpawnLocation4 = currentRoom.enemySpawn4;
        //set loot location to room zero loot location
        loot = currentRoom.loot;
        //set door locations to room zero door locations
        doorOne = currentRoom.door1;
        doorTwo = currentRoom.door2;
        doorThree = currentRoom.door3;
        doorFour = currentRoom.door4;
        //teleport player to player hub
        player.transform.position = playerHub.transform.position;
    }

    public void StartRun()
    {
        //teleport player to room zero
        player.transform.position = roomZero.playerSpawn.transform.position;
        runInProgress = true;
    }

    void Start()
    {
        roomZero = rooms[0];
        //set current room to room zero
        currentRoom = roomZero;
        //set enemy spawn locations to room zero enemy spawn locations
        enemySpawnLocation = currentRoom.enemySpawn1;
        enemySpawnLocation2 = currentRoom.enemySpawn2;
        enemySpawnLocation3 = currentRoom.enemySpawn3;
        enemySpawnLocation4 = currentRoom.enemySpawn4;
        //set loot location to room zero loot location
        loot.transform.position = currentRoom.loot.transform.position;
        //set door locations to room zero door locations
        doorOne.transform.position = currentRoom.door1.transform.position;
        doorTwo.transform.position = currentRoom.door2.transform.position;
        doorThree.transform.position = currentRoom.door3.transform.position;
        doorFour.transform.position = currentRoom.door4.transform.position;
        //set enemy count limit to 5
        enemyCountLimit = 5;
        //set enemy points to 5
        enemyPoints = 5;
    }

    //update is called once per frame
    void Update()
    {
        if (runInProgress == true)
        {
            if (transitionInProgress == false)
            {
                if (enemyCount < enemyCountLimit && !spawnCooldowns && enemyPoints > 0)
                {
                    spawnCooldowns = true;
                    EnemySpawn();
                    StartCoroutine(SpawnCooldown());
                }
                else if (enemyPoints == 0 && roomCleared == false && enemyCount == 0)
                {
                    //room clear
                    RoomClear();
                    transitionInProgress = true;
                }
            }
        }
    }
}