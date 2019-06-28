using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class SkillSlot : MonoBehaviour
{
    public Skill Skill { get; set; }
    public bool IsActive { get; set; }

    Image icon;
    Transform isActiveImage;

    void Awake()
    {
        icon = GetComponent<Image>();
        isActiveImage = transform.GetChild(0);
    }

    void Start()
    {
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        icon.sprite = Skill.Icon;
        isActiveImage.gameObject.SetActive(IsActive);
    }

    public void OnButtonClick()
    {
        FindObjectOfType<CharacterSkills>().SetSkill(Skill, IsActive);
    }

}
