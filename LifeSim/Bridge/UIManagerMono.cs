using UnityEngine;
using UnityEngine.UI;
using LifeSim.Core;
using System.Collections.Generic;
using TMPro;

public class UIManagerMono : MonoBehaviour
{
    public TextMeshProUGUI eventText;
    public Button[] choiceButtons;
    public TextMeshProUGUI statsText;
    private EventDef currentEvent;

    void Start()
    {
        RefreshUI();
    }

    public void ShowEvent(EventDef e)
    {
        currentEvent = e;
        eventText.text = e.text;

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < e.choices.Count)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].GetComponentInChildren<Text>().text = e.choices[i].text;
                int idx = i;
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => {
                    GameManagerMono.Instance.OnChoiceSelected(e.choices[idx]);
                    RefreshUI();
                });
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }
        UpdateStatsPanel();
    }

    public void UpdateStatsPanel()
    {
        var p = GameManagerMono.Instance.player;
        statsText.text = $"HP:{p.health:F0} EN:{p.energy:F0} HAP:{p.happiness:F0} $:{p.money:F0}";
    }

    public void RefreshUI()
    {
        var gm = GameManagerMono.Instance;
        var e = gm.eventEngine.PickEvent(gm.player, gm.world);
        if (e != null) ShowEvent(e);
        else
        {
            eventText.text = "No event found. You wait a day...";
            foreach (var b in choiceButtons) b.gameObject.SetActive(false);
        }
        UpdateStatsPanel();
    }
}
