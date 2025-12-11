using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SlotTrigger : MonoBehaviour
{
    public int slotIndex;
    public SlotMultiplierManager manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.OnBallInSlot(slotIndex);
            
        }
    }
    private void Start()
    {
        string SlotText = GetComponentInChildren<TextMeshPro>().text;
        for (int I = 0; I < manager.slotTexts.Length; I++)
        {
            if (SlotText == manager.slotTexts[I].text)
            {
                slotIndex = I;
                break;
            }
        }
        
    }
}
