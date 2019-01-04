using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Global
{
    private static int best_castell = 0;

    public static int BestCastell
    {
        get 
        {
            return best_castell;
        }
        set 
        {
            best_castell = value;
        }
    }

}