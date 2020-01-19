using UnityEngine;
using System.Collections;

public class WinPopupModel
{
    public System.Action OnAccept;
}

public class WinPopup : MonoBehaviour
{
    WinPopupModel _model;

    public void Open(WinPopupModel model)
    {
        _model = model;
        gameObject.SetActive(true);
    }

    public void Accept()
    {
        _model.OnAccept?.Invoke();
    }
}
