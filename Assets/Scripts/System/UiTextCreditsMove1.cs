using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiTextCreditsMove1 : MonoBehaviour
{
    [SerializeField] private RectTransform[] Background;
    public float moveSpeed;
    public float targetHeight;


    private void LateUpdate()
    {
        MoveDown();
    }

    private void MoveDown()
    {
        for (int i = 0; i < Background.Length; i++)
        {
            Background[i].transform.position = Vector2.MoveTowards(Background[i].transform.position,
                new Vector2(Background[i].transform.position.x, targetHeight), moveSpeed * Time.deltaTime);
        }
    }
}