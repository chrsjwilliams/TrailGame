using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestTileTypeButton : MonoBehaviour
{
    [SerializeField] CanvasGroup normalMapsGroup;
    [SerializeField] CanvasGroup tileTypeTestGroup;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] TextMeshProUGUI screenText;

    [SerializeField] bool showingNormalMaps;

    private void Start()
    {
        showingNormalMaps = true;
        buttonText.text = "Test Tile Type";

        screenText.text = "Normal Map Select";

        normalMapsGroup.ShowGroup(true);
        tileTypeTestGroup.ShowGroup(false);
    }

    public void OnPressed()
    {
        showingNormalMaps = !showingNormalMaps;
        buttonText.text = showingNormalMaps ? "Test Tile Type" : "Show Normal Maps";
        screenText.text = showingNormalMaps ? "Normal Map Select" : "Test Tile Type Map Select";

        normalMapsGroup.ShowGroup(showingNormalMaps);
        tileTypeTestGroup.ShowGroup(!showingNormalMaps);
    }
}
