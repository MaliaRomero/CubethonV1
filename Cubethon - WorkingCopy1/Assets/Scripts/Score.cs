using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI TextName;

    // Update is called once per frame
    void Update()
    {
        TextName.SetText(player.position.z.ToString("0"));
    }
}
