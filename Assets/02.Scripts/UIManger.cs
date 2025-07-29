using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManger : MonoBehaviour
{
    [Header("상단 UI")]
    public TMP_Text moneyTxt;       // 베팅 금액만큼 차감하고 팟 얻으면 상승
    // ShowDown까지 한 사이클 돌면 Round1에서 2,3 순으로 계속 증가
    public TMP_Text roundTxt;
    // Pre-Flop > Flop > Turn > River > ShowDown 순으로 각 턴에 맞게 변경
    // 플레이어가 LeaveGame 이후 재참여 하면 Round, Turn은 초기 상태로 변경
    public TMP_Text turnTxt;
    [Header("Btn 상호작용")]
    public Canvas DeckInfo;
    public RectTransform ruleBG;
    public RectTransform betBG;     // Rule UI 열리면 비활성화
    public Image firstRulePage;
    public Image secondRulePage;
    public Image thirdRulePage;
    public TMP_InputField betInput;
    public TMP_InputField raiseInput;

    public void OnClickDeckGuide()
    {

    }

    public void OnClickLeaveGame()
    {

    }

    public void OnClickGameRule()
    {

    }

    public void OnClickFold()
    {

    }

    public void OnClickCheck()
    {

    }

    public void OnClickBet()
    {

    }

    public void OnClickCall()
    {

    }

    public void OnClickRaise()
    {

    }

    public void OnClickRuleExit()
    {

    }

    public void OnClickPrevPage()
    {

    }

    public void OnClickNextPage()
    {

    }
}
