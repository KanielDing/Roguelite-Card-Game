using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class CombatUnit : MonoBehaviour
{
    public Slider healthBar;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI armorText;
    public TextMeshProUGUI titleText;
    public GameObject textPopupPrefab;
    public Vector3 popUpEffectPosition;

    protected int maxHp;
    protected int currentHp;
    protected int currentArmor = 0;
    protected string title;

    public void SetHpAndMaxHp(int hp, int maxHp)
    {
        currentHp = hp;
        this.maxHp = maxHp;
        UpdateHealthBarAndArmorValue();
    }
    public void ResetArmor()
    {
        currentArmor = 0;
        UpdateHealthBarAndArmorValue();
    }
    public void DealDamage(int damageAmount)
    {
        CameraController.instance.Shake(damageAmount);
        if (currentArmor > 0)
        {
            int leftOverDamage = damageAmount - currentArmor;
            currentArmor -= damageAmount;
            if (currentArmor < 0) currentArmor = 0;

            damageAmount = leftOverDamage > 0 ? leftOverDamage : 0;
        }
        CreatePopUp(Color.red, $"-{damageAmount} hp");
        currentHp -= damageAmount;
        UpdateHealthBarAndArmorValue();
        if (currentHp <= 0)
        {
            StartCoroutine(Die());
        }
    }
    
    public void GainArmor(int armorAmount)
    {
        currentArmor += armorAmount;
        CreatePopUp(Color.grey, $"+{armorAmount} armor");
        UpdateHealthBarAndArmorValue();
    }

    public void Heal(int healAmount)
    {
        CreatePopUp(Color.green, $"+{healAmount} hp");
        currentHp += healAmount;
        EventManager.TriggerEvent(EventName.ON_PLAYER_HEAL.ToString(), new EventData().With(integer: healAmount));
        if (currentHp > maxHp) currentHp = maxHp;
        UpdateHealthBarAndArmorValue();
    }

    protected abstract IEnumerator Die();

    protected void UpdateHealthBarAndArmorValue()
    {
        healthBar.maxValue = maxHp;
        healthBar.value = currentHp;
        healthText.text = currentHp.ToString() + '/' + maxHp;
        if (currentArmor == 0)
        {
            armorText.enabled = false;
        }
        else
        {
            armorText.enabled = true;
            armorText.text = currentArmor.ToString();
        }
    }

    public void CreatePopUp(Color color, string text)
    {
        TextPopup textPopup = Instantiate(textPopupPrefab, popUpEffectPosition, Quaternion.identity, transform).GetComponent<TextPopup>();
        textPopup.Initialise(color , text);
    }
}