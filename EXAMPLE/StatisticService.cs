using System.Runtime.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Architecture;
using System.Collections;

public class StatisticService : InteractorBase
{
    public override IEnumerator InitializeInteractor()
    {
        yield return new WaitForSeconds(1);
        Debug.Log($"{this} is Initialized");
    }

    public override IEnumerator StartInteractor()
    {
        yield return new WaitForSeconds(1);
        Debug.Log($"{this} is Started");
    }
}
