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
        SetResourceText(param.type, param.currentResource, param.resourceDelta);
        moneyText.text = formatMoney(param.currentMoney);
        CreateSlidingText(moneyText.transform, param.moneyDelta > 0, (param.moneyDelta > 0 ? "+" : "") + param.moneyDelta);
    }

    public void HandleTaxes(TaxesEP param)
    {
        moneyText.text = formatMoney(param.currentMoney);
        CreateSlidingText(moneyText.transform, false, "Taxes: -" + formatMoney(param.amount));
    }

    // !!!!!
    public void SetResourceText(ResourceType type, int value, int delta)
    {
        Text textToChange = null;
        if (type == ResourceType.GOLD)
        {
            textToChange = goldText;
        } else if (type == ResourceType.WOOD)
        {
            textToChange = woodText;
        } else if (type == ResourceType.OIL)
        {
            textToChange = oilText;
        }
        textToChange.text = value.ToString();

        CreateSlidingText(textToChange.transform, delta > 0, (delta > 0 ? "+" : "") + delta);
    }

    private string formatMoney(int money)
    {
        var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
        nfi.NumberGroupSeparator = " ";
        return money.ToString("#,0.00 $", nfi);
    }

    private void CreateSlidingText(Transform parent, bool positive, string text)
    {
        var instance = Instantiate(slidingText, parent);
        instance.transform.position = parent.position;
        instance.Init(positive, text);
    }
}
