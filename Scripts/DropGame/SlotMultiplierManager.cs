using UnityEngine;
using TMPro;

public class SlotMultiplierManager : MonoBehaviour
{
    public int[] multipliers = { 5, 10, 15, 20, 25, 30 };
    public TMP_Text[] slotTexts;
    public BoostTimer boostTimer;

    private void Start()
    {
        ShuffleMultipliers();
    }

    public void OnBallInSlot(int slotIndex)
    {
        int chosenMultiplier = multipliers[slotIndex];

        boostTimer.ActivateMultiplier(chosenMultiplier);

        ResultMenuController.instance.ShowMenu(chosenMultiplier);

        ShuffleMultipliers();
        //ShuffleMultipliers();
    }

    private void ShuffleMultipliers()
    {
        for (int i = 0; i < multipliers.Length; i++)
        {
            int rnd = Random.Range(0, multipliers.Length);
            (multipliers[i], multipliers[rnd]) = (multipliers[rnd], multipliers[i]);
        }

        for (int i = 0; i < slotTexts.Length; i++)
            slotTexts[i].text = "x" + multipliers[i];
    }
}
