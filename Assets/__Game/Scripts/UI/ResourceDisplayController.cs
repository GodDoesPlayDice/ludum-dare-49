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

    [SerializeField]
    private SlidingTextController slidingText;

    public void HandlePortfolioChanged(PortfolioChangedEP param)
    {
        SetResourceText(param.type, param.currentResource);
        moneyText.text = formatMoney(param.currentMoney);
    }

    public void HandleTaxes(TaxesEP param)
    {
        moneyText.text = formatMoney(param.currentMoney);
        var instance = Instantiate(slidingText, moneyText.transform);
        instance.transform.position = moneyText.transform.position;
        instance.Init(false, "Taxes: -" + formatMoney(param.amount));
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

    private string formatMoney(int money)
    {
        var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
        nfi.NumberGroupSeparator = " ";
        return money.ToString("#,0.00 $", nfi);
    }
}
