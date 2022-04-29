using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ColourPicker : MonoBehaviour
{
    public TMP_Text colourDebugText;

    private RectTransform Rect;

    private Texture2D colorTexture;
    private SettingsController _settingsController;
    
    public Material playerOneMaterial;
    public Material playerTwoMaterial;
    public Material playerThreeMaterial;

    public Color colorPickerColor;

    public Color savedColorPickerColor;
    
    void Start()
    {
        Rect = GetComponent<RectTransform>();
        colorTexture = GetComponent<Image>().mainTexture as Texture2D;
    }

    
    void Update()
    {
        Vector2 delta;
        //Gets the mouse position inside the rectangle
        RectTransformUtility.ScreenPointToLocalPointInRectangle(Rect, Mouse.current.position.ReadValue(), null, out delta);

        //prints mouse position
        string debug = "mousePosition= " + Mouse.current.position.ReadValue();
        //adds position inside the color picker square. 0,0 is now in the middle 
        debug += "<br>delta=" + delta;

        
        //Gets the offset value, so the bottom left of the square is 0
        float width = Rect.rect.width;
        float height = Rect.rect.height;
        delta += new Vector2(width * .5f, height * .5f);
        debug += "<br>offset delta=" + delta;

        //Converts the offset delta to a value between 0 and 1. 1 is top right, 0 is bottom left.
        float x = Mathf.Clamp(delta.x / width, 0, 1);
        float y = Mathf.Clamp(delta.y / height, 0, 1);
        
        debug += "<br>x="+ x + "<br>y=" +y;

        //Converts the offset delta value that's between 0 and 1 to a color value that we can use for the shader.
        int texX = Mathf.RoundToInt(x * colorTexture.width);
        int texY = Mathf.RoundToInt(y * colorTexture.width);
        debug += "<br>texX="+ texX + " texY=" +texY;

        colorPickerColor = colorTexture.GetPixel(texX, texY);
        //colourDebugText.color = colorPickerColor;
        colourDebugText.text = debug;
    }

    public void OnClickColorPicker(int colorObject)
    {
        if (colorObject == 1)
        {
            savedColorPickerColor = colorPickerColor;
            colourDebugText.color = savedColorPickerColor;
            //Saves color as string
            PlayerPrefs.SetString("PlayerOneColor",ColorUtility.ToHtmlStringRGBA(savedColorPickerColor));
            
        }
        else if (colorObject == 2)
        {
            savedColorPickerColor = colorPickerColor;
            colourDebugText.color = savedColorPickerColor;
            //Saves color as string
            PlayerPrefs.SetString("PlayerTwoColor",ColorUtility.ToHtmlStringRGBA(savedColorPickerColor));
            
        }
    }

    public void OnResetColorPicker(int colorObejct)
    {
        if (colorObejct == 1)
        {
            playerOneMaterial.SetColor("_Color", _settingsController.player1Default);
        }
        else if (colorObejct == 2)
        {
            playerTwoMaterial.SetColor("_Color", _settingsController.player2Default);
        }
        else if (colorObejct == 3)
        {
            playerThreeMaterial.SetColor("_Color", _settingsController.player3Default);
        }
    }
    
    //Loads the color from playerprefs
    private void LoadColor()
    {
        ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("PlayerOneColor"), out savedColorPickerColor);
    }
    
}
