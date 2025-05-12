using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour, IInteractable
{
    public static Reward Instance;

    //set within this script, get publicly
    public bool isOpened;
    //public string RewardID { get; private set; }
    //public GameObject itemPrefab; //item the chest drops, the reward. lets change it to a ui interface
    [Header("Reward Manager")]
    public GameObject rewardsPanel;
    public GameObject[] rewardButtonPrefabs;
    public Transform rewardOneContainer;
    public Transform rewardTwoContainer;
    public Transform rewardThreeContainer;
    //List<GameObject> _rewards = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //RewardID ??= GlobalHelper.GenerateUniqueID(gameObject);

        //rewardButtonPrefabs = GameObject.FindGameObjectsWithTag("Rewards");
        //_rewards.Add(Instantiate(rewardButtonPrefabs));
    }

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

        void Update()
    {
        //if (isOpened)
        //{
        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        CloseMenu();
        //    }
        //}
    }

    public bool CanInteract()
    {
        return !isOpened;
    }

    public void Interact()
    {
        if (!CanInteract()) return;
        //open chest
        OpenReward();
    }

    private void OpenReward()
    {
        SetOpened(true);

        //drop item
        /*if (itemPrefab)
        {
            GameObject droppedItem = Instantiate(itemPrefab, transform.position + Vector3.down, Quaternion.identity);
        }*/

        //instantiate random buttons into the containers
    }

    public void SetOpened(bool opened)
    {
        if (isOpened = opened)
        {
            ShowRewardUI(true);
        }
    }

    public void ShowRewardUI(bool show)
    {
        rewardsPanel.SetActive(show);


        Debug.Log("instantiated");
        Instantiate(rewardButtonPrefabs[Random.Range(0, 2)], rewardOneContainer.position, rewardOneContainer.rotation, rewardOneContainer.transform);
        Instantiate(rewardButtonPrefabs[Random.Range(3, 5)], rewardTwoContainer.position, rewardTwoContainer.rotation, rewardTwoContainer.transform);
        Instantiate(rewardButtonPrefabs[Random.Range(6, 8)], rewardThreeContainer.position, rewardThreeContainer.rotation, rewardThreeContainer.transform);
        //setpause
        Time.timeScale = 0;
    }

    public void CloseMenu()
    {
        //set pause false
        Time.timeScale = 1;
        rewardsPanel.SetActive(false);
        SetOpened(false);
        Destroy(gameObject);
        Debug.Log("destroyed");
    }
}
