using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class buttonbackmanager : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

   
}
