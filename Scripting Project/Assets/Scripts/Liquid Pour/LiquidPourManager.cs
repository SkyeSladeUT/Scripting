using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidPourManager : MonoBehaviour
{
    public List<PourTapManager> Holders;
    private PourTapManager selectedHolder;
    public int DesiredAmount;

    public void Initialize()
    {
        foreach(var h in Holders)
        {
            h.Initialize();
            h.onTap = OnHolderSelect;
        }
    }

    public void OnHolderSelect(PourTapManager pourManager)
    {
        //set selected
        if(selectedHolder == null)
        {
            selectedHolder = pourManager;
            selectedHolder.Selected = true;
        }
        //deselect selected
        else if(selectedHolder == pourManager){
            selectedHolder = null;
            pourManager.Selected = false;
        }
        //pour selected into tapped
        else
        {
            int amount = selectedHolder.Holder.PourLiquid();
            int remainder = pourManager.Holder.AddLiquid(amount);
            selectedHolder.Holder.AddLiquid(remainder);
            selectedHolder.Selected = false;
            selectedHolder = null;
            if (CheckAmount())
            {
                CompletePuzzle();
            }
        }
    }

    public bool CheckAmount()
    {
        foreach(var h in Holders)
        {
            if(h.Holder.LiquidAmount == DesiredAmount)
            {
                return true;
            }
        }
        return false;
    }

    private void CompletePuzzle()
    {
        Debug.Log("Puzzle Completed");
    }

}
