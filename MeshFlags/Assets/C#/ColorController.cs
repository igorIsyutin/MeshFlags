using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorController : MonoBehaviour
{
    [SerializeField] private Dropdown[] _dropdowns;

    private int _colorsQuantity = 1;

    public void OnValueChaged(Dropdown dd)
    {
        foreach (var dropdown in _dropdowns)
        {
            dropdown.gameObject.SetActive(false);
        }

        _colorsQuantity = dd.value + 1;
        
        for (int i = 0; i < _colorsQuantity; i++)
        {
            _dropdowns[i].gameObject.SetActive(true);
        }
    }

    public Color[] GetColors()
    {
        Color[] colors = new Color[_colorsQuantity];
        for (int i = 0; i < _colorsQuantity; i++)
        {
            colors[i] = GetDDColor(_dropdowns[i]);
        }

        return colors;
    }

    private Color GetDDColor(Dropdown dd)
    {
        switch (dd.value)
        {
            case 0:
                return Color.red;
            case 1:
                return Color.green;
            case 2:
                return Color.blue;
            case 3:
                return Color.yellow;
            case 4:
                return Color.white;
            case 5:
                return Color.magenta;
            case 6:
                return Color.black;
            case 7:
                return new Color(0.2f, 0.3f, 0.4f);
            default:
                return Color.black;
        }
    }

    public void OnDrowClicked()
    {
        foreach (var dropdown in _dropdowns)
        {
            dropdown.gameObject.SetActive(false);
        }
    }

}
