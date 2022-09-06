using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUPaddle : MonoBehaviour
{
    public float controlSpeed = 6f;

    // Update is called once per frame
    void Update()
    {
        List<GameObject> currentBalls = Game.instance._currentBalls;

        // Default to 0 Y if there are no balls
        float wantY = 0f;

        // If there are balls, find the highest X value and match its Y
        if (currentBalls.Count > 0)
        {
            float maxX = currentBalls[0].transform.position.x;
            wantY      = currentBalls[0].transform.position.y;

            // Get closest ball
            foreach (GameObject go in currentBalls)
            {
                if (go.transform.position.x > maxX)
                {
                    maxX  = go.transform.position.x;
                    wantY = go.transform.position.y;
                }

            }
        }

        // Move towards the desired Y
        float yDiff = wantY - transform.position.y;
        float adjustedControlSpeed = controlSpeed;
        
        float sign = yDiff / Mathf.Abs(yDiff);
        if (yDiff == 0)
            sign = 0;

        // If the yDiff is significantly more than what it can move, then double speed (equivilant to using shift for player)
        if (Mathf.Abs(yDiff) > controlSpeed * Time.deltaTime)
        {
            adjustedControlSpeed *= 2;
            float moveableY = transform.position.y + adjustedControlSpeed * sign * Time.deltaTime;
            transform.position = new Vector2(transform.position.x, moveableY);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, wantY);
        }

    }
}
