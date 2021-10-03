using UnityEngine;
using UnityEngine.UI;

public class ResourceDisplayController : MonoBehaviour
{
    [SerializeField]
    private Text goldText;

    [SerializeField]
    private Text woodText;

    [SerializeField]
    private Text oilText;

    [SerializeField]
    private Text moneyText;

    public void HandlePortfolioChanged(PortfolioChangedEP param)
    {
        SetResourceText(param.type, param.currentResource);
        moneyText.text = param.currentMoney.ToString() + " $";
    }

    // !!!!!
    public void SetResourceText(ResourceType type, int value)
    {
        if (type == ResourceType.GOLD)
        {
            goldText.text = value.ToString();
        } else if (type == ResourceType.WOOD)
        {
            woodText.text = value.ToString();
        } else if (type == ResourceType.OIL)
        {
            oilText.text = value.ToString();
        }
    }
}
