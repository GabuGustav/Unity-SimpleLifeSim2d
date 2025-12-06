using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MusicVolumeSlider : MonoBehaviour
{
    public Slider slider;

    private IEnumerator Start()
    {
        if (slider == null)
            slider = GetComponent<Slider>();

        slider.interactable = false; // disable until ready

        // Wait for MusicManager to exist
        while (MusicManager.Instance == null)
            yield return null;

        // Now safe
        float saved = MusicManager.Instance.GetSavedSliderValue();
        slider.value = saved;

        slider.onValueChanged.AddListener(MusicManager.Instance.SetVolumeFromSlider);

        slider.interactable = true;
    }
}
