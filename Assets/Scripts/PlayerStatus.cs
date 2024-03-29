using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    PlayerMovement playerMovement;

    [SerializeField] Slider cholesterolSlider;
    [SerializeField] TMP_Text cholesterolText;
    [SerializeField] Slider weightSlider;
    [SerializeField] TMP_Text weightText;

    [NonSerialized] public float cholesterolValue = 65f;
    [NonSerialized] public float weightValue = 120f;
    [NonSerialized] public float maxCholesterol = 100f;
    [NonSerialized] public float maxWeight = 150f;
    [NonSerialized] public float cholesterolThreshold = 70f;
    [NonSerialized] public float weightThreshold = 120f;

    [SerializeField] float cholesterolSpeed = 0.8f;
    [SerializeField] float weightSpeed = 0.4f;
    [NonSerialized] public bool isCholesterolChanging = true;

    [NonSerialized] public bool isDead;

    private void Start() {
        playerMovement = GetComponent<PlayerMovement>();

        if (StaticUtil.isInvoked) {
            StaticUtil.isInvoked = false;
            this.gameObject.transform.position = StaticUtil.lastPosition;
            cholesterolValue = StaticUtil.lastCholesterol;
            weightValue = StaticUtil.lastWeight;

            if (StaticUtil.isDead) {
                Die("Atherosclerosis.");
            }
        }
    }

    private void Update() {
        UpdateSliders();
        if (cholesterolValue > 75f) {
            Die("coronary heart disease");
        }
    }

    private void UpdateSliders() {
        if (isCholesterolChanging) {
            cholesterolValue = Mathf.Clamp(cholesterolValue + cholesterolSpeed * Time.deltaTime, 0f, maxCholesterol);
        }
        weightValue = Mathf.Clamp(weightValue + weightSpeed * Time.deltaTime, 0f, maxWeight);

        cholesterolSlider.value = cholesterolValue / maxCholesterol;
        weightSlider.value = weightValue / maxWeight;

        cholesterolText.color = cholesterolValue >= cholesterolThreshold ? new Color(1f, 0.3764706f, 0.3764706f, 1f) : new Color(1f, 1f, 1f, 1f);
        weightText.color = weightValue >= weightThreshold ? new Color(1f, 0.3764706f, 0.3764706f, 1f) : new Color(1f, 1f, 1f, 1f);
    }

    public void Die(string deathMessage) {
        playerMovement.deathMessage = deathMessage;
        isDead = true;
    }
}
