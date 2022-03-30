using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MessageBar : MonoBehaviour
{
    private MessageType previousMessageType;

    [SerializeField]
    private RawImage raw_Icon;
    [SerializeField]
    private TextMeshProUGUI txt_Message;
    [SerializeField]
    private List<Texture> icons;

    void Start()
    {
        this.previousMessageType = MessageType.Message;
    }

    public void DisplayMessage(Message message)
    {
        if (string.IsNullOrWhiteSpace(message.Text))
        {
            base.gameObject.SetActive(false);
            return;
        }

        base.gameObject.SetActive(true);
        if (message.Type != this.previousMessageType)
        {
            Texture texture = this.icons[(int)message.Type];
            this.raw_Icon.texture = texture;
            this.previousMessageType = message.Type;
        }
        this.txt_Message.text = message.Text;
    }
}
