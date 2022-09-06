using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaddle : MonoBehaviour
{
    public float controlSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        float adjustedControlSpeed = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? controlSpeed * 2 : controlSpeed;

        if (Input.GetKey(KeyCode.DownArrow))
        {
            Vector2 curPos = transform.position;
            curPos += new Vector2(0, adjustedControlSpeed * Time.deltaTime * -1);

            curPos.y = Mathf.Clamp(curPos.y, -4.25f, 4.25f);

            transform.position = curPos;
		}
        else if (Input.GetKey(KeyCode.UpArrow))
		{

            Vector2 curPos = transform.position;
            curPos += new Vector2(0, adjustedControlSpeed * Time.deltaTime);

            curPos.y = Mathf.Clamp(curPos.y, -4.25f, 4.25f);

            transform.position = curPos;
        }
    }
}
