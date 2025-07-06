using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{

    private const string NUMBER_POPUP = "NumberPopUp";
    [SerializeField] private TextMeshProUGUI countDownText;
    private Animator animator;
    private int prevCountDownNumber;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.isCountDownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        int countDownnumber = Mathf.CeilToInt(GameManager.Instance.GetCountDownToStartTimer());
        countDownText.text =countDownnumber.ToString();
        if (prevCountDownNumber != countDownnumber)
        {
            prevCountDownNumber = countDownnumber;
            animator.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.PlayCountDownSound();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
