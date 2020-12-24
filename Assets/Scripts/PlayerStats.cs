using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int QCooldown = 3;
    private int LShiftCooldown = 5;
    private int ECooldown = 5;
    private const int EActiveTime = 3;
    private int RCooldown = 8;
    private const int RActiveTime = 2;
    
    [Range(0, 3)][SerializeField]public int difficulty = 0;

    // Start is called before the first frame update
    void Start()
    {
        float scale = 1f;
        switch (difficulty)
        {
            // Casual
            case(0):
                scale = 1f;
                break;
            // Normal
            case(1):
                scale = 2f;
                break;
            // Rage mode
            case(2):
                scale = 1.5f;
                break;
            // Impossible
            case(3):
                scale = 2f;
                break;
        }

        QCooldown = (int)(QCooldown * scale);
        LShiftCooldown = (int)(LShiftCooldown * scale);
        ECooldown = (int)(ECooldown * scale);
        RCooldown = (int)(RCooldown * scale);
    }
}
