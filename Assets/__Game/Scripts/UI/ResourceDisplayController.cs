using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using System.Text;

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

        var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
        nfi.NumberGroupSeparator = " ";

        SetResourceText(param.type, param.currentResource);
        moneyText.text = param.currentMoney.ToString("#,0.00 $", nfi);
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
