using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementObjects : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Slider _progressSlider;
    [SerializeField] private TMP_Text _progressText;
    [SerializeField] private Image _icon;

    [SerializeField] private GameObject _rewardGameobject;
    [SerializeField] private Button _rewardButton;
    [SerializeField] private TMP_Text _rewardText;

    public TMP_Text Name { get => _name; set => _name = value; }
    public TMP_Text Description { get => _description; set => _description = value; }
    public Slider ProgressSlider { get => _progressSlider; set => _progressSlider = value; }
    public TMP_Text ProgressText { get => _progressText; set => _progressText = value; }
    public Image Icon { get => _icon; set => _icon = value; }
    public GameObject RewardGameobject { get => _rewardGameobject; set => _rewardGameobject = value; }
    public Button RewardButton { get => _rewardButton; set => _rewardButton = value; }
    public TMP_Text RewardText { get => _rewardText; set => _rewardText = value; }
}
