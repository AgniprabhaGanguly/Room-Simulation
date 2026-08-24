using UnityEngine;
using TMPro;
public class UIScript : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;

    public void OnSelect()
    {
        int index = dropdown.value;
        switch (index)
        {
            case 0:
                DayChanger.instance.Sunrise();
                break;
            case 1:
                DayChanger.instance.Noon();
                break;
            case 2:
                Debug.Log(index);
                break;
            case 3:
                DayChanger.instance.Night();
                break;
        }
    }
}
