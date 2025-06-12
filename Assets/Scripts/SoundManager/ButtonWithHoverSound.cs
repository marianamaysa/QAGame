using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonWithHoverSound : MonoBehaviour, IPointerEnterHandler
{

    // Chamado quando o mouse entra na �rea do bot�o
    public void OnPointerEnter(PointerEventData eventData)
    {
       SoundManager.Instance.PlayButtonSound();
    }
}
