using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonWithHoverSound : MonoBehaviour, IPointerEnterHandler
{

    // Chamado quando o mouse entra na área do botão
    public void OnPointerEnter(PointerEventData eventData)
    {
       SoundManager.Instance.PlayButtonSound();
    }
}
