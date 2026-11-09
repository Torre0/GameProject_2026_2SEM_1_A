using System.Runtime.ConstrainedExecution;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questText;
    private Quest currentQuest;

    private SimpleInventory inventory;

    private void Awake()
    {
        inventory = FindFirstObjectByType<SimpleInventory>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        questText.text = "진행 중인 퀘스트가 없습니다.";
    }

    public void TalkToNPC(Quest npcQuest)
    {
        if (npcQuest.state == QuestStarte.Completed)
        {
            Debug.Log("이미 완료한 퀘스트 입니다.");
            return;
        }

        if (currentQuest == null || currentQuest.state == QuestStarte.Completed)
        {
            StartQuest(npcQuest);
        }

        if (currentQuest != npcQuest)
        {
            Debug.Log("이미 다른 퀘스트를 진행 중입니다.");
            return;
        }

        if (currentQuest.state == QuestStarte.InProgress)
        {
            Debug.Log("아직 아이템이 부족합니다.");
        }
        else if(currentQuest.state == QuestStarte.ReadyToComplete)
        {
            CompleteQuest();
        }
    }

    private void UpdateQuestText()
    {
        if(currentQuest.state == QuestStarte.InProgress)
        {
            questText.text =
                currentQuest.questName + "\n" +
                currentQuest.targetItemName + " " +
                currentQuest.currentAmount + " / " +
                currentQuest.targetAmount;
        }
        else if(currentQuest.state == QuestStarte.ReadyToComplete)
        {
            questText.text = "수집 완료! \nNOC에게 돌아가세요";
        }
        else if(currentQuest.state == QuestStarte.Completed)
        {
            questText.text = currentQuest.questName + "\n퀘스트 완료";
        }
    }

    void StartQuest(Quest newQuest)
    {
        currentQuest = newQuest;
        currentQuest.currentAmount = inventory.GetItemAmount(currentQuest. targetItemName);

        if (currentQuest.currentAmount >= currentQuest.targetAmount)
        {
            currentQuest.currentAmount = currentQuest.targetAmount;
            currentQuest.state = QuestStarte.ReadyToComplete;
        }
        else
        {
            currentQuest.state = QuestStarte.InProgress;
        }

        Debug.Log(currentQuest.questName + "퀘스트 시작!");
        UpdateQuestText();
    }

    public void AddQuestItem(string itemName , int amount)
    {
        if (currentQuest == null) return;
        if (currentQuest.state != QuestStarte.InProgress) return;
        if (currentQuest.targetItemName != itemName) return;

        currentQuest.currentAmount += amount;

        if (currentQuest.currentAmount >= currentQuest.targetAmount)
        {
            currentQuest.currentAmount = currentQuest.targetAmount;
            currentQuest.state = QuestStarte.ReadyToComplete;
        }

        UpdateQuestText();
    }

    public void CompleteQuest()
    {
        if(inventory.RemoveItem(currentQuest.targetItemName, currentQuest.targetAmount) == false)
        {
            Debug.Log("퀘스트 아이템이 부족합니다.");
            return;
        }

        currentQuest.state = QuestStarte.Completed;
        Debug.Log(currentQuest.questName + "퀘스트 완료!");
        UpdateQuestText();
    }
}
