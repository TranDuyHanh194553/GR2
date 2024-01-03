using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillScript : MonoBehaviour
{
    public float cooldownTime = 6f; // Thời gian hồi kỹ năng (60 giây)
    private float currentTime = 0f;
    private bool isCooldown = false;

    public Button skillButton; // Nút kỹ năng
    public TextMeshProUGUI cooldownText; // Text hiển thị thời gian đếm ngược
    public Image cooldownFill;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        skillButton.onClick.AddListener(ActivateSkill); // Gán hàm ActivateSkill cho sự kiện click nút kỹ năng
        currentTime = cooldownTime; // Set thời gian ban đầu bằng thời gian hồi
        UpdateCooldownUI();
    }

    void Update()
    {
        // Nếu đang trong trạng thái hồi kỹ năng
        if (isCooldown)
        {
            currentTime -= Time.deltaTime;
            UpdateCooldownUI();

            // Kết thúc thời gian hồi kỹ năng
            if (currentTime <= 0)
            {
                currentTime = 0;
                isCooldown = false;
                skillButton.interactable = true; // Cho phép sử dụng kỹ năng lại
                cooldownText.gameObject.SetActive(false);
            }
        }
    }

    void ActivateSkill()
    {
        // Kiểm tra xem có thể sử dụng kỹ năng không
        if (!isCooldown)
        {
            audioManager.PlaySFX(audioManager.click);
            // Kích hoạt kỹ năng ở đây
            Debug.Log("Skill activated");
            cooldownText.gameObject.SetActive(true);
            // Bắt đầu thời gian hồi kỹ năng
            currentTime = cooldownTime;
            isCooldown = true;
            skillButton.interactable = false; // Vô hiệu hóa nút kỹ năng trong thời gian hồi
        }
    }

    void UpdateCooldownUI()
    {
        // Hiển thị thời gian đếm ngược trên giao diện người dùng

        float fillAmount = 1 - (currentTime / cooldownTime);
        // Cập nhật fillAmount của Image để hiển thị sự mở rộng của vòng tròn
        cooldownFill.fillAmount = fillAmount;

        // Sử dụng fillAmount để kiểm soát sự mờ của ô kỹ năng
        Color buttonColor = skillButton.image.color;
        buttonColor.a = Mathf.Lerp(0.5f, 1f, fillAmount);
        skillButton.image.color = buttonColor;

        // // Hiển thị thời gian đếm ngược trên Text
        cooldownText.text = Mathf.Ceil(currentTime).ToString() + "s";
        // Debug.Log("End of cooldownText");
    }
}
