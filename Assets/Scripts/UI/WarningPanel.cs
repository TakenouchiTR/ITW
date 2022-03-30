using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WarningPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI txt_Message;
    [SerializeField]
    private Button btn_Acknowledge;

    public void DisplayMessage(Message message)
    {
        this.txt_Message.text = message.Text;
        this.btn_Acknowledge.interactable = false;
        base.gameObject.SetActive(true);
        base.StartCoroutine(this.EnableButtonCoroutine());
    }

    private IEnumerator EnableButtonCoroutine()
    {
        yield return new WaitForSeconds(5);
        this.btn_Acknowledge.interactable = true;
    }
}
