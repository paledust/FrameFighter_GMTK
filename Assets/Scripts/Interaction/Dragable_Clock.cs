using UnityEngine;

public class Dragable_Clock : Basic_Clickable
{
    public override void OnClick(PlayerController player, Vector3 hitPos)
    {
        // Implement the logic for when the clock is clicked
        Debug.Log("Clock clicked!");
    }

    public override void OnHover(PlayerController player)
    {
        // Implement the logic for when the clock is hovered over
        Debug.Log("Clock hovered!");
    }

    public override void OnRelease(PlayerController player)
    {
        // Implement the logic for when the clock is released
        Debug.Log("Clock released!");
    }
}
