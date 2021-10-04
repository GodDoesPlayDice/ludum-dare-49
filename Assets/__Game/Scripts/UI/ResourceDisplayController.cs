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
        CreateSlidingText(moneyText.transform, param.moneyDelta);
    }

    public void HandleTaxes(TaxesEP param)
    {
        moneyText.text = formatMoney(param.currentMoney);
        CreateSlidingText(moneyText.transform, param.amount * -1, "Taxes: -" + formatMoney(param.amount));
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

        CreateSlidingText(textToChange.transform, delta);
    }

    private string formatMoney(int money)
    {
        var nfi = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
        nfi.NumberGroupSeparator = " ";
        return money.ToString("#,0.00 $", nfi);
    }

    private void CreateSlidingText(Transform parent, int delta, string stringValue = null)
    {
        if (delta == 0)
        {
            return;
        }
        var instance = Instantiate(slidingText, parent);
        instance.transform.position = parent.position;
        if (stringValue == null)
        {
            stringValue = (delta > 0 ? "+" : "") + delta;
        }
        instance.Init(delta > 0, stringValue);
    }
}
