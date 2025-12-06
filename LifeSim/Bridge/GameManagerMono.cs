using UnityEngine;
using LifeSim.Core;
using System.Collections.Generic;
using Newtonsoft.Json; // <-- add this

public class GameManagerMono : MonoBehaviour
{
    public static GameManagerMono Instance;

    public int seed = -1;
    public Player player;
    public WorldState world;
    public EventEngine eventEngine;
    public RandomUtil rng;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        rng = seed <= 0 ? new RandomUtil() : new RandomUtil(seed);
        player = new Player();
        world = new WorldState();
        eventEngine = new EventEngine(rng);

        // Load events from JSON
        TextAsset eventsText = Resources.Load<TextAsset>("events");
        if (eventsText != null)
        {
            var events = JsonConvert.DeserializeObject<List<EventDef>>(eventsText.text); // <-- patched
            eventEngine.LoadEvents(events);
        }

        // Optional: load items or NPCs using same pattern
        
        TextAsset itemsText = Resources.Load<TextAsset>("items");
        var items = JsonConvert.DeserializeObject<List<Item>>(itemsText.text);

        TextAsset npcsText = Resources.Load<TextAsset>("npcs");
        var npcs = JsonConvert.DeserializeObject<List<NPC>>(npcsText.text);
        
    }

    public void AdvanceDay()
    {
        world.NextDay();
        player.energy = Mathf.Max(0, player.energy - 10f);
        player.happiness = Mathf.Max(0, player.happiness - 1f);
    }

    public void OnChoiceSelected(EventChoice choice)
    {
        eventEngine.ApplyChoiceEffects(player, world, choice);
        if (!player.isAlive || player.health <= 0)
        {
            player.isAlive = false;
            Debug.Log("You died!");
        }
    }
}
