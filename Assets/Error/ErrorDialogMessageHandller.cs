using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorDialogMessageHandller : ErrorDialogMessageHandlerField
{

    public TMPro.TMP_Text TMPMessage;

    private void Start()
    {
        TMPMessage.text = ErrorMessageText;
    }
}
