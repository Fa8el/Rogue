using UnityEngine;

public class Almuerzo : Agarrable
{
    public override void OnPick()
    {
        base.OnPick();
        managerAlmuerzo.instancia?.SumarAlmuerzo();
    }
}

