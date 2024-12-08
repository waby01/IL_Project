using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class QuestManager : MonoBehaviour
{
    [System.Serializable]
    public class Quest
    {
        public string targetFishTag;
        public Image questImage;
        public bool isCompleted = false;
    }

    public Quest[] quests;
    public FinishGameScreen finishGameScreen;

    public void CompleteQuest(string fishTag)
    {
        foreach (Quest quest in quests)
        {
            if (quest.targetFishTag == fishTag && !quest.isCompleted)
            {
                Debug.Log("Quest completed for fish tag: " + fishTag);
                quest.questImage.color = Color.green;
                quest.isCompleted = true;
                CheckAllQuestsCompleted();
                return;
            }
        }
    }

    private void CheckAllQuestsCompleted()
    {
        foreach (Quest quest in quests)
        {
            if (!quest.isCompleted) return;
        }

        if (finishGameScreen != null)
        {
            Time.timeScale = 0;
            finishGameScreen.Setup();
        }
    }
}
